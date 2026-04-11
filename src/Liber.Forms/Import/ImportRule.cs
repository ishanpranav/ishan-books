// ImportRule.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
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

    [LocalizedDisplayName(nameof(TaxType))]
    public string? TaxType { get; set; }

    [LocalizedDisplayName(nameof(Equity))]
    public bool Equity { get; set; }

    [LocalizedDisplayName(nameof(OtherEquity))]
    public bool OtherEquity { get; set; }

    [LocalizedDisplayName(nameof(Strict))]
    public bool Strict { get; set; }

    public void Apply(ImportContext context)
    {
        if (Type == AccountType.None && CashFlow == CashFlow.None && TaxType == null && !Equity && !OtherEquity)
        {
            return;
        }

        foreach (Account account in context.Accounts)
        {
            if (!Filter.IsMatch(account.Name))
            {
                continue;
            }

            if (Type != AccountType.None && (Strict || account.Type.IsUncategorized()))
            {
                account.Type = Type;
            }

            if (CashFlow != CashFlow.None && (Strict || account.CashFlow == CashFlow.None))
            {
                account.CashFlow = CashFlow;
            }

            if (TaxType != null && (Strict || account.TaxType == null || account.TaxType == "T" || account.TaxType == "F"))
            {
                account.TaxType = TaxType;
            }

            if (Equity && (Strict || context.EquityAccount == null))
            {
                context.EquityAccount = account;
            }

            if (OtherEquity && (Strict || context.OtherEquityAccount == null))
            {
                context.OtherEquityAccount = account;
            }
        }
    }
}
