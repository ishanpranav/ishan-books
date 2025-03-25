// JsonColorConverter.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;

namespace System.Text.Json.Serialization;

/// <summary>
/// Converts a <see cref="Color"/> to and from its JSON representation.
/// </summary>
public sealed class JsonColorConverter : JsonConverter<Color>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonColorConverter"/> class.
    /// </summary>
    public JsonColorConverter() { }

    /// <inheritdoc/>
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Color.FromArgb(reader.GetInt32());
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.ToArgb());
    }
}
