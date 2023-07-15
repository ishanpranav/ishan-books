using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Liber.Forms.Accounts;

internal sealed class ImportAccountsForm : ImportForm
{
    private readonly BindingList<Account> _accounts = new BindingList<Account>();

    public ImportAccountsForm(Company company) : base(company)
    {
        _dataGridView.DataSource = _accounts;
    }

    public ImportAccountsForm(Company company, IReadOnlyCollection<Account> accounts) : base(company)
    {
        foreach (Account account in accounts)
        {
            if (account.Name != null)
            {
                int index = account.Name.IndexOf(" - ");

                if (index != -1)
                {
                    account.Name = account.Name.Substring(index + 2).Trim();
                }
            }

            _accounts.Add(account);
        }

        _dataGridView.DataSource = _accounts;

        _dataGridView.AutoResizeColumns();
    }

    protected override void CommitChanges()
    {
        foreach (Account account in _accounts)
        {
            Company.AddAccount(account, Guid.Empty);
        }
    }
}
