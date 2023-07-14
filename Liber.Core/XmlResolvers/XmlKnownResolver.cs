using System.IO;

namespace System.Xml.Resolvers;

internal sealed class XmlKnownResolver : XmlResolver
{
    private readonly XmlReaderSettings _settings;

    public XmlKnownResolver(XmlReaderSettings settings)
    {
        _settings = settings;
    }

    public override object? GetEntity(Uri absoluteUri, string? role, Type? ofObjectToReturn)
    {
        if (!absoluteUri.IsFile)
        {
            return null;
        }

        return XmlReader.Create(absoluteUri.LocalPath, _settings);
    }
}
