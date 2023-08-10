// ReverseDateTimeComparer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace System.Collections.Specialized;

/// <summary>
/// Represents a comparer for <see cref="DateTime"/> values that sorts them in descending order.
/// </summary>
/// <remarks>
/// This comparer is used to sort <see cref="DateTime"/> values in descending (reverse chronological) order.
/// </remarks>
public class ReverseDateTimeComparer : Comparer<DateTime>
{
    private static ReverseDateTimeComparer? s_instance;

    /// <summary>
    /// Gets the default instance of <see cref="ReverseDateTimeComparer"/>.
    /// </summary>
    /// <value>The default instance of the <see cref="ReverseDateTimeComparer"/> class.</value>
    public static new ReverseDateTimeComparer Default
    {
        get
        {
            s_instance ??= new ReverseDateTimeComparer();

            return s_instance;
        }
    }

    /// <inheritdoc/>
    public override int Compare(DateTime x, DateTime y)
    {
        return y.CompareTo(x);
    }
}
