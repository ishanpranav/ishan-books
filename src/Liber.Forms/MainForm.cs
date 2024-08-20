// MainForm.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.Companies;
using Liber.Forms.Lines;
using Liber.Forms.Properties;
using Liber.Forms.Reports;
using Liber.Forms.Reports.Xsl;
using Liber.Forms.Transactions;
using Liber.Forms.Writers;
using Liber.Sqlite;
using Liber.Writers;
using MessagePack;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Liber.Forms;

internal sealed partial class MainForm : Form
{
    private static readonly MessagePackSerializerOptions s_messagePackOptions = MessagePackSerializerOptions.Standard
        .WithSecurity(MessagePackSecurity.UntrustedData);

    private readonly Company _company = new Company();
    private readonly ReportEngine _engine;

    private string? _path;

    public MainForm()
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        Text = SystemFeatures.ApplicationName;
        aboutToolStripMenuItem.Text = FormattedStrings.AboutText;
        _company.AccountRemoved += (sender, e) => _factory.Kill(e.Id);
        exportCompanyXmlToolStripMenuItem.Tag = new Writer(FilterIndex.Xml, new XmlReportWriter());
        exportAccountsToolStripMenuItem.Tag = new Writer(FilterIndex.Csv, new GnuCashAccountWriter());
        exportTransactionsToolStripMenuItem.Tag = new Writer(FilterIndex.Csv, new GnuCashTransactionWriter());
        exportAccountsIifToolStripMenuItem.Tag = new Writer(FilterIndex.Iif, new IifAccountWriter());
        _engine = new ReportEngine(_company);

        if (_engine.Views.Count == 0)
        {
            reportsToolStripMenuItem1.Visible = false;
        }

        foreach (KeyValuePair<string, IReportView> view in _engine.Views)
        {
            if (view.Value is not XslReportView)
            {
                continue;
            }

            ToolStripItem item = reportsToolStripMenuItem1.DropDownItems.Add(view.Value.Title);

            item.Click += (sender, e) =>
            {
                Guid key = Guid.NewGuid();
                ReportsForm form = new ReportsForm(_engine);

                form.Load += (sender, e) =>
                {
                    form.InitializeReport(view.Key);
                };

                _factory.Kill(key);
                _factory.Register(key, form);
            };
        }
    }

    public MainForm(string path) : this()
    {
        _recentPathManager.Add(path);
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        InitializeRecentPaths();

        if (!_recentPathManager.Empty)
        {
            await ImportAsync(_recentPathManager.Paths.First());
        }
    }

    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = MessageBox.Show(
            Resources.WarningText,
            Resources.CancelCaption,
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning) == DialogResult.Cancel;
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

    private void InitializeRecentPaths()
    {
        if (_recentPathManager.Empty)
        {
            return;
        }

        int i = 1;
        JumpList list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, Handle);
        JumpListCustomCategory companiesCategory = new JumpListCustomCategory(Resources.CompaniesJumpListCustomCategory);

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

            JumpListLink link = new JumpListLink(path, Path.GetFileName(path))
            {
                Arguments = "-1"
            };

            companiesCategory.AddJumpListItems(link);
        }

        recentPathsToolStripMenuItem.Visible = true;
        recentPathsToolStripSeparator.Visible = true;

        list.AddCustomCategories(companiesCategory);
        list.Refresh();
    }

    private async void OnNewToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (await TryCancelAsync())
        {
            return;
        }

        _path = null;

        CloseChildren();

        using NewCompanyForm form = new NewCompanyForm();

        if (form.ShowDialog() == DialogResult.OK)
        {
            form.Company.CopyTo(_company);
        }
    }

    private void CloseChildren()
    {
        foreach (Form child in MdiChildren)
        {
            child.Close();
        }
    }

    private async Task<bool> TryCancelAsync()
    {
        switch (MessageBox.Show(
            _company.GetCancelText(),
            Resources.CancelCaption,
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Warning))
        {
            case DialogResult.No:
                return false;

            case DialogResult.Cancel:
                return true;
        }

        await SaveAsync();

        return false;
    }

    private Task SaveAsAsync()
    {
        if (!TryGetSavePath(FilterIndex.Liber, out string? path))
        {
            return Task.CompletedTask;
        }

        return ExportAsync(path);
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

    public bool TryGetOpenPath(FilterIndex filterIndex, [MaybeNullWhen(false)] out string result)
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

    private async Task ImportMessagePackCompanyAsync(string path)
    {
        await using FileStream input = File.OpenRead(path);

        (await MessagePackSerializer.DeserializeAsync<Company>(input, s_messagePackOptions)).CopyTo(_company);
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
        using PasswordForm form = new PasswordForm();

        if (form.ShowDialog() == DialogResult.OK)
        {
            (await SqliteSerializer.DeserializeAsync(path, form.Password)).CopyTo(_company);
        }
    }

    private async Task ImportAsync(string path)
    {
        await AbortRetryIgnoreAsync(async () =>
        {
            switch (Path.GetExtension(path).ToUpperInvariant())
            {
                case ".JSON":
                    await ImportJsonCompanyAsync(path);
                    break;

                case ".MPK":
                case ".MSGPACK":
                    await ImportMessagePackCompanyAsync(path);
                    break;

                default:
                    await ImportSqliteCompanyAsync(path);
                    break;
            }

            CloseChildren();
            _recentPathManager.Add(path);

            _path = path;
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

    private async Task ExportMessagePackCompanyAsync(string path)
    {
        await using FileStream output = File.Create(path);

        await MessagePackSerializer.SerializeAsync(output, _company, s_messagePackOptions);

        _path = path;
    }

    private async Task ExportJsonCompanyAsync(string path)
    {
        await using FileStream output = File.Create(path);

        await JsonSerializer.SerializeAsync(output, _company, FormattedStrings.JsonOptions);

        _path = path;
    }

    private async Task ExportSqliteCompanyAsync(string path)
    {
        await SqliteSerializer.SerializeAsync(path, _company);

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
                    await ExportSqliteCompanyAsync(path);
                    break;

                case ".JSON":
                    await ExportJsonCompanyAsync(path);
                    break;

                case ".MPK":
                case ".MSGPACK":
                    await ExportMessagePackCompanyAsync(path);
                    break;

                default:
                    FormattedStrings.ShowNotSupportedMessage(extension);
                    break;
            }

            _recentPathManager.Add(path);
        });
    }

    private Task SaveAsync()
    {
        if (_path == null)
        {
            return SaveAsAsync();
        }

        return ExportAsync(_path);
    }

    private async void OnSaveToolStripMenuItemClick(object sender, EventArgs e)
    {
        await SaveAsync();
    }

    private void OnRecentPathManagerUpdated(object sender, EventArgs e)
    {
        recentPathsToolStripMenuItem.DropDownItems.Clear();

        InitializeRecentPaths();
    }

    private void OnEditToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new EditCompanyForm(_company));
    }

    private void OnAccountsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new AccountsForm(_company, _factory));
    }

    private void OnNewAccountToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new NewAccountForm(_company));
    }

    private void OnSettingsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new SettingsForm());
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

            _factory.Register(Guid.NewGuid(), new ImportAccountsForm(_company, _factory, accounts));
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
        using CheckDialog checkForm = new CheckDialog(new CheckView(_company));

        if (checkForm.ShowDialog() == DialogResult.OK)
        {
            Guid key = Guid.NewGuid();
            ReportsForm form = new ReportsForm(_engine);

            form.Load += (sender, e) =>
            {
                form.InitializeCheck(checkForm.Value);
            };

            _factory.Kill(key);
            _factory.Register(key, form);
        }
    }

    private void OnReportsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.Register(Guid.NewGuid(), new ReportsForm(_engine));
    }

    private void OnTransactionToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new TransactionForm(_company));
    }

    private void OnTransactionsToolStripMenuItemClick(object sender, EventArgs e)
    {
        using AccountDialog accountDialog = new AccountDialog(new EditableAccountView(_company));

        if (accountDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        Guid id = accountDialog.Value.Id;

        if (_factory.TryKill(id))
        {
            return;
        }

        TransactionsForm transactionsForm = new TransactionsForm(_company, id);

        _factory.Register(id, transactionsForm);
    }
}
