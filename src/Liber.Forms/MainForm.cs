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
using Liber.Forms.Forms;
using Liber.Forms.Help;
using Liber.Forms.Lines;
using Liber.Forms.LineSources;
using Liber.Forms.Properties;
using Liber.Forms.Reports;
using Liber.Forms.Reports.Gdi;
using Liber.Forms.Saving;
using Liber.Forms.TaskItems;
using Liber.Forms.Transactions;
using Liber.Forms.Writers;
using Liber.Sqlite;
using Liber.Writers;
using Microsoft.WindowsAPICodePack.Taskbar;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Liber.Forms;

internal partial class MainForm : Form
{
    private readonly JsonCompanyWriter _jsonWriter = new JsonCompanyWriter(FormattedStrings.JsonOptions);
    private readonly XmlReportWriter _xmlWriter = new XmlReportWriter();
    private readonly Dictionary<string, Guid> _nameKeys = new Dictionary<string, Guid>();

    private bool _pendingClose;
    private string? _path;
    private ReportEngine _engine;
    private Company _company;

    public event EventHandler? Loaded;

    public MainForm()
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);
        SetCompany(new Company());

        Text = SystemFeatures.ApplicationName;
        _factory.Parent = this;
    }

    public MainForm(string path) : this()
    {
        _recentPathManager.Add(path);
    }

    [MemberNotNull(nameof(_engine))]
    [MemberNotNull(nameof(_company))]
    private void SetCompany(Company value)
    {
        _engine = new ReportEngine(value);
        value.AccountRemoved += (_, e) => _factory.Kill(e.Id);

        foreach (Form child in MdiChildren)
        {
            child.Close();
        }

        _factory.KillAll();

        _company = value;
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        aboutToolStripMenuItem.Text = string.Format(aboutToolStripMenuItem.Text ?? "{0}", SystemFeatures.ApplicationName);
        exportCompanyJsonToolStripMenuItem.Tag = new Writer(FilterIndex.Json, _jsonWriter);
        exportCompanyXmlToolStripMenuItem.Tag = new Writer(FilterIndex.Xml, _xmlWriter);
        exportAccountsToolStripMenuItem.Tag = new Writer(FilterIndex.Csv, new GnuCashAccountWriter());
        exportTransactionsToolStripMenuItem.Tag = new Writer(FilterIndex.Csv, new GnuCashTransactionWriter());
        exportAccountsIifToolStripMenuItem.Tag = new Writer(FilterIndex.Iif, new IifAccountWriter());

        InitializeRecentPaths();
        InitializeReportEngine();

        if (!_recentPathManager.Empty)
        {
            await ImportAsync(_recentPathManager.Paths.First());
        }

        _factory.RegisterEmbedded(Guid.NewGuid(), new ReportsForm(_engine));
        Loaded?.Invoke(sender: this, EventArgs.Empty);
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

    private void InitializeReportEngine()
    {
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
        ReportsForm form = new ReportsForm(_engine)
        {
            Text = _engine.Views[name].GenericTitle
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
            SetCompany(form.Company);
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

        await ExportAsync(path, _company);

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

        SetCompany(company);
    }

    private async Task ImportSqliteCompanyAsync(string path)
    {
        if (await SqliteSerializer.CheckPasswordAsync(path, string.Empty))
        {
            SetCompany(await SqliteSerializer.DeserializeAsync(path, string.Empty));

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

        SetCompany(await SqliteSerializer.DeserializeAsync(path, password));
    }

    private async Task ImportGnuCashSqliteCompanyAsync(string path)
    {
        ImportRule[]? rules = JsonSerializer.Deserialize<ImportRule[]>(Settings.Default.ImportRules, FormattedStrings.JsonOptions);

        SetCompany(await GnuCashSqliteSerializer.DeserializeAsync(path, rules ?? Enumerable.Empty<ImportRule>()));
    }

    private async Task ImportAsync(string path)
    {
        await AbortRetryIgnoreAsync(async () =>
        {
            bool canSave = true;

            switch (FilterIndexExtensions.FromExtension(Path.GetExtension(path)))
            {
                case FilterIndex.Json:
                    await ImportJsonCompanyAsync(path);
                    break;

                case FilterIndex.GnuCashSqlite:
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

    private async Task ExportCompanyAsync(string path, Company company, IWriter writer)
    {
        await using FileStream output = File.Create(path);

        await writer.WriteAsync(output, company);

        _path = path;
    }

    private async Task ExportSqliteCompanyAsync(string path, Company company, IProgress progress)
    {
        await SqliteSerializer.SerializeAsync(path, company, progress);

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

    private async Task ExportAsync(string path, Company company)
    {
        SavingForm form = new SavingForm();
        SavingProgress progress = new SavingProgress(form, company);

        form.Show();

        await AbortRetryIgnoreAsync(async () =>
        {
            string extension = Path.GetExtension(path);

            switch (FilterIndexExtensions.FromExtension(extension))
            {
                case FilterIndex.Sqlite:
                case FilterIndex.Liber:
                    await ExportSqliteCompanyAsync(path, company, progress);
                    break;

                case FilterIndex.Json:
                    await ExportCompanyAsync(path, company, _jsonWriter);
                    break;

                case FilterIndex.Xml:
                    await ExportCompanyAsync(path, company, _xmlWriter);
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

        await ExportAsync(_path, _company);

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
        _factory.AutoRegister(() => new AccountsForm(_company, _factory, _engine));
    }

    private void OnNewAccountToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new NewAccountForm(_company));
    }

    private void OnReconcileAccountToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_company.Accounts.Count > 0)
        {
            AccountHelpers.BeginReconcile(
                _company,
                _factory,
                _company.Accounts.MaxBy(x => x.Reconciled)!.Id);
        }
    }

    private void OnUnreconcileToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_company.Accounts.Count < 1)
        {
            return;
        }

        Account account = _company.Accounts.MaxBy(x => x.Reconciled)!;

        if (FormattedStrings.ShowUnreconcileMessage(account) == DialogResult.OK)
        {
            _company.Unreconcile(account);
        }
    }

    private void OnRemoveAccountToolStripMenuItemClick(object sender, EventArgs e)
    {
        using AccountDialog form = new AccountDialog(new EditableAccountView(_company), validator: null);

        if (form.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        if (!_company.RemoveAccount(form.Value.Id))
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

            _factory.Register(Guid.NewGuid(), new ImportAccountsForm(_company, _factory, _engine, accounts));
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
        if (!_engine.TryGetReport(ReportEngine.CheckReport, out GdiCheckReport? report))
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
        _factory.Register(Guid.NewGuid(), new ReportsForm(_engine));
    }

    private void OnTransactionToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new TransactionForm(_company));
    }

    private void OnTaskItemsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new TaskItemsForm(_company, _factory));
    }

    private void OnAccountTransactionsToolStripMenuItemClick(object sender, EventArgs e)
    {
        using AccountDialog accountDialog = new AccountDialog(new EditableAccountView(_company), x => !x.ReadOnly);

        if (accountDialog.ShowDialog() != DialogResult.OK || accountDialog.Value.Value == null)
        {
            return;
        }

        AccountHelpers.BeginTransactions(_company, _factory, accountDialog.Value.Value);
    }

    private void OnNameTransactionsToolStripMenuItemClick(object sender, EventArgs e)
    {
        using NameDialog nameDialog = new NameDialog(_company);

        if (nameDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        if (!_nameKeys.TryGetValue(nameDialog.Value, out Guid key))
        {
            key = Guid.NewGuid();
            _nameKeys[nameDialog.Value] = key;
        }

        TransactionsForm form = new TransactionsForm(_company, new NameLineSource(_company, nameDialog.Value), _factory);

        _factory.Register(key, form);
    }

    private void OnCloseAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _factory.KillAll();
    }

    private void OnFormsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new FormsForm(_factory));
    }

    private void OnRecentPathManagerInvalidated(object sender, EventArgs e)
    {
        InitializeRecentPaths();
    }

    private async void OnAnonymizeToolStripMenuItemClick(object sender, EventArgs e)
    {
        Company clone = _company.Clone();
        Anonymizer anonymizer = new Anonymizer(clone);

        anonymizer.Anonymize();

        if (TryGetSavePath(FilterIndex.Liber, out string? path))
        {
            await ExportAsync(path, clone);
            SetCompany(clone);

            _path = path;
        }
    }

    private void OnFactoryInvalidated(object sender, EventArgs e)
    {
        formsToolStripSeparator.Visible = false;
        formsToolStripMenuItem1.Visible = false;

        formsToolStripMenuItem1.DropDownItems.Clear();

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

            formsToolStripMenuItem1.DropDownItems.Add(item);

            i++;
        }

        if (formsToolStripMenuItem1.DropDownItems.Count > 0)
        {
            formsToolStripSeparator.Visible = true;
            formsToolStripMenuItem1.Visible = true;
        }
    }

    private void OnContentsToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new UriForm(FormattedStrings.AboutUri));
    }

    private void OnIndexToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new UriForm(FormattedStrings.AboutUri));
    }

    private void OnSearchToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new UriForm(FormattedStrings.AboutUri));
    }

    private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new AboutBox(_factory));
    }

    private void OnSplashScreenToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new SplashScreen());
    }

    private void OnExitToolStripMenuItemClick(object sender, EventArgs e)
    {
        Close();
    }
}
