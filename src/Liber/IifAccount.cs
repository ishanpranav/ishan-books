// IifAccount.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using CsvHelper.Configuration.Attributes;

namespace Liber;

[Delimiter("\t")]
public class IifAccount
{
    public IifAccount(Account value, int referenceNumber, IifExtra extra)
    {
        Value = value;
        ReferenceNumber = referenceNumber;
        Extra = extra;
    }

    [Name("!ACCNT")]
    public IifRecordType RecordType
    {
        get
        {
            return IifRecordType.Account;
        }
    }

    [Name("NAME")]
    public string Name
    {
        get
        {
            return Value.Name;
        }
    }

    [Name("REFNUM")]
    public int ReferenceNumber { get; }

    [Name("TIMESTAMP")]
    public long Timestamp
    {
        get
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }

    [Name("ACCNTTYPE")]
    public AccountType Type
    {
        get
        {
            return Value.Type;
        }
    }

    [Name("OBAMOUNT")]
    public decimal OpeningBalance
    {
        get
        {
            return Value.Balance;
        }
    }

    [Name("DESC")]
    public string? Description
    {
        get
        {
            return Value.Description;
        }
    }

    [Name("SCD")]
    public int ScdNumber
    {
        get
        {
            return 0;
        }
    }

    [Name("BANKNUM")]
    public string? NonVirtualNumber
    {
        get
        {
            return null;
        }
    }

    [Name("EXTRA")]
    public IifExtra Extra { get; set; }

    [BooleanFalseValues("N")]
    [BooleanTrueValues("Y")]
    [Name("HIDDEN")]
    public bool Hidden
    {
        get
        {
            return false;
        }
    }

    [Name("DELCOUNT")]
    public int DeleteCount
    {
        get
        {
            return 0;
        }
    }

    [BooleanFalseValues("N")]
    [BooleanTrueValues("Y")]
    [Name("USEID")]
    public bool UseId
    {
        get
        {
            return false;
        }
    }

    [Ignore]
    public Account Value { get; }
}
