// XmlReportResolver.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace System.Xml.Resolvers;

internal sealed class XmlReportResolver : XmlResolver
{
    private readonly XmlReaderSettings _settings;

    public XmlReportResolver(XmlReaderSettings settings)
    {
        _settings = settings;
    }

    public override object? GetEntity(Uri absoluteUri, string? role, Type? ofObjectToReturn)
    {
        if (!absoluteUri.IsFile)
        {
            return null;
        }

        return XmlReader.Create(absoluteUri.LocalPath, _settings);
    }
}
