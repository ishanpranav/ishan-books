// XmlReportSerializer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace Liber.Forms.Reports.Xsl;

internal static class XmlReportSerializer
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

    private static XmlWriterSettings? s_settings;
    private static XmlSerializer? s_serializer;

    public static XmlSerializer Serializer
    {
        get
        {
            s_serializer ??= new XmlSerializer(typeof(XslReport));

            return s_serializer;
        }
    }

    public static XmlWriterSettings Settings
    {
        get
        {
            s_settings ??= new XmlWriterSettings()
            {
                Indent = true,
                NewLineOnAttributes = true
            };

            return s_settings;
        }
    }

    public static XslCompiledTransform DeserializeTransform(string path)
    {
        XslCompiledTransform result = new XslCompiledTransform();

        using XmlReader xslReader = XmlReader.Create(path, s_xslSettings);

        result.Load(xslReader, s_xsltSettings, s_resolver);

        return result;
    }

    public static string Serialize(XslCompiledTransform transform, XslReport report)
    {
        using MemoryStream memoryStream = new MemoryStream();
        using XmlWriter xmlWriter = XmlWriter.Create(memoryStream);

        Serializer.Serialize(xmlWriter, report);
        memoryStream.Seek(offset: 0, SeekOrigin.Begin);

        using XmlReader xmlReader = XmlReader.Create(memoryStream, s_xslSettings);
        using StringWriter stringWriter = new StringWriter();
        using XmlWriter xhtmlWriter = XmlWriter.Create(stringWriter, Settings);

        XsltArgumentList arguments = new XsltArgumentList();

        arguments.AddExtensionObject("urn:liber", report);
        transform.Transform(xmlReader, arguments, xhtmlWriter, s_resolver);

        return stringWriter.ToString();
    }
}
