using System.Xml.Serialization;

namespace Liber;

[XmlRoot("variable")]
public class XslVariable
{
    [XmlAttribute("name")]
    public string Name { get; set; } = string.Empty;

    [XmlText]
    public string Value { get; set; } = string.Empty;
}
