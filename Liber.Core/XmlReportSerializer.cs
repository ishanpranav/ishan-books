using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace Liber;

public static class XmlReportSerializer
{
    private static readonly XmlWriterSettings s_xmlSettings = new XmlWriterSettings()
    {
        Async = true
    };
    private static readonly XmlWriterSettings s_xhtmlSettings = new XmlWriterSettings()
    {
        Async = true,
        Indent = true,
        NewLineOnAttributes = true
    };

    public static string Serialize(XslCompiledTransform transform, Company company)
    {
        using MemoryStream memoryStream = new MemoryStream();
        using XmlWriter xmlWriter = XmlWriter.Create(memoryStream, s_xmlSettings);

        XmlSerializers.Report.Serialize(xmlWriter, new Report()
        {
            Company = company,
            Start = new DateTime(DateTime.Today.Year, 1, 1),
            End = DateTime.Today
        });
        memoryStream.Seek(offset: 0, SeekOrigin.Begin);

        using XmlReader xmlReader = XmlReader.Create(memoryStream);
        using StringWriter stringWriter = new StringWriter();
        using XmlWriter xhtmlWriter = XmlWriter.Create(stringWriter, s_xhtmlSettings);

        transform.Transform(xmlReader, xhtmlWriter);

        return stringWriter.ToString();
    }
}
