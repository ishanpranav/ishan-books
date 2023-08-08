// Writer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber.Writers;

namespace Liber.Forms.Writers;

internal sealed class Writer
{
    public Writer(FilterIndex index, IWriter value)
    {
        Index = index;
        Value = value;
    }

    public FilterIndex Index { get; }
    public IWriter Value { get; }
}
