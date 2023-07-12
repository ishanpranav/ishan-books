using Liber.Forms;
using Liber.Properties;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liber.Commands.Companies;

internal sealed class SaveCompanyCommand : Command
{
    public override Task ExecuteAsync(MainContext context)
    {
        return ShellExecuteAsync(context);
    }

    public static async Task ShellExecuteAsync(MainContext context)
    {
        if (context.Path == null)
        {
            await SaveCompanyAsCommand.ShellExecuteAsync(context);

            return;
        }

        try
        {
            await ObjectLoader.SaveCompanyAsync(context.Path, context.Company);
        }
        catch (IOException ioException)
        {
            MessageBox.Show(ioException.Message, Resources.ExceptionCaption);
        }
        catch (JsonException jsonException)
        {
            MessageBox.Show(jsonException.Message, Resources.ExceptionCaption);
        }
    }
}
