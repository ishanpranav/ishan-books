using System;
using System.Xml.Serialization;

namespace Liber;

[XmlRoot("report")]
public class Report
{
    [XmlElement("company")]
    public Company? Company { get; set; }

    [XmlElement("started")]
    public DateTime Started { get; set; }

    [XmlElement("posted")]
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
