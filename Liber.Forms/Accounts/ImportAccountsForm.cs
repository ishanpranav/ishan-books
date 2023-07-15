using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Liber.Forms.Accounts;

internal sealed class ImportAccountsForm : ImportForm
{
    private readonly BindingList<GnuCashAccount> _accounts = new BindingList<GnuCashAccount>();

    public ImportAccountsForm(Company company) : base(company)
    {
        _dataGridView.DataSource = _accounts;
    }

    public ImportAccountsForm(Company company, IReadOnlyCollection<GnuCashAccount> accounts) : this(company)
    {
        foreach (GnuCashAccount account in accounts)
        {
            account.Value.Name = ExtractName(account.Value.Name);

            _accounts.Add(account);
        }

        _dataGridView.DataSource = _accounts;

        _dataGridView.AutoResizeColumns();
    }

    private static string ExtractName(string value)
    {
        int index = value.IndexOf(" - ");

        if (index == -1)
        {
            return value;
        }

        return value.Substring(index + 2).Trim();
    }

    protected override void CommitChanges()
    {
        foreach (GnuCashAccount account in _accounts)
        {
            _company.AddAccount(account, Guid.Empty);
        }

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
