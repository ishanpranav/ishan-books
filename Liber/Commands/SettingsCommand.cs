using Liber.Forms;
using System;
using System.Threading.Tasks;

namespace Liber.Commands;

internal sealed class SettingsCommand : Command
{
    public override Task ExecuteAsync(MainContext context)
    {
        Guid key = new Guid("5B1517A0-9F3A-4796-8CB4-3A25CA528B57");

        if (context.TryKill(key))
        {
            return Task.CompletedTask;
        }

        context.Register(key, new SettingsForm(context.Settings));

        return Task.CompletedTask;
    }
}
