using Liber.Forms.Accounts;
using Liber.Forms.Companies;
using Liber.Forms.Properties;
using Liber.GnuCash;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liber.Forms;

internal sealed partial class MainForm : Form
{
    private static readonly JsonSerializerOptions s_jsonOptions = new JsonSerializerOptions()
    {
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    private static readonly MessagePackSerializerOptions s_messagePackOptions = MessagePackSerializerOptions.Standard
        .WithSecurity(MessagePackSecurity.UntrustedData);
    private readonly Company _company = new Company();
    private string? _path;

    static MainForm()
    {
        s_jsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
        s_jsonOptions.Converters.Add(new TypeConverterJsonConverterAdapter());
    }

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

    private async void OnFormClosing(object sender, FormClosingEventArgs e)
    {
        if (await TryCancelAsync())
        {
            e.Cancel = true;
        }
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
            ToolStripItem item = recentPathsToolStripMenuItem.DropDownItems.Add($"{i} - {path}");

            item.Click += async (_, _) => await ImportAsync(path);
            i++;
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
        if (!TryGetSavePath(filterIndex: 1, out string? path))
        {
            return Task.CompletedTask;
        }

        return ExportAsync(path);
    }

    private bool TryGetSavePath(int filterIndex, [MaybeNullWhen(false)] out string result)
    {
        _saveFileDialog.FilterIndex = filterIndex;
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
        if (await TryCancelAsync() || !TryGetOpenPath(filterIndex: 2, out string? path))
        {
            return;
        }

        await ImportAsync(path);
    }

    public bool TryGetOpenPath(int filterIndex, [MaybeNullWhen(false)] out string result)
    {
        _openFileDialog.FilterIndex = filterIndex;

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

        Company? company = await JsonSerializer.DeserializeAsync<Company>(input, s_jsonOptions);

        if (company == null)
        {
            throw new JsonException();
        }

        company.CopyTo(_company);
    }

    private async Task ImportAsync(string path)
    {
        DialogResult result;

        do
        {
            try
            {
                switch (Path.GetExtension(path).ToLower())
                {
                    case ".json":
                        await ImportJsonCompanyAsync(path);
                        break;

                    case ".liber":
                        await ImportCompanyAsync(path);
                        break;
                }

                CloseChildren();
                _recentPathManager.Add(path);

                _path = path;
                result = DialogResult.OK;
            }
            catch (Exception exception)
            {
                result = MessageBox.Show(exception.Message, Resources.ExceptionCaption, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
        }
        while (result == DialogResult.Retry);
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

        await JsonSerializer.SerializeAsync(output, _company, s_jsonOptions);

        _path = path;
    }

    private async Task ExportAsync(string path)
    {
        DialogResult result;

        do
        {
            try
            {
                switch (Path.GetExtension(path).ToLower())
                {
                    case ".json":
                        await ExportJsonCompanyAsync(path);
                        break;

                    case ".liber":
                        await ExportCompanyAsync(path);
                        break;
                }

                _recentPathManager.Add(path);

                result = DialogResult.OK;
            }
            catch (Exception exception)
            {
                result = MessageBox.Show(exception.Message, Resources.ExceptionCaption, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
        }
        while (result == DialogResult.Retry);
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
        if (!TryGetOpenPath(filterIndex: 5, out string? path))
        {
            return;
        }

        await using FileStream input = File.OpenRead(path);

        IReadOnlyCollection<Account> accounts = await GnuCashSerializer.DeserializeAccountsAsync(input);

        _factory.Register(Guid.NewGuid(), new ImportAccountsForm(_company, accounts));
    }

    private void OnSettingsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new SettingsForm());
    }

    private async void OnExportAccountsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetSavePath(filterIndex: 3, out string? path))
        {
            return;
        }

        await using FileStream output = File.Create(path);

        await GnuCashSerializer.SerializeAccountsAsync(output, _company.Accounts.Values);
    }
}
