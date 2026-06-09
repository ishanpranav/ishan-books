// Line.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace Liber;

public class Line
{
    [Browsable(false)]
    [Ignore]
    public Guid AccountId { get; set; }

    [Browsable(false)]
    [Index(14)]
    [Name("Value Num.")]
    [NumberStyles(NumberStyles.Currency)]
    public decimal Balance { get; set; }

    [JsonIgnore]
    [LocalizedDisplayName(nameof(Debit))]
    public decimal Debit
    {
        get
        {
            if (Balance < 0)
            {
                return 0;
            }

            return Balance;
        }
    }

    [JsonIgnore]
    [LocalizedDisplayName(nameof(Credit))]
    public decimal Credit
    {
        get
        {
            if (Balance > 0)
            {
                return 0;
            }

            return -Balance;
        }
    }

    [Index(8)]
    [LocalizedDisplayName(nameof(Description))]
    [Name("Memo")]
    [NullValues("")]
    [Optional]
    public string? Description { get; set; }

    [Index(16)]
    [LocalizedDisplayName(nameof(Reconciled))]
    [Name("Reconcile Date")]
    [NullValues("")]
    [Optional]
    public DateTime? Reconciled { get; set; }

    [Browsable(false)]
    [Ignore]
    [JsonIgnore]
    public Transaction? Transaction { get; internal set; }

    [Browsable(false)]
    [Ignore]
    [JsonIgnore]
    public Line? Sibling
    {
        get
        {
            if (Transaction == null)
            {
                return null;
            }

            return Transaction.GetDoubleEntry(this);
        }
    }
}
