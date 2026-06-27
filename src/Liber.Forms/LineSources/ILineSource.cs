// LineSource.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Liber.Forms.LineSources;

internal interface ILineSource
{
    string Name { get; }
    Color Color { get; }

    bool Contains(Line value);

    bool CanEditSibling(Line value);
    bool CanGetNewLines(Guid siblingId);
    IReadOnlyCollection<Line> GetNewLines(Guid siblingId, decimal balance);

    IEnumerable<Line> GetOrderedLines();
    AccountType GetRepresentativeType();

    bool IsInvalidatedByAccountRemoved(Guid id);
    bool IsInvalidatedByAccountUpdated(Guid id);
    bool IsInvalidatedByTransactionAdded(Guid id);
    bool IsInvalidatedByTransactionReconciled(Guid id);
}
