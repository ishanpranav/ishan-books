// AccountsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using Humanizer;
using Liber.Forms.Components;
using Liber.Forms.Properties;
using Liber.Forms.Reports;
using Liber.Forms.Transactions;

namespace Liber.Forms.Accounts;

internal sealed partial class AccountsForm : Form
{
    private readonly Company _company;
    private readonly FormFactory _factory;
    private readonly Dictionary<Guid, ListViewItem> _items = new Dictionary<Guid, ListViewItem>();
    private readonly Dictionary<Color, int> _imageIndices = new Dictionary<Color, int>();
    private readonly ReportEngine _engine;

    public AccountsForm(Company company, FormFactory factory, ReportEngine engine)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);
        Design.ApplyStyles(_contextMenu);

        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
        _company = company;
        _factory = factory;
        _engine = engine;
        inactiveToolStripMenuItem.Checked = Settings.Default.Inactive;

        InitializeAccounts();
    }

    private void InitializeAccounts()
    {
        _items.Clear();
        _listView.BeginUpdate();

        try
        {
            _listView.Items.Clear();

            foreach (Account account in _company.Accounts)
            {
                InitializeAccount(account);
            }

            _listView.AutoResizeColumns();
            _listView.Sort();
        }
        finally
        {
            _listView.EndUpdate();
        }
    }

    private void InitializeAccount(Account value)
    {
        if (value.Inactive && !inactiveToolStripMenuItem.Checked)
        {
            return;
        }

        ListViewItem item = _listView.Items.Add(new ListViewItem()
        {
            Tag = value
        });

        AddSubItems(item, value);
        _items.Add(value.Id, item);
    }

    private void InitializeTransaction(Account? account)
    {
        Guid key = typeof(TransactionForm).GUID;

        if (_factory.TryActivate(key))
        {
            return;
        }

        if (account != null && account.ReadOnly)
        {
            account = null;
        }

        TransactionForm form = new TransactionForm(_company);
        DateTime lastPosted = Settings.Default.LastPosted;
        Line[] lines = account == null ? Array.Empty<Line>() : new Line[]
        {
            new Line()
            {
                AccountId = account.Id
            }
        };
        Transaction transaction = new Transaction(Guid.Empty, name: null, lines)
        {
            Posted = lastPosted == default ? DateTime.Today : lastPosted,
            Number = _company.NextTransactionNumber
        };

        form.InitializeTransaction(transaction);
        _factory.Register(key, form);
    }

    private void InitializeTransactions(Account account)
    {
        if (_factory.TryActivate(account.Id) || account.ReadOnly)
        {
            return;
        }

        TransactionsForm form = new TransactionsForm(_company, account);

        _factory.Register(account.Id, form);
    }

    private void AddSubItems(ListViewItem item, Account value)
    {
        item.Text = value.Name;

        decimal balance = value.Type.ToBalance(_company.GetBalance(value, Filters.Any()));
        Color color = _company.GetColorOrDefault(value);

        if (!_imageIndices.TryGetValue(color, out int imageIndex))
        {
            imageIndex = _imageList.Images.Count;
            _imageIndices[color] = imageIndex;

            _imageList.Images.Add(CreateColorImage(color));
        }

        item.ImageIndex = imageIndex;

        item.SubItems.Add(value.Number.ToString()).Tag = value.Number;
        item.SubItems.Add(value.Type.Humanize());
        item.SubItems.Add(value.CashFlow.Humanize());
        item.SubItems.Add(FormattedStrings.GetTaxTypeText(value.TaxType));
        item.SubItems.Add(balance.ToLocalizedString()).Tag = balance;

        if (value.ReadOnly)
        {
            item.ForeColor = Colors.Gray;
        }
    }

    private static Bitmap CreateColorImage(Color color)
    {
        Bitmap result = new Bitmap(16, 16);

        using Graphics graphics = Graphics.FromImage(result);

        graphics.Clear(Color.Transparent);

        using SolidBrush solidBrush = new SolidBrush(color);
        using Pen pen = new Pen(Colors.Gray);

        graphics.FillRectangle(solidBrush, x: 3, y: 3, width: 10, height: 10);
        graphics.DrawRectangle(pen, x: 3, y: 3, width: 9, height: 9);

        return result;
    }

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        InitializeAccount(_company.GetAccount(e.Id));
        _listView.AutoResizeColumns();
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        ListViewItem item = _items[e.Id];

        item.SubItems.Clear();
        AddSubItems(item, _company.GetAccount(e.Id));
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        ListViewItem item = _items[e.Id];

        _listView.Items.Remove(item);
        _listView.AutoResizeColumns();
    }

    public bool TryGetSelection([NotNullWhen(true)] out Account? value)
    {
        if (_listView.SelectedItems.Count == 0)
        {
            value = default;

            return false;
        }

        value = (Account)_listView.SelectedItems[0].Tag!;

        return true;
    }

    private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (TryGetSelection(out Account? account))
        {
            _factory.AutoRegister(() => new NewAccountForm(_company, account.Id));
        }
        else
        {
            _factory.AutoRegister(() => new NewAccountForm(_company));
        }
    }

    private void OnEditToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetSelection(out Account? account) || _factory.TryActivate(account.Id))
        {
            return;
        }

        EditAccountForm form = new EditAccountForm(_company, account);

        _factory.Register(account.Id, form);
    }

    private void OnRenameToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_listView.SelectedItems.Count == 0)
        {
            return;
        }

        _listView.SelectedItems[0].BeginEdit();
    }

    private void OnRemoveToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetSelection(out Account? account))
        {
            return;
        }

        if (!_company.RemoveAccount(account.Id))
        {
            // TODO: show remove account failed
        }
    }

    private void OnTransactionToolStripMenuItemClick(object sender, EventArgs e)
    {
        TryGetSelection(out Account? account);
        InitializeTransaction(account);
    }

    private void OnTransactionsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (TryGetSelection(out Account? account))
        {
            InitializeTransactions(account);
        }
    }

    private void OnListViewAfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        Account account = (Account)_listView.Items[e.Item].Tag!;

        account.Name = e.Label!;

        _company.UpdateAccount(account.Id, account.ParentId);
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        if (!TryGetSelection(out Account? account))
        {
            return;
        }

        if (account.ReadOnly)
        {
            QuickReport(account);

            return;
        }

        if (account.Type == AccountType.Equity)
        {
            return;
        }

        if (account.Type == AccountType.Bank || account.Type == AccountType.CreditCard)
        {
            InitializeTransactions(account);

            return;
        }

        if (account.Type.IsTemporary())
        {
            return;
        }

        InitializeTransaction(account);
    }

    private void OnListViewSelectedIndexChanged(object sender, EventArgs e)
    {
        if (!TryGetSelection(out Account? account))
        {
            return;
        }

        bool readOnly = account.ReadOnly;

        transactionToolStripMenuItem.Enabled = !readOnly;
        transactionToolStripMenuItem1.Enabled = !readOnly;
        transactionsToolStripMenuItem.Enabled = !readOnly;
        transactionsToolStripMenuItem1.Enabled = !readOnly;
        reconcileToolStripMenuItem.Enabled = !readOnly;
        reconcileToolStripMenuItem1.Enabled = !readOnly;
    }

    private void OnQuickReportToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetSelection(out Account? account))
        {
            return;
        }

        QuickReport(account);
    }

    private void QuickReport(Account account)
    {
        if (!_engine.TryGetReport(ReportEngine.GeneralJournalReport, out IntervalView? report))
        {
            return;
        }

        string title = $"{_engine.Views[ReportEngine.GeneralJournalReport].GenericTitle} - {account.Name}";

        report.Title = title;
        report.Started = _company.Started;
        report.Posted = _company.Posted;
        report.Level = ReportLevel.ByAccount;

        ReportsForm form = new ReportsForm(_engine)
        {
            Text = title
        };

        form.Load += (sender, e) =>
        {
            HashSet<Account> accounts = new HashSet<Account>(account.Children) { account };

            report.Accounts = new AccountsView(_company, accounts);

            form.InitializeReport(ReportEngine.GeneralJournalReport);
        };
        _factory.Kill(account.Id);
        _factory.Register(account.Id, form);
    }

    private void OnRefreshToolStripMenuItemClick(object sender, EventArgs e)
    {
        InitializeAccounts();
    }

    private void OnInactiveToolStripMenuItemCheckedChanged(object sender, EventArgs e)
    {
        Settings.Default.Inactive = inactiveToolStripMenuItem.Checked;

        Settings.Default.Save();
        InitializeAccounts();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _company.AccountAdded -= OnCompanyAccountAdded;
            _company.AccountUpdated -= OnCompanyAccountUpdated;
            _company.AccountRemoved -= OnCompanyAccountRemoved;

            if (components != null)
            {
                components.Dispose();
                components = null;
            }
        }

        base.Dispose(disposing);
    }
}
