// ImportContext.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Liber.Forms.Import;

internal sealed class ImportContext
{
    public ImportContext(IReadOnlyCollection<Account> accounts)
    {
        Accounts = accounts;
    }

    public IReadOnlyCollection<Account> Accounts { get; }
    public Account? EquityAccount { get; set; }
    public Account? OtherEquityAccount { get; set; }
}
