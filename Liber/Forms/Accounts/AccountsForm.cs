using Liber.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal sealed partial class AccountsForm : Form
{
    private readonly MainContext _context;

    public AccountsForm(MainContext context, Guid _)
    {
        InitializeComponent();

        _context = context;
        _context.Company.AccountAdded += OnCompanyAccountAdded;
        _context.Company.AccountRemoved += OnCompanyAccountRemoved;

        foreach (KeyValuePair<Guid, Account> account in _context.Company.Accounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        _listView.AutoResizeColumns();

        deleteToolStripMenuItem.ShortcutKeys = Keys.Delete;
    }

    private void InitializeAccount(Guid id, Account value)
    {
        string key = id.ToString();
        ListViewItem item = _listView.Items.Add(key, value.Name, imageIndex: 0);

        item.Tag = id;
        item.Selected = true;

        item.SubItems.AddRange(new string[]
        {
            value.Number.ToString(),
            value.Type.ToLocalizedString(),
            value.Balance.ToString("c")
        });
    }

    private void OnCompanyAccountAdded(object? sender, AccountEventArgs e)
    {
        InitializeAccount(e.Id, e.Account);
        _listView.AutoResizeColumns();
    }

    private void OnCompanyAccountRemoved(object? sender, AccountEventArgs e)
    {
        _listView.Items.RemoveByKey(e.Id.ToString());
        _listView.AutoResizeColumns();
    }

    private async void OnNewToolStripMenuItemClick(object sender, EventArgs e)
    {
        await FormCommand.ShellExecuteAsync(_context, typeof(NewAccountForm));
    }

    private void OnEditToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out Guid id) || _context.TryKill(id))
        {
            return;
        }

        EditAccountForm form = new EditAccountForm(_context, id);

        _context.Register(id, form);
    }

    private void OnRenameAccountClick(object sender, EventArgs e)
    {
        if (_listView.SelectedItems.Count == 0)
        {
            return;
        }

        _listView.SelectedItems[0].BeginEdit();
    }

    private void OnDeleteAccountToolStripMenuItem(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out Guid id))
        {
            return;
        }

        if (_context.Company.Accounts[id].Transactions.Count > 0)
        {
            return;
        }

        _ = _context.Company.RemoveAccount(id);
    }

    private async void OnImportToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!_context.Form.TryGetOpenPath(".csv", filterIndex: 3, out string? path))
        {
            return;
        }

        await ImportCommand.ShellExecuteAsync(_context, path);
    }

    private void OnJournalToolStripMenuItemClick(object sender, EventArgs e)
    {
        FormCommand.ShellExecuteAsync(_context, typeof(JournalForm));
    }

    private void OnListViewAfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        ListViewItem item = _listView.Items[e.Item];
        Guid id = (Guid)item.Tag;
        Account value = _context.Company.Accounts[id];

        try
        {
            _context.Company.AddOrUpdateAccount(id, value.Number, e.Label!, value.Type, value.Locked, value.CompanionId);
        }
        catch (ArgumentException)
        {
            e.CancelEdit = true;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();

            _context.Company.AccountAdded -= OnCompanyAccountAdded;
            _context.Company.AccountRemoved -= OnCompanyAccountRemoved;
        }

        base.Dispose(disposing);
    }
}
