using System;
using System.Xml.Serialization;

namespace Liber;

[XmlRoot("report")]
public class Report
{
    [XmlElement("company")]
    public Company? Company { get; set; }

    [XmlElement("start")]
    public DateTime Start { get; set; }

    [XmlElement("end")]
    public DateTime End { get; set; }
}
