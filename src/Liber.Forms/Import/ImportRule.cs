// ImportRule.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Liber.Forms.Import;

internal sealed class ImportRule
{
    [JsonConverter(typeof(JsonRegexConverter))]
    [LocalizedDisplayName(nameof(Filter))]
    [TypeConverter(typeof(RegexConverter))]
    public Regex Filter { get; set; } = Filters.Any();

    [LocalizedDisplayName(nameof(Type))]
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public AccountType Type { get; set; }

    [LocalizedDisplayName(nameof(CashFlow))]
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public CashFlow CashFlow { get; set; }

    [LocalizedDisplayName(nameof(Strict))]
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public bool Strict { get; set; }

    public void Apply(IEnumerable<Account> accounts)
    {
        if (Type == AccountType.None && CashFlow == CashFlow.None)
        {
            return;
        }

        foreach (Account account in accounts)
        {
            if (Filter.IsMatch(account.Name))
            {
                if (Type != AccountType.None &&
                    (Strict || account.Type.IsUncategorized()))
                {
                    account.Type = Type;
                }

                if (CashFlow != CashFlow.None)
                {
                    account.CashFlow = CashFlow;
                }
            }
        }
    }
}
