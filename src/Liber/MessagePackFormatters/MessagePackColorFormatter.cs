// MessagePackColorFormatter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;

namespace MessagePack.Formatters;

public sealed class MessagePackColorFormatter : IMessagePackFormatter<Color>
{
    public Color Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return Color.FromArgb(reader.ReadInt32());
    }

    public void Serialize(ref MessagePackWriter writer, Color value, MessagePackSerializerOptions options)
    {
        writer.WriteInt32(value.ToArgb());
    }
}
