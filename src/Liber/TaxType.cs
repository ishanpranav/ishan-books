// TaxType.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Resources;
using CsvHelper.Configuration.Attributes;

namespace Liber;

public enum TaxType
{
    [Name("F")]
    None = 0,

    [Name("T")]
    Other = 1
}

public static class TaxTypeExtensions
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(TaxTypeExtensions));

    public static string ToLocalizedString(this TaxType value)
    {
        string key = value.ToString();

        return s_resourceManager.GetString(key) ?? key;
    }
}
