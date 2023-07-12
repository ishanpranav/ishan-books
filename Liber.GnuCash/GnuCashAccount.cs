using CsvHelper.Configuration.Attributes;

namespace Liber.GnuCash;

public class GnuCashAccount
{
    private readonly Account _account;

    public GnuCashAccount(Account account)
    {
        _account = account;
    }

    [Index(0)]
    [Name("Type")]
    public AccountType Type
    {
        get
        {
            return _account.Type;
        }
        set
        {
            _account.Type = value;
        }
    }

    [Index(1)]
    [Name("Full Account Name")]
    public string Path { get; set; }

    public Account ToAccount()
    {
        return _account;
    }
}
