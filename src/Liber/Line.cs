// Line.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;
using MessagePack;

namespace Liber;

[MessagePackObject]
public class Line
{
    [Browsable(false)]
    [Ignore]
    [Key(0)]
    public Guid AccountId { get; set; }

    [Browsable(false)]
    [Index(14)]
    [Key(1)]
    [Name("Value Num.")]
    [NumberStyles(NumberStyles.Currency)]
    public decimal Balance { get; set; }

    [IgnoreMember]
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

    [IgnoreMember]
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
    [Key(2)]
    [LocalizedDisplayName(nameof(Description))]
    [Name("Memo")]
    [NullValues("")]
    [Optional]
    public string? Description { get; set; }

    [Index(16)]
    [Key(3)]
    [LocalizedDisplayName(nameof(Reconciled))]
    [Name("Reconcile Date")]
    [NullValues("")]
    [Optional]
    public DateTime? Reconciled { get; set; }

    [Browsable(false)]
    [Ignore]
    [IgnoreMember]
    [JsonIgnore]
    public Transaction? Transaction { get; internal set; }

    [Browsable(false)]
    [Ignore]
    [IgnoreMember]
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
