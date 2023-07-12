using Liber.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liber.Commands;

internal static class FormCommand
{
    public static Task ShellExecuteAsync(MainContext context, Type type)
    {
        Guid key = type.GUID;

        if (context.TryKill(key))
        {
            return Task.CompletedTask;
        }

        Form form = (Form)Activator.CreateInstance(type, context, key)!;

        context.Register(key, form);

        return Task.CompletedTask;
    }
}

internal sealed class FormCommand<TForm> : Command where TForm : Form
{
    public override string DisplayName
    {
        get
        {
            return FormattedStrings.GetString(typeof(TForm).Name + "CommandName");
        }
    }

    public override Task ExecuteAsync(MainContext context)
    {
        return FormCommand.ShellExecuteAsync(context, typeof(TForm));
    }
}
