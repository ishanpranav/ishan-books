// JsonRegexConverter.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Text.RegularExpressions;

namespace System.Text.Json.Serialization;

/// <summary>
/// Converts a <see cref="Regex"/> to and from its JSON representation.
/// </summary>
public sealed class JsonRegexConverter : JsonConverter<Regex>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonColorConverter"/> class.
    /// </summary>
    public JsonRegexConverter() { }

    /// <inheritdoc/>
    public override Regex Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();

        if (value == null)
        {
            return new Regex(string.Empty);
        }

        return new Regex(value);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Regex value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
