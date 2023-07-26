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
    private static readonly XsltSettings s_xsltSettings = new XsltSettings()
    {
        EnableDocumentFunction = true
    };
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

    public static XslCompiledTransform DeserializeTransform(string path)
    {
        XslCompiledTransform result = new XslCompiledTransform();

        using XmlReader xslReader = XmlReader.Create(path, s_xslSettings);

        result.Load(xslReader, s_xsltSettings, s_resolver);

        return result;
    }

    public static string Serialize(XslCompiledTransform transform, Report report)
    {
        using MemoryStream memoryStream = new MemoryStream();
        using XmlWriter xmlWriter = new XmlReportWriter(memoryStream, report);

        memoryStream.Seek(offset: 0, SeekOrigin.Begin);

        using XmlReader xmlReader = XmlReader.Create(memoryStream, s_xslSettings);
        using StringWriter stringWriter = new StringWriter();
        using XmlWriter xhtmlWriter = XmlWriter.Create(stringWriter, s_xhtmlSettings);

        XsltArgumentList arguments = new XsltArgumentList();

        arguments.AddExtensionObject("urn:liber", report);
        transform.Transform(xmlReader, arguments, xhtmlWriter, s_resolver);

        return stringWriter.ToString();
    }
}
