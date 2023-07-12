using Liber.Forms;
using System.Threading.Tasks;

namespace Liber.Commands.Companies;

internal sealed class OpenCompanyCommand : Command
{
    public override async Task ExecuteAsync(MainContext context)
    {
        if (await context.Form.TryCancelAsync())
        {
            return;
        }

        if (!context.Form.TryGetOpenPath(".json", filterIndex: 2, out string? path))
        {
            return;
        }

        await ImportCommand.ShellExecuteAsync(context, path);
    }
}
