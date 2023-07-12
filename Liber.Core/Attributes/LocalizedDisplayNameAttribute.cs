using System.Resources;

namespace System.ComponentModel;

internal sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(LocalizedDisplayNameAttribute));

    public LocalizedDisplayNameAttribute(string key)
    {
        DisplayNameValue = s_resourceManager.GetString(key) ?? key;
    }
}
