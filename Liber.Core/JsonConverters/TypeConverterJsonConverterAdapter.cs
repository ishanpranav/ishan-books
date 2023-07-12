// https://github.com/dotnet/runtime/issues/1761
/*
 The MIT License (MIT)

 Copyright (c) .NET Foundation and Contributors

 All rights reserved.

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
*/

#nullable disable
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// Provides an adapter between the <see cref="TypeConverter"/> and <see cref="JsonConverter"/> classes.
    /// </summary>
    public class TypeConverterJsonConverterAdapter : JsonConverterFactory
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert
                .GetCustomAttributes<TypeConverterAttribute>(inherit: true)
                .Any();
        }

        /// <inheritdoc/>
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(typeof(TypeConverterJsonConverterAdapter<>).MakeGenericType(typeToConvert));
        }
    }

    internal sealed class TypeConverterJsonConverterAdapter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (T)TypeDescriptor
                .GetConverter(typeToConvert)
                .ConvertFromString(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(TypeDescriptor
                .GetConverter(value)
                .ConvertToString(value));
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert
                .GetCustomAttributes<TypeConverterAttribute>(inherit: true)
                .Any();
        }
    }
}
#nullable enable