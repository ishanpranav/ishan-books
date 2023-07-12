using Liber.Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace Liber.Commands;

internal sealed class UrlCommand : Command
{
    public UrlCommand(string name, string destination)
    {
        Name = name;
        Destination = destination;
    }

    public string Name { get; }
    public string Destination { get; }

    public override string DisplayName
    {
        get
        {
            return FormattedStrings.GetString(Name + "Name");
        }
    }

    public override Image? Image
    {
        get
        {
            return FormattedStrings.GetImage(Name + "Image");
        }
    }

    public override Task ExecuteAsync(MainContext context)
    {
        context.Register(
            Guid.NewGuid(),
            new UrlForm(new Uri(string.Format(Destination, context.Settings.Culture))));

        return Task.CompletedTask;
    }
}
