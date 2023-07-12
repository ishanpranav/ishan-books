using Liber.Forms;
using System.Threading.Tasks;

namespace Liber.Commands;

internal sealed class ErrorsCommand : Command
{
    public override Task ExecuteAsync(MainContext context)
    {
        context.ReportErrors();

        return Task.CompletedTask;
    }
}
