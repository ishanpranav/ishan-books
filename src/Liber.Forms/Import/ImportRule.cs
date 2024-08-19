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

    [LocalizedDisplayName(nameof(Strict))]
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public bool Strict { get; set; }

    public void Apply(IEnumerable<Account> accounts)
    {
        if (Type == AccountType.None)
        {
            return;
        }

        foreach (Account account in accounts)
        {
            if (!Strict && !account.Type.IsUncategorized())
            {
                continue;
            }

            if (Filter.IsMatch(account.Name))
            {
                account.Type = Type;
            }
        }
    }
}
