// ImportRule.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

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

    [LocalizedDisplayName(nameof(Adjustment))]
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public TriState Adjustment { get; set; }

    [LocalizedDisplayName(nameof(Strict))]
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public bool Strict { get; set; }

    public void Apply(IEnumerable<Account> accounts)
    {
        if (Type == AccountType.None && Adjustment == TriState.UseDefault)
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

                switch (Adjustment)
                {
                    case TriState.True:
                        account.Adjustment = true;
                        break;

                    case TriState.False:
                        account.Adjustment = false;
                        break;
                }
            }
        }
    }
}
