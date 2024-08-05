// GuidEventArgs.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

/// <summary>
/// Provides event arguments containing a <see cref="Guid"/> identifier.
/// </summary>
public class GuidEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GuidEventArgs"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The <see cref="Guid"/> identifier.</param>
    public GuidEventArgs(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the <see cref="Guid"/> identifier associated with the event.
    /// </summary>
    /// <value>The <see cref="Guid"/> identifier.</value>
    public Guid Id { get; }
}
