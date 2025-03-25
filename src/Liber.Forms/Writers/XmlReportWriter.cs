// XmlReportWriter.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Liber.Forms.Reports.Xsl;
using Liber.Writers;

namespace Liber.Forms.Writers;

internal sealed class XmlReportWriter : IWriter
{
    public Task WriteAsync(Stream output, Company company)
    {
        XmlWriter writer = XmlWriter.Create(output, XmlReportSerializer.Settings);

        XmlReportSerializer.Serializer.Serialize(writer, new XslReport(company.DisplayName, company)
        {
            Started = DateTime.MinValue,
            Posted = DateTime.MaxValue
        });

        return Task.CompletedTask;
    }
}
