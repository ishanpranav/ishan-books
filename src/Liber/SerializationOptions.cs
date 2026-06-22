// SerializationOptions.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Liber;

public static class SerializationOptions
{
    public static readonly JsonSerializerOptions Json = new JsonSerializerOptions()
    {
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    static SerializationOptions()
    {
        Json.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
        Json.Converters.Add(new JsonColorConverter());
        Json.Converters.Add(new TypeConverterJsonConverterAdapter());
    }
}
