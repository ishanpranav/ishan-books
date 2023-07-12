using Liber.Forms;
using System.Threading.Tasks;

namespace Liber.Commands;

internal sealed class CloseCommand : Command
{
    public override Task ExecuteAsync(MainContext context)
    {
        context.Form.Close();

        return Task.CompletedTask;
    }
}
