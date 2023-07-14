using System.Collections.Generic;
using System.Xml.Serialization;

namespace Liber;

[XmlRoot("stylesheet")]
public class XslStylesheet
{
    [XmlElement("variable")]
    public List<XslVariable> Variables { get; set; } = new List<XslVariable>();

    public IReadOnlyDictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> results = new Dictionary<string, string>();

        foreach (XslVariable variable in Variables)
        {
            results.Add(variable.Name, variable.Value);
        }

        return results;
    }
}
