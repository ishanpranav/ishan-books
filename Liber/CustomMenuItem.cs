using Liber.Commands;
using System.Windows.Forms;

namespace Liber;

internal sealed class CustomMenuItem
{
    public CustomMenuItem(Command command)
    {
        Command = command;
    }

    public Command Command { get; }
    public Keys ShortcutKeys { get; set; }
    public bool HasSeparator { get; set; }
}
