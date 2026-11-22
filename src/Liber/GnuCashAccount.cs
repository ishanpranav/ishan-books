// GnuCashAccount.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace Liber;

[NewLine("\n")]
public class GnuCashAccount
{
    /// <summary>
    /// Gets or sets the account number.
    /// </summary>
    /// <value>The account number.</value>
    [Default(0)]
    [Index(3)]
    [LocalizedDisplayName(nameof(Number))]
    [Name("Account Code")]
    [Optional]
    public decimal Number
    {
        get
        {
            return Value.Number;
        }
        set
        {
            Value.Number = value;
        }
    }

    /// <summary>
    /// Gets or sets the account name.
    /// </summary>
    /// <value>The account name.</value>
    [Index(2)]
    [LocalizedDisplayName(nameof(Name))]
    [Name("Account Name")]
    [Optional]
    public string Name
    {
        get
        {
            return Value.Name;
        }
        set
        {
            Value.Name = value;
        }
    }

    /// <summary>
    /// Gets or sets the account type.
    /// </summary>
    /// <value>The account type.</value>
    [Index(0)]
    [LocalizedDisplayName(nameof(Type))]
    [Name("Type")]
    [Optional]
    [CsvHelper.Configuration.Attributes.TypeConverter(typeof(GnuCashAccountTypeConverter))]
    [System.ComponentModel.TypeConverter(typeof(LocalizedEnumConverter<AccountType>))]
    public AccountType Type
    {
        get
        {
            return Value.Type;
        }
        set
        {
            Value.Type = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the account is a placeholder.
    /// </summary>
    /// <value><see langword="true"/> if the account is a placeholder; otherwise, <see langword="false"/>.</value>
    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(11)]
    [LocalizedDisplayName(nameof(Placeholder))]
    [Name("Placeholder")]
    [Optional]
    public bool Placeholder
    {
        get
        {
            return Value.Placeholder;
        }
        set
        {
            Value.Placeholder = value;
        }
    }

    /// <summary>
    /// Gets or sets the description of the account.
    /// </summary>
    /// <value>The description of the account.</value>
    [Index(4)]
    [LocalizedDisplayName(nameof(Description))]
    [Name("Description")]
    [NullValues("")]
    [Optional]
    public string? Description
    {
        get
        {
            return Value.Description;
        }
        set
        {
            Value.Description = value;
        }
    }

    /// <summary>
    /// Gets or sets the memo associated with the account.
    /// </summary>
    /// <value>The memo associated with the account.</value>
    [Index(6)]
    [LocalizedDisplayName(nameof(Memo))]
    [Name("Notes")]
    [NullValues("")]
    [Optional]
    public string? Memo
    {
        get
        {
            return Value.Memo;
        }
        set
        {
            Value.Memo = value;
        }
    }

    /// <summary>
    /// Gets or sets the color associated with the account.
    /// </summary>
    /// <value>The color associated with the account.</value>
    [Index(5)]
    [LocalizedDisplayName(nameof(Color))]
    [Name("Account Color")]
    [Optional]
    [CsvHelper.Configuration.Attributes.TypeConverter(typeof(CsvHelper.TypeConversion.ColorConverter))]
    public Color Color
    {
        get
        {
            return Value.Color;
        }
        set
        {
            Value.Color = value;
        }
    }

    /// <summary>
    /// Gets or sets the tax category associated with the account.
    /// </summary>
    /// <value>The tax category associated with the account.</value>
    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [LocalizedDisplayName(nameof(TaxType))]
    [Name("Tax Info")]
    public bool TaxType
    {
        get
        {
            return Value.TaxType;
        }
        set
        {
            Value.TaxType = value;
        }
    }

    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(9)]
    [LocalizedDisplayName(nameof(Inactive))]
    [Name("Hidden")]
    [Optional]
    public bool Inactive
    {
        get
        {
            return Value.Inactive;
        }
        set
        {
            Value.Inactive = value;
        }
    }

    [Index(1)]
    [LocalizedDisplayName("GnuCashAccount.Path")]
    [Name("Account Full Name", "Full Account Name")]
    [Optional]
    public string Path { get; set; } = string.Empty;

    [Browsable(false)]
    [Index(7)]
    [Name("Symbol")]
    [Optional]
    public string Symbol { get; set; } = "USD";

    [Browsable(false)]
    [Index(8)]
    [Name("Namespace")]
    [Optional]
    public string Namespace { get; set; } = "CURRENCY";

    [Browsable(false)]
    [Ignore]
    public Account Value { get; set; } = new Account();
}
