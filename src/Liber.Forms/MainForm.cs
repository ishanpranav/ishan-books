// MainForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.AccountViews;
using Liber.Forms.Companies;
using Liber.Forms.Lines;
using Liber.Forms.Properties;
using Liber.Forms.Reports;
using Liber.Forms.Reports.Gdi;
using Liber.Forms.Saving;
using Liber.Forms.Transactions;
using Liber.Forms.Writers;
using Liber.Sqlite;
using Liber.Writers;
using Microsoft.WindowsAPICodePack.Taskbar;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Liber.Forms;

internal sealed partial class MainForm : Form
{
    private readonly Company _company = new Company();

    private ReportEngine? _engine;
    private IntervalView? _favoriteReport;
    private bool _isFavoriteTemporary;
    private bool _pendingClose;
    private string? _path;

    public MainForm()
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        Text = SystemFeatures.ApplicationName;
        aboutToolStripMenuItem.Text = FormattedStrings.AboutText;
        exportCompanyXmlToolStripMenuItem.Tag = new Writer(FilterIndex.Xml, new XmlReportWriter());
        exportAccountsToolStripMenuItem.Tag = new Writer(FilterIndex.Csv, new GnuCashAccountWriter());
        exportTransactionsToolStripMenuItem.Tag = new Writer(FilterIndex.Csv, new GnuCashTransactionWriter());
        exportAccountsIifToolStripMenuItem.Tag = new Writer(FilterIndex.Iif, new IifAccountWriter());
        _company.Invalidated += (_, e) =>
        {
            foreach (Form child in MdiChildren)
            {
                child.Close();
            }

            _factory.KillAll();
        };
        _company.AccountRemoved += (_, e) => _factory.Kill(e.Id);
    }

    public MainForm(string path) : this()
    {
        _recentPathManager.Add(path);
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        InitializeRecentPaths();
        InitializeReportEngine();

        if (!_recentPathManager.Empty)
        {
            await ImportAsync(_recentPathManager.Paths.First());
        }

        if (_engine.TryGetReport(ReportEngine.AccountMapReport, out IntervalView? report))
        {
            report.Title = _company.DisplayName;
            report.Started = _company.Started;
            report.Posted = _company.Posted;
            _favoriteReport = report;
            _company.NameChanged += (_, _) =>
            {
                report.Title = _company.DisplayName;
            };

            ReportsForm form = new ReportsForm(_engine);

            form.Load += (_, _) =>
            {
                RefreshFavoriteReport();
                _timer.Start();
            };
            form.Edited += OnReportsFormEdited;
            form.FormClosed += OnReportsFormEdited;

            _factory.RegisterEmbedded(Guid.NewGuid(), parent: this, form);
        }
    }

    private void OnReportsFormEdited(object? sender, EventArgs e)
    {
        _timer.Stop();

        _favoriteReport = null;
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        RefreshFavoriteReport();
    }

    private void RefreshFavoriteReport()
    {
        if (_favoriteReport == null || _factory.Embedded is not ReportsForm form)
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
            }

            return;
        }

        _favoriteReport.Accounts = new AccountsView(
            _company,
            _company.Accounts
                .Where(x => _isFavoriteTemporary ? !x.Type.IsTemporary() : x.Type.IsTemporary())
                .ToHashSet());
        _isFavoriteTemporary = !_isFavoriteTemporary;

        form.InitializeReport(ReportEngine.AccountMapReport);
    }

    private void InitializeRecentPaths()
    {
        recentPathsToolStripMenuItem.Visible = false;
        recentPathsToolStripSeparator.Visible = false;

        if (_recentPathManager.Empty)
        {
            return;
        }

        recentPathsToolStripMenuItem.DropDownItems.Clear();

        int i = 1;

        foreach (string path in _recentPathManager.Paths)
        {
            if (!File.Exists(path))
            {
                continue;
            }

            ToolStripItem item = recentPathsToolStripMenuItem.DropDownItems.Add($"{i} - {path}");

            item.Click += async (_, _) => await ImportAsync(path);
            i++;

            string? directoryName = Path.GetDirectoryName(path);

            if (directoryName != null)
            {
                FileDialogCustomPlace customPlace = new FileDialogCustomPlace(directoryName);

                _openFileDialog.CustomPlaces.Add(customPlace);
                _saveFileDialog.CustomPlaces.Add(customPlace);
            }
        }

        recentPathsToolStripMenuItem.Visible = true;
        recentPathsToolStripSeparator.Visible = true;

        try
        {
            JumpList list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, Handle);
            JumpListCustomCategory companiesCategory = new JumpListCustomCategory(Resources.CompaniesJumpListCustomCategory);

            foreach (string path in _recentPathManager.Paths)
            {
                if (!File.Exists(path))
                {
                    continue;
                }

                JumpListLink link = new JumpListLink(path, Path.GetFileName(path))
                {
                    Arguments = "-1"
                };

                companiesCategory.AddJumpListItems(link);
            }

            list.AddCustomCategories(companiesCategory);
            list.Refresh();
        }
        catch (UnauthorizedAccessException) { }
        catch (ObjectDisposedException) { }
    }

    [MemberNotNull(nameof(_engine))]
    private void InitializeReportEngine()
    {
        _engine = new ReportEngine(_company);

        if (_engine.Views.Count == 0)
        {
            reportsToolStripMenuItem1.Visible = false;
        }

        Dictionary<string, ToolStripMenuItem> groups = new Dictionary<string, ToolStripMenuItem>();
        Dictionary<(int SortOrder, string Key), List<(string Name, string GenericTitle)>> pending = new Dictionary<(int, string), List<(string, string)>>();

        foreach (KeyValuePair<string, IReportView> view in _engine.Views)
        {
            string genericTitle = view.Value.GenericTitle;
            string baseTitle;
            int open = genericTitle.IndexOf('(');
            int close = genericTitle.LastIndexOf(')');

            if (open >= 0 && close > open)
            {
                baseTitle = genericTitle.Substring(0, open).TrimEnd();
            }
            else
            {
                baseTitle = genericTitle;
            }

            int type = view.Value.SortOrder;

            if (!pending.TryGetValue((type, baseTitle), out List<(string, string)>? items))
            {
                items = new List<(string, string)>();
                pending[(type, baseTitle)] = items;
            }

            items.Add((view.Key, genericTitle));
        }

        List<IGrouping<int, KeyValuePair<(int, string), List<(string, string)>>>> typeGroups = pending
            .GroupBy(x => x.Key.SortOrder)
            .OrderBy(x => x.Key)
            .ToList();

        for (int i = 0; i < typeGroups.Count; i++)
        {
            ToolStripMenuItem grandparent = i == 0
                ? reportsToolStripMenuItem1
                : otherReportsToolStripMenuItem;

            foreach (KeyValuePair<(int, string Key), List<(string Name, string GenericTitle)>> group in typeGroups[i])
            {
                if (group.Value.Count == 1)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(group.Value[0].GenericTitle)
                    {
                        Tag = group.Value[0].Name
                    };

                    item.Click += OnReportToolStripMenuItemClick;
                    grandparent.DropDownItems.Add(item);

                    continue;
                }

                ToolStripMenuItem parent = new ToolStripMenuItem(group.Key.Key);

                grandparent.DropDownItems.Add(parent);

                foreach ((string key, string fullTitle) in group.Value.OrderBy(x => x.GenericTitle))
                {
                    ToolStripMenuItem child = new ToolStripMenuItem(fullTitle)
                    {
                        Tag = key
                    };

                    child.Click += OnReportToolStripMenuItemClick;
                    parent.DropDownItems.Add(child);
                }
            }

            if (i < typeGroups.Count - 1)
            {
                grandparent.DropDownItems.Add(new ToolStripSeparator());
            }
        }

        reportsToolStripMenuItem1.DropDownItems.Add(otherReportsToolStripMenuItem);
        reportsToolStripMenuItem1.DropDownItems.RemoveAt(0);
    }

    private void OnReportToolStripMenuItemClick(object? sender, EventArgs e)
    {
        string name = ((ToolStripMenuItem)sender!).Tag!.ToString()!;
        Guid key = Guid.NewGuid();
        ReportsForm form = new ReportsForm(_engine!)
        {
            Text = _engine!.Views[name].GenericTitle
        };

        form.Load += (sender, e) =>
        {
            form.InitializeReport(name);
        };

        _factory.Kill(key);
        _factory.Register(key, form);
    }

    private async void OnFormClosing(object sender, FormClosingEventArgs e)
    {
        if (_pendingClose)
        {
            return;
        }

        e.Cancel = true;

        if (await TryCancelAsync())
        {
            return;
        }

        _pendingClose = true;

        Close();
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

    private async void OnNewToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (await TryCancelAsync())
        {
            return;
        }

        _path = null;

        using NewCompanyForm form = new NewCompanyForm();

        if (form.ShowDialog() == DialogResult.OK)
        {
            form.Company.CopyTo(_company);
        }
    }

    private async Task<bool> TryCancelAsync()
    {
        switch (FormattedStrings.ShowCancelMessage(_company))
        {
            case DialogResult.No:
                return false;

            case DialogResult.Cancel:
                return true;
        }

        return !await SaveAsync();
    }

    private async Task<bool> SaveAsAsync()
    {
        if (!TryGetSavePath(FilterIndex.Liber, out string? path))
        {
            return false;
        }

        await ExportAsync(path);

        return true;
    }

    private bool TryGetSavePath(FilterIndex filterIndex, [MaybeNullWhen(false)] out string result)
    {
        _saveFileDialog.FilterIndex = (int)filterIndex + (int)FilterIndex.Offset;
        _saveFileDialog.FileName = _company.Name;

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
        if (await TryCancelAsync() || !TryGetOpenPath(FilterIndex.AllSupportedFiles, out string? path))
        {
            return;
        }

        await ImportAsync(path);
    }

    public bool TryGetOpenPath(FilterIndex filterIndex, [NotNullWhen(true)] out string? result)
    {
        _openFileDialog.FilterIndex = (int)filterIndex;

        if (_openFileDialog.ShowDialog() != DialogResult.OK)
        {
            result = null;

            return false;
        }

        result = _openFileDialog.FileName;

        return true;
    }

    public bool TryGetOpenPaths(FilterIndex filterIndex, [MaybeNullWhen(false)] out string[] result)
    {
        _openFileDialog.FilterIndex = (int)filterIndex;
        _openFileDialog.Multiselect = true;

        if (_openFileDialog.ShowDialog() != DialogResult.OK)
        {
            result = null;

            return false;
        }

        result = _openFileDialog.FileNames;

        Array.Sort(result);

        return true;
    }

    private async Task ImportJsonCompanyAsync(string path)
    {
        await using FileStream input = File.OpenRead(path);

        Company? company = await JsonSerializer.DeserializeAsync<Company>(input, FormattedStrings.JsonOptions);

        if (company == null)
        {
            throw new JsonException();
        }

        company.CopyTo(_company);
    }

    private async Task ImportSqliteCompanyAsync(string path)
    {
        if (await SqliteSerializer.CheckPasswordAsync(path, string.Empty))
        {
            (await SqliteSerializer.DeserializeAsync(path, string.Empty)).CopyTo(_company);

            return;
        }

        DialogResult result;
        string password;

        do
        {
            using PasswordForm form = new PasswordForm();

            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            password = form.Password;

            if (await SqliteSerializer.CheckPasswordAsync(path, password))
            {
                break;
            }

            result = MessageBox.Show(
                Resources.IncorrectPasswordError,
                Resources.ExceptionCaption,
                MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Abort)
            {
                return;
            }
        }
        while (result == DialogResult.Retry);

        (await SqliteSerializer.DeserializeAsync(path, password)).CopyTo(_company);
    }

    private async Task ImportGnuCashSqliteCompanyAsync(string path)
    {
        ImportRule[]? rules = JsonSerializer.Deserialize<ImportRule[]>(Settings.Default.ImportRules, FormattedStrings.JsonOptions);

        (await GnuCashSqliteSerializer.DeserializeAsync(path, rules ?? Enumerable.Empty<ImportRule>())).CopyTo(_company);
    }

    private async Task ImportAsync(string path)
    {
        await AbortRetryIgnoreAsync(async () =>
        {
            bool canSave = true;

            switch (Path.GetExtension(path).ToUpperInvariant())
            {
                case ".JSON":
                    await ImportJsonCompanyAsync(path);
                    break;

                case ".GNUCASH":
                    await ImportGnuCashSqliteCompanyAsync(path);

                    canSave = false;
                    break;

                default:
                    await ImportSqliteCompanyAsync(path);
                    break;
            }

            _recentPathManager.Add(path);

            _path = canSave ? path : null;
        });
    }

    private async void OnSaveAsToolStripMenuItemClick(object sender, EventArgs e)
    {
        await SaveAsAsync();
    }

    private void OnExitToolStripMenuItemClick(object sender, EventArgs e)
    {
        Close();
    }

    private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new UrlForm(FormattedStrings.AboutUrl));
    }

    private async Task ExportJsonCompanyAsync(string path)
    {
        await using FileStream output = File.Create(path);

        await JsonSerializer.SerializeAsync(output, _company, FormattedStrings.JsonOptions);

        _path = path;
    }

    private async Task ExportSqliteCompanyAsync(string path, IProgress progress)
    {
        await SqliteSerializer.SerializeAsync(path, _company, progress);

        _path = path;
    }

    private static async Task AbortRetryIgnoreAsync(Func<Task> action)
    {
        DialogResult result;

        do
        {
            try
            {
                await action();

                result = DialogResult.OK;
            }
            catch (Exception exception)
            {
                result = MessageBox.Show(exception.Message, Resources.ExceptionCaption, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
        }
        while (result == DialogResult.Retry);

    }

    private async Task ExportAsync(string path)
    {
        SavingForm form = new SavingForm();
        SavingProgress progress = new SavingProgress(form, _company);

        form.Show();

        await AbortRetryIgnoreAsync(async () =>
        {
            string extension = Path.GetExtension(path);

            switch (extension.ToUpperInvariant())
            {
                case ".IZBK":
                case ".SQLITE":
                case ".SQLITE3":
                case ".DB":
                case ".DB3":
                case ".S3DB":
                case ".SL3":
                    await ExportSqliteCompanyAsync(path, progress);
                    break;

                case ".JSON":
                    await ExportJsonCompanyAsync(path);
                    break;

                default:
                    FormattedStrings.ShowNotSupportedMessage(extension);
                    break;
            }

            _recentPathManager.Add(path);
        });

        form.Close();
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

    private async void OnSaveToolStripMenuItemClick(object sender, EventArgs e)
    {
        await SaveAsync();
    }

    private void OnEditToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new EditCompanyForm(_company));
    }

    private void OnAccountsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new AccountsForm(_company, _factory, _engine!));
    }

    private void OnNewAccountToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new NewAccountForm(_company));
    }

    private void OnRemoveAccountToolStripMenuItemClick(object sender, EventArgs e)
    {
        using AccountDialog accountDialog = new AccountDialog(new EditableAccountView(_company), validator: null);

        if (accountDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        if (!_company.RemoveAccount(accountDialog.Value.Id))
        {
            FormattedStrings.ShowDeleteAccountMessage();
        }
    }

    private void OnCultureToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new CultureForm());
    }

    private void OnImportSettingsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new ImportRulesForm());
    }

    private async void OnCombinePdfDocumentsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetOpenPaths(FilterIndex.Pdf, out string[]? inputPaths) ||
            !TryGetSavePath(FilterIndex.Pdf, out string? outputPath))
        {
            return;
        }

        SavingForm form = new SavingForm();

        form.Show();

        await AbortRetryIgnoreAsync(async () =>
        {
            using PdfDocument document = new PdfDocument();

            PdfOutlineCollection outline = document.Outlines;
            int i = 0;

            foreach (string inputPath in inputPaths)
            {
                using PdfDocument inputPdfDocument = PdfReader.Open(inputPath, PdfDocumentOpenMode.Import);

                form.MaxProgress += inputPdfDocument.PageCount;
                document.Version = Math.Max(document.Version, inputPdfDocument.Version);

                int j = i;

                foreach (PdfPage page in inputPdfDocument.Pages)
                {
                    document.AddPage(page);

                    i++;
                    form.Progress++;
                }

                string chapterTitle = Path.GetFileNameWithoutExtension(inputPath);
                const string delimiter = " - ";
                int index = chapterTitle.IndexOf(delimiter);

                if (index >= 0)
                {
                    chapterTitle = chapterTitle.Substring(index + delimiter.Length);
                }

                outline.Add(chapterTitle, document.Pages[j], opened: true);
            }

            document.Save(outputPath);
            Process.Start(new ProcessStartInfo()
            {
                FileName = outputPath,
                UseShellExecute = true
            });
        });

        form.Close();
    }

    private async void OnImportAccountsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetOpenPath(FilterIndex.Csv, out string? path))
        {
            return;
        }

        await AbortRetryIgnoreAsync(async () =>
        {
            await using FileStream input = File.OpenRead(path);

            IReadOnlyCollection<GnuCashAccount> accounts = await GnuCashSerializer.DeserializeAsync<GnuCashAccount>(input);

            _factory.Register(Guid.NewGuid(), new ImportAccountsForm(_company, _factory, _engine!, accounts));
        });
    }

    private async void OnImportTransactionsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetOpenPath(FilterIndex.Csv, out string? path))
        {
            return;
        }

        await AbortRetryIgnoreAsync(async () =>
        {
            await using FileStream input = File.OpenRead(path);

            IReadOnlyCollection<GnuCashLine> lines = await GnuCashSerializer.DeserializeAsync<GnuCashLine>(input);

            _factory.Register(Guid.NewGuid(), new ImportTransactionsForm(_company, lines));
        });
    }

    private async void OnExportToolStripMenuItemClick(object sender, EventArgs e)
    {
        Writer writer = (Writer)((ToolStripMenuItem)sender).Tag!;

        if (!TryGetSavePath(writer.Index, out string? path))
        {
            return;
        }

        await AbortRetryIgnoreAsync(async () =>
        {
            await using FileStream output = File.Create(path);

            await writer.Value.WriteAsync(output, _company);
        });
    }

    private void OnCheckToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!_engine!.TryGetReport(ReportEngine.CheckReport, out GdiCheckReport? report))
        {
            return;
        }

        using CheckDialog checkForm = new CheckDialog(new CheckView(_company));

        if (checkForm.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        report.Check = checkForm.Value;

        Guid key = Guid.NewGuid();
        ReportsForm form = new ReportsForm(_engine)
        {
            Text = checkToolStripMenuItem.Text
        };

        form.Load += (sender, e) => form.InitializeReport(ReportEngine.CheckReport);

        _factory.Kill(key);
        _factory.Register(key, form);
    }

    private void OnReportsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.Register(Guid.NewGuid(), new ReportsForm(_engine!));
    }

    private void OnTransactionToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new TransactionForm(_company));
    }

    private void OnTransactionsToolStripMenuItemClick(object sender, EventArgs e)
    {
        using AccountDialog accountDialog = new AccountDialog(new EditableAccountView(_company), x => !x.ReadOnly);

        if (accountDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        Guid id = accountDialog.Value.Id;

        if (_factory.TryActivate(id) || accountDialog.Value.Value == null)
        {
            return;
        }

        TransactionsForm form = new TransactionsForm(_company, accountDialog.Value.Value, _factory);

        _factory.Register(id, form);
    }

    private void OnCloseAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _factory.KillAll();
    }

    private void OnRecentPathManagerInvalidated(object sender, EventArgs e)
    {
        InitializeRecentPaths();
    }

    private void OnFactoryInvalidated(object sender, EventArgs e)
    {
        formsToolStripSeparator.Visible = false;
        formsToolStripMenuItem.Visible = false;

        formsToolStripMenuItem.DropDownItems.Clear();

        int i = 1;

        foreach (KeyValuePair<Guid, Form> entry in _factory.Forms)
        {
            if (entry.Value == _factory.Embedded)
            {
                continue;
            }

            ToolStripItem item = recentPathsToolStripMenuItem.DropDownItems.Add($"{i} - {entry.Value.Text}");

            item.Click += async (_, _) =>
            {
                _factory.TryActivate(entry.Key);
            };
            entry.Value.TextChanged += async (_, _) =>
            {
                item.Text = $"{i} - {entry.Value.Text}";
            };

            formsToolStripMenuItem.DropDownItems.Add(item);

            i++;
        }

        if (formsToolStripMenuItem.DropDownItems.Count > 0)
        {
            formsToolStripSeparator.Visible = true;
            formsToolStripMenuItem.Visible = true;
        }
    }
}
