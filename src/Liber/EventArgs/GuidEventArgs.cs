// GuidEventArgs.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

public class GuidEventArgs : EventArgs
{
    public GuidEventArgs(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
