using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal sealed class ImportAccountsForm : ImportForm
{
    private readonly IReadOnlyCollection<GnuCashAccount> _accounts;

    public ImportAccountsForm(Company company, FormFactory factory, IReadOnlyCollection<GnuCashAccount> accounts) : base(company, factory)
    {
        _accounts = accounts;

        foreach (GnuCashAccount account in accounts)
        {
            account.Value.Name = account.Value.Name;

            _listView.Items.Add(new ListViewItem(account.Value.Name));
        }
    }

    private Guid GetParentKey(Guid key, string value)
    {
        foreach (KeyValuePair<Guid, Account> account in Company.Accounts)
        {
            if (account.Key == key)
            {
                continue;
            }

            if (account.Value.ToString() == value || account.Value.Name == value)
            {
                return account.Key;
            }
        }

        return Guid.Empty;
    }

    protected override void CommitChanges()
    {
        Factory.Kill(typeof(AccountsForm).GUID);

        foreach (GnuCashAccount account in _accounts)
        {
            account.Key = Company.AddAccount(account.Value, Guid.Empty);
        }

        foreach (GnuCashAccount account in _accounts)
        {
            string[] segments = account.Path.Split(':');

            if (segments.Length < 2)
            {
                continue;
            }

            Guid parentKey = GetParentKey(account.Key, segments[segments.Length - 2]);

            if (parentKey == Guid.Empty)
            {
                continue;
            }

            Company.UpdateAccount(account.Key, parentKey);
        }

        Factory.Register(typeof(AccountsForm).GUID, new AccountsForm(Company, Factory));
    }
}
