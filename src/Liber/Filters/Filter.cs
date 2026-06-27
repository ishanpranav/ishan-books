// Filter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Liber.Filters;

[TypeConverter(typeof(FilterConverter))]
public abstract class Filter : ICloneable
{
    public abstract string Name { get; }

    public abstract bool IsMatch(Line value);

    public abstract Filter Clone();

    object ICloneable.Clone()
    {
        return Clone();
    }

    public override string ToString()
    {
        return Name;
    }
}
