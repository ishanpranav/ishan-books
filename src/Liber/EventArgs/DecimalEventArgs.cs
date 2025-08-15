// DecimalEventArgs.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

/// <summary>
/// Provides event arguments containing a <see cref="decimal"/> value.
/// </summary>
public class DecimalEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalEventArgs"/> class with the specified value.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> value.</param>
    public DecimalEventArgs(decimal value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the <see cref="decimal"/> value associated with the event.
    /// </summary>
    /// <value>The <see cref="decimal"/> value.</value>
    public decimal Value { get; }
}
