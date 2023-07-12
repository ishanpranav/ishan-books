using Liber.Forms;
using Liber.Properties;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liber.Commands.Companies;

internal sealed class SaveCompanyAsCommand : Command
{
    public override Task ExecuteAsync(MainContext context)
    {
        return ShellExecuteAsync(context);
    }

    public static async Task ShellExecuteAsync(MainContext context)
    {
        if (!context.Form.TryGetSavePath(out string? path))
        {
            return;
        }

        try
        {
            await ObjectLoader.SaveCompanyAsync(path, context.Company);

            context.Path = path;

            context.Settings.AddRecentItem(path);
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
