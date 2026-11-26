// GnuCashUtilities.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text;

namespace Liber.CompanyWriters;

internal static class GnuCashUtilities
{
    public static string GetPath(Company company, Account account)
    {
        StringBuilder pathBuilder = new StringBuilder();

        pathBuilder.Append(account.Name);

        Account current = account;

        while (current.ParentId != Guid.Empty)
        {
            current = company.GetAccount(current.ParentId);

            pathBuilder
                .Insert(0, ':')
                .Insert(0, current.Name);
        }

        return pathBuilder.ToString();
    }
}
