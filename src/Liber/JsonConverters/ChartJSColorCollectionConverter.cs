// ChartJSColorConverter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Drawing;

namespace System.Text.Json.Serialization;

/// <summary>
/// Converts a <see cref="Color"/> to and from its ChartJS-compatible JSON representation.
/// </summary>
public sealed class ChartJSColorCollectionConverter : JsonConverter<IEnumerable<Color>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonColorConverter"/> class.
    /// </summary>
    public ChartJSColorCollectionConverter() { }

    /// <inheritdoc/>
    public override IEnumerable<Color> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new InvalidOperationException();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IEnumerable<Color> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (Color color in value)
        {
            writer.WriteStringValue($"rgba({color.R},{color.G},{color.B},{color.A / 255d})");
        }

        writer.WriteEndArray();
    }
}
