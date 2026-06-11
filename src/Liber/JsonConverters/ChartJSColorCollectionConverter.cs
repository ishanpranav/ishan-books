// ChartJSColorCollectionConverter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Drawing;

namespace System.Text.Json.Serialization;

/// <summary>
/// Converts a collection of <see cref="Color"/> objects to its ChartJS-compatible JSON representation.
/// </summary>
public sealed class ChartJSColorCollectionConverter : JsonConverter<IEnumerable<Color>>
{
    private static readonly ChartJSColorConverter s_itemConverter = new ChartJSColorConverter();

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonColorConverter"/> class.
    /// </summary>
    public ChartJSColorCollectionConverter() { }

    /// <inheritdoc/>
    public override IEnumerable<Color> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IEnumerable<Color> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (Color item in value)
        {
            s_itemConverter.Write(writer, item, options);
        }

        writer.WriteEndArray();
    }
}
