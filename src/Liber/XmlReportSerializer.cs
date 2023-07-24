// XmlReportSerializer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Xml;
using System.Xml.Resolvers;
using System.Xml.Xsl;

namespace Liber;

public static class XmlReportSerializer
{
    private static readonly XmlReaderSettings s_xslSettings = new XmlReaderSettings()
    {
        XmlResolver = s_resolver
    };
    private static readonly XmlReportResolver s_resolver = new XmlReportResolver(s_xslSettings);
    private static readonly XmlWriterSettings s_xhtmlSettings = new XmlWriterSettings()
    {
        Indent = true,
        NewLineOnAttributes = true
    };

    public static XslStylesheet DeserializeStylesheet(string path)
    {
        using XmlReader xslReader = XmlReader.Create(path, s_xslSettings);

        return (XslStylesheet)XmlSerializers.Stylesheet.Deserialize(xslReader)!;
    }

    public static XslCompiledTransform DeserializeTransform(string path)
    {
        XslCompiledTransform result = new XslCompiledTransform();

        using XmlReader xslReader = XmlReader.Create(path, s_xslSettings);

        result.Load(xslReader, settings: null, s_resolver);

        return result;
    }

    public static string Serialize(XslCompiledTransform transform, Report report)
    {
        using MemoryStream memoryStream = new MemoryStream();
        using XmlReportWriter xmlWriter = new XmlReportWriter(memoryStream, report);

        memoryStream.Seek(offset: 0, SeekOrigin.Begin);

        using XmlReader xmlReader = XmlReader.Create(memoryStream);
        using StringWriter stringWriter = new StringWriter();
        using XmlWriter xhtmlWriter = XmlWriter.Create(stringWriter, s_xhtmlSettings);

        transform.Transform(xmlReader, xhtmlWriter);

        return stringWriter.ToString();
    }
}
