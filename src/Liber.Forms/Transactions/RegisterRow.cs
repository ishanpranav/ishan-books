// RegisterRow.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Transactions;

internal sealed class RegisterRow
{
    public int LineIndex { get; }

    public RegisterRow(int lineIndex)
    {
        LineIndex = lineIndex;
    }
}
