// XmlReportResolver.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Xml;

namespace Liber.Forms.Reports.Xsl;

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
