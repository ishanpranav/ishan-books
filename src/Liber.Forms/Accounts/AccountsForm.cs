// AccountsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Humanizer;
using Liber.Forms.Components;
using Liber.Forms.Properties;
using Liber.Forms.Transactions;

namespace Liber.Forms.Accounts;

// TODO: Make sure that accounts without certain permissions cannot use all the menu options
internal sealed partial class AccountsForm : Form
{
    private readonly Company _company;
    private readonly FormFactory _factory;
    private readonly Dictionary<Guid, ListViewItem> _items = new Dictionary<Guid, ListViewItem>();
    private readonly Dictionary<Color, int> _imageIndices = new Dictionary<Color, int>();

    public AccountsForm(Company company, FormFactory factory)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);
        Design.ApplyStyles(_contextMenu);

        _company = company;
        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
        _factory = factory;
        inactiveToolStripMenuItem.Checked = Settings.Default.Inactive;

        InitializeAccounts();
    }

    private void InitializeAccounts()
    {
        _items.Clear();
        _listView.BeginUpdate();
        _listView.Items.Clear();

        foreach (KeyValuePair<Guid, Account> account in _company.OrderedAccounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        _listView.AutoResizeColumns();
        _listView.Sort();
        _listView.EndUpdate();
    }

    private void InitializeAccount(Guid id, Account value)
    {
        if (value.Inactive && !inactiveToolStripMenuItem.Checked)
        {
            return;
        }

        ListViewItem item = _listView.Items.Add(new ListViewItem()
        {
            Tag = id
        });

        AddSubItems(item, value);
        _items.Add(id, item);
    }

    private void InitializeTransaction(Guid id)
    {
        Guid key = typeof(TransactionForm).GUID;

        if (_factory.TryKill(key))
        {
            return;
        }

        if (id != Guid.Empty && _company.Accounts[id].Placeholder)
        {
            id = Guid.Empty;
        }

        TransactionForm form = new TransactionForm(_company);
        DateTime lastPosted = Settings.Default.LastPosted;
        Transaction transaction = new Transaction()
        {
            Posted = lastPosted == default ? DateTime.Today : lastPosted,
            Number = _company.NextTransactionNumber
        };

        if (id != Guid.Empty)
        {
            transaction.Lines.Add(new Line()
            {
                AccountId = id
            });
        }

        form.InitializeTransaction(transaction);
        _factory.Register(key, form);
    }

    private void InitializeTransactions(Guid id)
    {
        if (_factory.TryKill(id) || _company.Accounts[id].Placeholder)
        {
            return;
        }

        TransactionsForm form = new TransactionsForm(_company, id);

        _factory.Register(id, form);
    }

    private void AddSubItems(ListViewItem item, Account value)
    {
        item.Text = value.Name;

        decimal balance;
        DateTime posted = DateTime.Today;
        DateTime started = new DateTime(posted.Year, month: 1, day: 1);

        if (value == _company.Accounts[_company.EquityAccountId])
        {
            balance = _company.GetEquity(started, Filters.Any());
        }
        else if (value.Type.IsTemporary())
        {
            balance = value.GetBalance(started, posted, Filters.Any());
        }
        else
        {
            balance = value.GetBalance(posted, Filters.Any());
        }

        balance = value.Type.ToBalance(balance);

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

        if (value.Placeholder)
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
        InitializeAccount(e.Id, _company.Accounts[e.Id]);
        _listView.AutoResizeColumns();
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        ListViewItem item = _items[e.Id];

        item.SubItems.Clear();
        AddSubItems(item, _company.Accounts[e.Id]);
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        ListViewItem item = _items[e.Id];

        _listView.Items.Remove(item);
        _listView.AutoResizeColumns();
    }

    private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_listView.TryGetSelection(out Guid id))
        {
            _factory.AutoRegister(() => new NewAccountForm(_company, id));
        }
        else
        {
            _factory.AutoRegister(() => new NewAccountForm(_company));
        }
    }

    private void OnEditToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out Guid id) || _factory.TryKill(id))
        {
            return;
        }

        EditAccountForm form = new EditAccountForm(_company, id);

        _factory.Register(id, form);
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
        if (!_listView.TryGetSelection(out Guid id))
        {
            return;
        }

        _company.RemoveAccount(id);
    }

    private void OnTransactionToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_listView.TryGetSelection(out Guid id))
        {
            InitializeTransaction(id);
        }
        else
        {
            InitializeTransaction(Guid.Empty);
        }
    }

    private void OnTransactionsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_listView.TryGetSelection(out Guid id))
        {
            InitializeTransactions(id);
        }
    }

    private void OnListViewAfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        Guid id = (Guid)_listView.Items[e.Item].Tag!;

        if (!_company.Accounts.TryGetValue(id, out Account? value))
        {
            return;
        }

        value.Name = e.Label!;

        _company.UpdateAccount(id, value.ParentId);
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out Guid id))
        {
            return;
        }

        Account value = _company.Accounts[id];

        if (value.Placeholder)
        {
            // TODO: QuickReport

            return;
        }

        if (value.Type == AccountType.Equity)
        {

        }

        if (value.Type == AccountType.Bank || value.Type == AccountType.CreditCard)
        {
            InitializeTransactions(id);
        }
        else if (value.Type.IsTemporary())
        {

        }
        else
        {
            InitializeTransaction(id);
        }
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
