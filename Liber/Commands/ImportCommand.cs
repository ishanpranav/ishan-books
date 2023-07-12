using Liber.Forms;
using Liber.Properties;
using Liber.Readers;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liber.Commands;

internal sealed class ImportCommand : Command, ICommandParent
{
    public IReadOnlyCollection<Command> GetChildren(MainContext context)
    {
        List<Command> results = new List<Command>();

        foreach (KeyValuePair<string, Reader> reader in context.Settings.Readers)
        {
            results.Add(new ChildCommand(reader.Key, reader.Value));
        }

        return results;
    }

    public static async Task ShellExecuteAsync(MainContext context, string path)
    {
        if (!context.Settings.Readers.TryGetValue(Path.GetExtension(path), out Reader? reader))
        {
            reader = new JsonCompanyReader();
        }

        try
        {
            await reader.ReadAsync(context, path);
        }
        catch (IOException ioException)
        {
            context.Settings.ClearRecentItems();
            
            _ = MessageBox.Show(ioException.Message, Resources.ExceptionCaption);
        }
        catch (JsonException jsonException)
        {
            context.Settings.ClearRecentItems();

            _ = MessageBox.Show(jsonException.Message, Resources.ExceptionCaption);
        }
    }

    private sealed class ChildCommand : Command
    {
        public ChildCommand(string defaultExtension, Reader reader)
        {
            DefaultExtension = defaultExtension;
            Reader = reader;
        }

        public override string DisplayName
        {
            get
            {
                return $"{Reader.DisplayName} (*.{DefaultExtension})";
            }
        }

        public string DefaultExtension { get; }
        public Reader Reader { get; }

        public override Task ExecuteAsync(MainContext context)
        {
            if (!context.Form.TryGetOpenPath(DefaultExtension, Reader.FilterIndex, out string? path))
            {
                return Task.CompletedTask;
            }

            return ShellExecuteAsync(context, path);
        }
    }
}
