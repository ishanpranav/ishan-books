// Feature.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Classifiers;

public class Feature : IEquatable<Feature>
{
    public string Type { get; }
    public string Value { get; }
    public int Weight { get; }

    public Feature(string type, string value, int weight)
    {
        Type = type;
        Value = value;
        Weight = weight;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Feature);
    }

    public bool Equals(Feature? other)
    {
        return other != null && Type == other.Type && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Value);
    }
}
