using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Liber.TaxNodes;

public class AccountTaxNode : TaxNode
{
    [JsonIgnore]
    public IReadOnlySet<Account> Accounts { get; set; } = new HashSet<Account>();

    public AccountType Type { get; set; } = AccountType.Income;
    public string? TaxType { get; set; }

    protected override decimal EvaluateCore(DateTime started, DateTime posted)
    {
        decimal sum = 0;

        foreach (Account account in Accounts)
        {
            sum += account.GetBalance(started, posted, Filters.Any());
        }

        return Type.ToBalance(sum);
    }
}
