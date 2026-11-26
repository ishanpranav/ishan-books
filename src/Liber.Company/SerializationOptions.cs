// SerializationOptions.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Liber;

public static class SerializationOptions
{
    private static JsonSerializerOptions? s_json;
    private static JsonSerializerOptions? s_htmlJson;

    public static JsonSerializerOptions Json
    {
        get
        {
            if (s_json == null)
            {
                s_json = new JsonSerializerOptions()
                {
                    AllowTrailingCommas = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                };

                Json.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
                Json.Converters.Add(new JsonColorConverter());
                Json.Converters.Add(new TypeConverterJsonConverterAdapter());
            }

            return s_json;
        }
    }

    public static JsonSerializerOptions HtmlJson
    {
        get
        {
            if (s_htmlJson == null)
            {
                s_htmlJson = new JsonSerializerOptions(SerializationOptions.Json)
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.Never
                };

                s_htmlJson.Converters.Add(new JsonHtmlStringConverter());
            }

            return s_htmlJson;
        }
    }
}
