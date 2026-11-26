// GnuCashAccountWriter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Threading.Tasks;

namespace Liber.CompanyWriters;

/// <summary>
/// An <see cref="ICompanyWriter"/> for GnuCash (CSV) account data.
/// </summary>
public class GnuCashAccountWriter : ICompanyWriter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GnuCashAccountWriter"/> class.
    /// </summary>
    public GnuCashAccountWriter() { }

    /// <inheritdoc/>
    public async Task WriteAsync(Stream output, Company company)
    {
        int i = 0;
        GnuCashAccount[] accounts = new GnuCashAccount[company.Accounts.Count];

        foreach (Account account in company.Accounts)
        {
            accounts[i] = new GnuCashAccount()
            {
                Value = account,
                Path = GnuCashUtilities.GetPath(company, account)
            };
            i++;
        }

        await CsvSerializer.SerializeAsync(output, accounts);
    }
}
