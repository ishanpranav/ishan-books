// KeyEventArgs.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

public class KeyEventArgs : EventArgs
{
    public KeyEventArgs(Guid key)
    {
        Key = key;
    }

    public Guid Key { get; }
}
