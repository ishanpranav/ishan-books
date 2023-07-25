// Report.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Xml.Serialization;

namespace Liber;

[XmlRoot("report")]
public class Report
{
    [XmlElement("company")]
    public Company? Company { get; set; }

    [XmlIgnore]
    public DateTime Started { get; set; }

    [XmlIgnore]
    public DateTime Posted { get; set; }

    public Transaction MinTransaction
    {
        get
        {
            return new Transaction()
            {
                Posted = Started
            };
        }
    }

    public Transaction MaxTransaction
    {
        get
        {
            return new Transaction()
            {
                Posted = Posted
            };
        }
    }
}
