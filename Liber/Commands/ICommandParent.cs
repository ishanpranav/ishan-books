using Liber.Forms;
using System.Collections.Generic;

namespace Liber.Commands;

internal interface ICommandParent
{
    IReadOnlyCollection<Command> GetChildren(MainContext context);
}
