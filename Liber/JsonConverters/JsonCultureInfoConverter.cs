// JsonCultureInfoConverter.cs
// Copyright (c) 2021-2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Globalization;

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// Converts a <see cref="CultureInfo"/> value to or from JSON.
    /// </summary>
    public class JsonCultureInfoConverter : JsonConverter<CultureInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonCultureInfoConverter"/> class.
        /// </summary>
        public JsonCultureInfoConverter() { }

        /// <inheritdoc/>
        public override CultureInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? text = reader.GetString();

            if (text == null)
            {
                return null;
            }

            return new CultureInfo(text);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, CultureInfo? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value.Name);
            }
        }
    }
}