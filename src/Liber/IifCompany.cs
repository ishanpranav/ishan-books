// IifCompany.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using CsvHelper.Configuration.Attributes;

namespace Liber;

[Delimiter("\t")]
internal sealed class IifCompany
{
    [Name("!HDR")]
    public IifRecordType RecordType
    {
        get
        {
            return IifRecordType.Company;
        }
    }

    [Name("PROD")]
    public string Product
    {
        get
        {
            return "IshanBooks";
        }
    }

    [Name("VER")]
    public string ProductVersion
    {
        get
        {
            return "Version 1.0";
        }
    }

    [Name("REL")]
    public string ProductRelease
    {
        get
        {
            return "Release R1";
        }
    }

    [Name("IIFVER")]
    public int IifVersion
    {
        get
        {
            return 1;
        }
    }

    [Format("yyyy-MM-dd")]
    [Name("DATE")]
    public DateTime Date
    {
        get
        {
            return DateTime.Today;
        }
    }

    [Name("TIME")]
    public long Timestamp
    {
        get
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }

    [BooleanFalseValues("N")]
    [BooleanTrueValues("Y")]
    [Name("ACCTNT")]
    public bool IsAccountant
    {
        get
        {
            return false;
        }
    }

    [Name("ACCTNTSPLITTIME")]
    public int AccountantSplitTime
    {
        get
        {
            return 0;
        }
    }
}
