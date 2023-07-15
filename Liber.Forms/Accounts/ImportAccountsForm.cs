using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal sealed class ImportAccountsForm : ImportForm
{
    private readonly Company _company;
    private readonly BindingList<GnuCashAccount> _accounts = new BindingList<GnuCashAccount>();

    public ImportAccountsForm(Company company) : base(company)
    {
        _dataGridView.DataSource = _accounts;
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

    public ImportAccountsForm(Company company, IReadOnlyCollection<GnuCashAccount> accounts) : this(company)
    {
        foreach (GnuCashAccount account in accounts)
        {
            account.Value.Name = ExtractName(account.Value.Name);

            _accounts.Add(account);
        }

        return value.Substring(index + 2).Trim();
    }

    protected override void CommitChanges()
    {
        foreach (GnuCashAccount account in _accounts)
        {
            account.Key = _company.AddAccount(account.Value, Guid.Empty);
        }

        foreach (GnuCashAccount account in _accounts)
        {
            string[] segments = account.Path.Split(':');

            if (segments.Length < 2)
            {
                continue;
            }

            Guid parentKey = _company.Accounts.SingleOrDefault(x => x.Value.Name == ExtractName(segments[segments.Length - 2])).Key;

            if (parentKey == Guid.Empty)
            {
                continue;
            }

            _company.UpdateAccount(account.Key, parentKey);
        }

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
