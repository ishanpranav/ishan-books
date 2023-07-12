using Liber.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liber.Commands.Companies;

internal sealed class OpenRecentCompanyCommand : Command, ICommandParent
{
    public IReadOnlyCollection<Command> GetChildren(MainContext context)
    {
        List<ChildCommand> results = new List<ChildCommand>();
        int number = 1;

        foreach (RecentItem recentItem in context.Settings.RecentItems)
        {
            results.Add(new ChildCommand(recentItem.Path)
            {
                Number = number
            });

            number++;
        }

        return results;
    }

    private sealed class ChildCommand : Command
    {
        public ChildCommand(string path)
        {
            Path = path;
        }

        public string Path { get; }
        public int Number { get; set; }

        public override string DisplayName
        {
            get
            {
                return Number + " " + Path;
            }
        }

        public override async Task ExecuteAsync(MainContext context)
        {
            if (await context.Form.TryCancelAsync())
            {
                return;
            }

            await ImportCommand.ShellExecuteAsync(context, Path);
        }
    }
}
