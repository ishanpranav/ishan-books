// MessagePackColorFormatter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;

namespace MessagePack.Formatters;

/// <summary>
/// Formats a <see cref="Color"/> for serialization and deserialization using MessagePack.
/// </summary>
public class MessagePackColorFormatter : IMessagePackFormatter<Color>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessagePackColorFormatter"/> class.
    /// </summary>
    public MessagePackColorFormatter() { }

    /// <inheritdoc/>
    public Color Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return Color.FromArgb(reader.ReadInt32());
    }

    /// <inheritdoc/>
    public void Serialize(ref MessagePackWriter writer, Color value, MessagePackSerializerOptions options)
    {
        writer.WriteInt32(value.ToArgb());
    }
}
