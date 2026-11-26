// XmlReportWriter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Liber.CompanyWriters;
using Liber.Forms.Reports.Xsl;

namespace Liber.Forms.Writers;

internal sealed class XmlReportWriter : ICompanyWriter
{
    public Task WriteAsync(Stream output, Company company)
    {
        XmlWriter writer = XmlWriter.Create(output, XmlReportSerializer.Settings);

        XmlReportSerializer.Serializer.Serialize(writer, new XslReport(Path.ChangeExtension("company", "xml"), company)
        {
            Started = DateTime.UnixEpoch,
            Posted = DateTime.Today
        });

        return Task.CompletedTask;
    }
}
