// ChartJSColorConverter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;

namespace System.Text.Json.Serialization;

/// <summary>
/// Converts a collection of <see cref="Color"/> objects to its ChartJS-compatible JSON representation.
/// </summary>
public class ChartJSColorConverter : JsonConverter<Color>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonColorConverter"/> class.
    /// </summary>
    public ChartJSColorConverter() { }

    /// <inheritdoc/>
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStringValue($"rgba({value.R},{value.G},{value.B},{value.A / 255d})");
    }
}
