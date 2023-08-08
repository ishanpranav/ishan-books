// GnuCashAccountWriter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Liber.Writers;

public class GnuCashAccountWriter : IWriter
{
    private static string GetPath(Company company, Account account)
    {
        StringBuilder pathBuilder = new StringBuilder();

        pathBuilder.Append(account.Name);

        Account current = account;

        while (current.ParentId != Guid.Empty)
        {
            current = company.Accounts[current.ParentId];

            pathBuilder
                .Insert(0, ':')
                .Insert(0, current.Name);
        }

        return pathBuilder.ToString();
    }

    public async Task SerializeAsync(Stream output, Company company)
    {
        int i = 0;
        GnuCashAccount[] accounts = new GnuCashAccount[company.Accounts.Count];

        foreach (Account account in company.Accounts.Values)
        {
            accounts[i] = new GnuCashAccount()
            {
                Value = account,
                Path = GetPath(company, account)
            };
            i++;
        }

        await GnuCashSerializer.SerializeAsync(output, accounts);
    }
}
