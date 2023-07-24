// MainForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Liber.Forms.Accounts;
using Liber.Forms.Companies;
using Liber.Forms.Properties;
using Liber.Forms.Transactions;
using Liber.Sqlite;
using MessagePack;

namespace Liber.Forms;

internal sealed partial class MainForm : Form
{
    private static readonly MessagePackSerializerOptions s_messagePackOptions = MessagePackSerializerOptions.Standard
        .WithSecurity(MessagePackSecurity.UntrustedData);

    private readonly Company _company = new Company();

    private string? _path;

    public MainForm(string[] args)
    {
        InitializeComponent();

        if (args.Length > 0)
        {
            _recentPathManager.Add(args[0]);
        }
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
    }

    private async void OnNewToolStripMenuItemClick(object sender, EventArgs e)
    {
        Guid key = new Guid("3441FF73-E251-4AC0-972C-7584E9481EDF");

        if (_factory.TryKill(key))
        {
            return;
        }

        if (await TryCancelAsync())
        {
            return;
        }

        _path = null;

        CloseChildren();

        NewCompanyForm form = new NewCompanyForm();

        form.FormClosed += (_, _) =>
        {
            if (form.DialogResult == DialogResult.OK)
            {
                form.Company.CopyTo(_company);
            }
        };

        _factory.Register(key, form);
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
        if (!TryGetSavePath(FilterIndex.MessagePack, out string? path))
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

    private async Task ImportCompanyAsync(string path)
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
                case ".SQLITE":
                case ".SQLITE3":
                case ".DB":
                case ".DB3":
                case ".S3DB":
                case ".SL3":
                    await ImportSqliteCompanyAsync(path);
                    break;

                case ".JSON":
                    await ImportJsonCompanyAsync(path);
                    break;

                case ".SHBK":
                    await ImportCompanyAsync(path);
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
        _factory.AutoRegister(() => new UrlForm(FormattedStrings.GetHelpUrl()));
    }

    private async Task ExportCompanyAsync(string path)
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
            switch (Path.GetExtension(path).ToUpperInvariant())
            {
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

                case ".SHBK":
                    await ExportCompanyAsync(path);
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

            _factory.Register(Guid.NewGuid(), new ImportTransactionsForm(_company, _factory, lines));
        });
    }

    private void OnSettingsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new SettingsForm());
    }

    private async void OnExportAccountsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetSavePath(FilterIndex.Csv, out string? path))
        {
            return;
        }

        await AbortRetryIgnoreAsync(async () =>
        {
            await using FileStream output = File.Create(path);

            await GnuCashSerializer.SerializeAccountsAsync(output, _company);
        });
    }

    private async void OnExportTransactionsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetSavePath(FilterIndex.Csv, out string? path))
        {
            return;
        }

        await AbortRetryIgnoreAsync(async () =>
        {
            await using FileStream output = File.Create(path);

            await GnuCashSerializer.SerializeTransactionsAsync(output, _company);
        });
    }

    private async void OnExportCompanyToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetSavePath(FilterIndex.Xml, out string? path))
        {
            return;
        }

        await AbortRetryIgnoreAsync(() =>
        {
            using XmlWriter writer = XmlWriter.Create(path);

            XmlSerializers.Company.Serialize(writer, _company);

            return Task.CompletedTask;
        });
    }

    private void OnReportsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new ReportsForm(_company));
    }

    private void OnTransactionToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new TransactionForm(_company));
    }
}
