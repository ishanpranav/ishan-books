// XslStylesheet.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Liber;

[XmlRoot("stylesheet", Namespace = "http://www.w3.org/1999/XSL/Transform")]
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
