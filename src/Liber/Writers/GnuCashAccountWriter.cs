// GnuCashAccountWriter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Threading.Tasks;

namespace Liber.Writers;

/// <summary>
/// An <see cref="IWriter"/> for GnuCash (CSV) account data.
/// </summary>
public class GnuCashAccountWriter : IWriter
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

        foreach (Account account in company.Accounts.Values)
        {
            accounts[i] = new GnuCashAccount()
            {
                Value = account,
                Path = GnuCashSerializer.GetPath(company, account)
            };
            i++;
        }

        await GnuCashSerializer.SerializeAsync(output, accounts);
    }
}
