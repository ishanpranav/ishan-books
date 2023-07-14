using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal sealed partial class ImportAccountsForm : Form
{
    private readonly Company _company;
    private readonly BindingList<Account> _accounts = new BindingList<Account>();

    public ImportAccountsForm(Company company)
    {
        InitializeComponent();

        _company = company;
    }

    public ImportAccountsForm(Company company, IReadOnlyCollection<Account> accounts) : this(company)
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
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        foreach (Account account in _accounts)
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
