// BinderForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.Saving;

namespace Liber.Forms.Reports;

internal partial class BinderForm : Form
{
    private const int FilterOffset = 1;
    private const int JsonFilterIndex = 1;
    private const int PdfFilterIndex = 2;

    private readonly ReportEngine _engine;
    private readonly ReportWebView _webView = new ReportWebView();

    private string? _path;

    public BinderForm(ReportEngine engine, IEnumerable<IReportView> views)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);
        _listView.Initialize(engine);
        _customReportListView.Initialize(engine, views);

        _engine = engine;
    }

    public IEnumerable<IReportView> Sections
    {
        get
        {
            foreach (ListViewItem item in _customReportListView.Items)
            {
                yield return (IReportView)item.Tag!;
            }
        }
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        await _webView.InitializeWebViewAsync(_recentPathManager);
    }

    private void OnDragOver(object sender, DragEventArgs e)
    {
        if (e.Data is not null && e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effect = DragDropEffects.Copy;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private async void OnDragDrop(object sender, DragEventArgs e)
    {
        if (e.Data == null || e.Data.GetData(DataFormats.FileDrop) is not string[] paths || paths.Length == 0)
        {
            return;
        }

        if (await TryCancelAsync())
        {
            return;
        }

        await ImportAsync(paths[0]);
    }

    private void OnAddToolStripMenuItemClick(object sender, EventArgs e)
    {
        foreach (ListViewItem selectedItem in _listView.SelectedItems)
        {
            IReportView view = ((IReportView)selectedItem.Tag!).Clone();

            _customReportListView.InsertReport(
                _customReportListView.Items.Count,
                _engine,
                Guid.NewGuid().ToString(),
                view);
            _customReportListView.AutoResizeColumns();
        }
    }

    private void OnEditToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_customReportListView.SelectedItems.Count != 1)
        {
            return;
        }

        IReportView view = (IReportView)_customReportListView.SelectedItems[0].Tag!;
        ReportsForm form = new ReportsForm(_engine, ReportsFormStyle.ConfigurationOnly)
        {
            WindowState = FormWindowState.Normal
        };

        if (view.Properties is IntervalView report)
        {
            report.Accounts = new AccountsView(_engine.Company);
        }

        form.InitializeReport(view);
        form.ShowDialog();

        _customReportListView.SelectedItems[0].Text = view.Properties.Title;
    }

    private void OnRenameToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_customReportListView.SelectedItems.Count == 1)
        {
            _customReportListView.SelectedItems[0].BeginEdit();
        }
    }

    private void OnDuplicateToolStripMenuItemClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in _customReportListView.SelectedItems)
        {
            IReportView view = ((IReportView)item.Tag!).Clone();

            _customReportListView.InsertReport(
                item.Index + 1,
                _engine,
                Guid.NewGuid().ToString(),
                view);
        }
    }

    private void OnRemoveToolStripMenuItemClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in _customReportListView.SelectedItems)
        {
            item.Remove();
        }
    }

    private void OnMoveUpToolStripMenuItemClick(object sender, EventArgs e)
    {
        ListViewItem[] selectedItems = new ListViewItem[_customReportListView.SelectedItems.Count];

        _customReportListView.SelectedItems.CopyTo(selectedItems, index: 0);
        Array.Sort(selectedItems, (left, right) => left.Index - right.Index);

        foreach (ListViewItem item in selectedItems)
        {
            int index = item.Index;

            if (index == 0)
            {
                continue;
            }

            _customReportListView.Items.RemoveAt(index);
            _customReportListView.Items.Insert(index - 1, item);
        }
    }

    private void OnMoveDownButtonClick(object sender, EventArgs e)
    {
        ListViewItem[] selectedItems = new ListViewItem[_customReportListView.SelectedItems.Count];

        _customReportListView.SelectedItems.CopyTo(selectedItems, index: 0);
        Array.Sort(selectedItems, (left, right) => right.Index - left.Index);

        int lastIndex = _customReportListView.Items.Count - 1;

        foreach (ListViewItem item in selectedItems)
        {
            int index = item.Index;

            if (index == lastIndex)
            {
                continue;
            }

            _customReportListView.Items.RemoveAt(index);
            _customReportListView.Items.Insert(index + 1, item);
        }
    }

    private void OnSelectAllToolStripMenuItemClick(object sender, EventArgs e)
    {
        _listView.SelectAll();
    }

    private void OnCustomReportSelectAllToolStripMenuItemClick(object sender, EventArgs e)
    {
        _customReportListView.SelectAll();
    }

    private void OnCustomReportListViewAfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        if (e.Label != null)
        {
            IReportView view = (IReportView)_customReportListView.Items[e.Item].Tag!;

            view.Properties.Title = e.Label;
        }
    }

    private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
    {
        _customReportListView.Clear();
    }

    private async Task<bool> TryCancelAsync()
    {
        //switch (FormattedStrings.ShowCancelMessage(_company))
        //{
        //    case DialogResult.No:
        //        return false;

        //    case DialogResult.Cancel:
        //        return true;
        //}

        //return !await SaveAsync();

        return false;
    }

    private async Task ImportAsync(string path)
    {
        await Program.AbortRetryIgnoreAsync(async () =>
        {
            await using FileStream input = File.OpenRead(path);

            IReport[]? reports = await JsonSerializer.DeserializeAsync<IReport[]>(input, SerializationOptions.Json);

            if (reports == null)
            {
                throw new JsonException();
            }

            _customReportListView.BeginUpdate();
            _customReportListView.Items.Clear();

            try
            {
                foreach (IReport report in reports)
                {
                    _customReportListView.InsertReport(
                        _customReportListView.Items.Count,
                        _engine,
                        Guid.NewGuid().ToString(),
                        report.CreateView(_engine.Company));
                }

                _customReportListView.AutoResizeColumns();
            }
            finally
            {
                _customReportListView.EndUpdate();
            }

            _path = path;
        });
    }

    private async Task ExportAsync(string path)
    {
        SavingForm form = new SavingForm();

        form.Show();

        await Program.AbortRetryIgnoreAsync(async () =>
        {
            string extension = Path.GetExtension(path);

            switch (FilterIndexExtensions.FromExtension(extension))
            {
                case FilterIndex.Json:
                    {
                        await using FileStream output = File.Create(path);
                        IReport[] reports = new IReport[_customReportListView.Items.Count];

                        for (int i = 0; i < _customReportListView.Items.Count; i++)
                        {
                            reports[i] = ((IReportView)_customReportListView.Items[i].Tag!).Properties;
                        }

                        await JsonSerializer.SerializeAsync(output, reports, SerializationOptions.Json);

                        _path = path;
                    }
                    break;

                case FilterIndex.Pdf:
                    {
                        Print(path);
                    }
                    break;

                default:
                    FormattedStrings.ShowNotSupportedMessage(extension);
                    break;
            }
        });

        form.Close();
    }

    private async Task<bool> SaveAsAsync()
    {
        if (!TryGetSavePath(JsonFilterIndex, out string? path))
        {
            return false;
        }

        await ExportAsync(path);

        return true;
    }

    private async Task<bool> SaveAsync()
    {
        if (_path == null)
        {
            return await SaveAsAsync();
        }

        await ExportAsync(_path);

        return true;
    }

    private bool TryGetSavePath(int filterIndex, [MaybeNullWhen(false)] out string result)
    {
        _saveFileDialog.FilterIndex = filterIndex;

        if (_saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            result = null;

            return false;
        }

        result = _saveFileDialog.FileName;

        return true;
    }

    private async void OnOpenToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (await TryCancelAsync() || !TryGetOpenPath(JsonFilterIndex, out string? path))
        {
            return;
        }

        await ImportAsync(path);
    }

    public bool TryGetOpenPath(int filterIndex, [NotNullWhen(true)] out string? result)
    {
        _openFileDialog.FilterIndex = filterIndex + FilterOffset;

        if (_openFileDialog.ShowDialog() != DialogResult.OK)
        {
            result = null;

            return false;
        }

        result = _openFileDialog.FileName;

        return true;
    }

    private async void OnSaveAsToolStripMenuItemClick(object sender, EventArgs e)
    {
        await SaveAsAsync();
    }

    private async void OnSaveToolStripMenuItemClick(object sender, EventArgs e)
    {
        await SaveAsync();
    }

    private void OnPrintToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (TryGetSavePath(PdfFilterIndex, out string? path))
        {
            Print(path);
        }
    }

    private void Print(string path)
    {
        Queue<IReportView> queue = new Queue<IReportView>(Sections);
        SavingForm savingForm = new SavingForm()
        {
            MaxProgress = queue.Count
        };

        savingForm.Show();

        _webView.PrintQueue(path, queue, () =>
        {
            savingForm.Progress++;
        }, () =>
        {
            savingForm.Close();
            Process.Start(new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true
            });
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (ListViewItem item in _listView.Items)
            {
                if (item.Tag is IDisposable disposable)
                {
                    disposable.Dispose();

                    item.Tag = null;
                }
            }

            foreach (ListViewItem item in _customReportListView.Items)
            {
                if (item.Tag is IDisposable disposable)
                {
                    disposable.Dispose();

                    item.Tag = null;
                }
            }

            if (components != null)
            {
                components.Dispose();

                components = null;
            }
        }

        base.Dispose(disposing);
    }
}
