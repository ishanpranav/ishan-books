// XmlReportWriter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using Liber;

namespace System.Xml;

internal sealed class XmlReportWriter : XmlWriter
{
    private static readonly XmlWriterSettings s_settings = new XmlWriterSettings();

    private XmlWriter? _writer;

    public XmlReportWriter(Stream output, XslReport report)
    {
        _writer = Create(output, s_settings);
        Report = report;

        XmlSerializers.Report.Serialize(this, report);
    }

    public override WriteState WriteState
    {
        get
        {
            if (_writer == null)
            {
                return WriteState.Closed;
            }

            return _writer.WriteState;
        }
    }

    public XslReport Report { get; }

    private XmlWriter Writer
    {
        get
        {
            if (_writer == null)
            {
                throw new InvalidOperationException();
            }

            return _writer;
        }
    }

    public override void Flush()
    {
        Writer.Flush();
    }

    public override string? LookupPrefix(string ns)
    {
        return Writer.LookupPrefix(ns);
    }

    public override void WriteBase64(byte[] buffer, int index, int count)
    {
        Writer.WriteBase64(buffer, index, count);
    }

    public override void WriteCData(string? text)
    {
        Writer.WriteCData(text);
    }

    public override void WriteCharEntity(char ch)
    {
        Writer.WriteCharEntity(ch);
    }

    public override void WriteChars(char[] buffer, int index, int count)
    {
        Writer.WriteChars(buffer, index, count);
    }

    public override void WriteComment(string? text)
    {
        Writer.WriteComment(text);
    }

    public override void WriteDocType(string name, string? pubid, string? sysid, string? subset)
    {
        Writer.WriteDocType(name, pubid, sysid, subset);
    }

    public override void WriteEndAttribute()
    {
        Writer.WriteEndAttribute();
    }

    public override void WriteEndDocument()
    {
        Writer.WriteEndDocument();
    }

    public override void WriteEndElement()
    {
        Writer.WriteEndElement();
    }

    public override void WriteEntityRef(string name)
    {
        Writer.WriteEntityRef(name);
    }

    public override void WriteFullEndElement()
    {
        Writer.WriteFullEndElement();
    }

    public override void WriteProcessingInstruction(string name, string? text)
    {
        Writer.WriteProcessingInstruction(name, text);
    }

    public override void WriteRaw(char[] buffer, int index, int count)
    {
        Writer.WriteRaw(buffer, index, count);
    }

    public override void WriteRaw(string data)
    {
        Writer.WriteRaw(data);
    }

    public override void WriteStartAttribute(string? prefix, string localName, string? ns)
    {
        Writer.WriteStartAttribute(prefix, localName, ns);
    }

    public override void WriteStartDocument()
    {
        Writer.WriteStartDocument();
    }

    public override void WriteStartDocument(bool standalone)
    {
        Writer.WriteStartDocument(standalone);
    }

    public override void WriteStartElement(string? prefix, string localName, string? ns)
    {
        Writer.WriteStartElement(prefix, localName, ns);
    }

    public override void WriteString(string? text)
    {
        Writer.WriteString(text);
    }

    public override void WriteSurrogateCharEntity(char lowChar, char highChar)
    {
        Writer.WriteSurrogateCharEntity(lowChar, highChar);
    }

    public override void WriteWhitespace(string? ws)
    {
        Writer.WriteWhitespace(ws);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_writer != null)
            {
                _writer.Dispose();
                _writer = null;
            }
        }

        base.Dispose(disposing);
    }
}
