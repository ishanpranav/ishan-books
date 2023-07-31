// CompanyType.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Resources;

namespace Liber;

public enum CompanyType
{
    None = 0,
    Individual = 1,
    Partnership = 2,
    Corporation = 3
}

public static class CompanyTypeExtensions
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(CompanyTypeExtensions));

    public static string ToLocalizedString(this CompanyType value)
    {
        string key = value.ToString();

        return s_resourceManager.GetString(key) ?? key;
    }
}
