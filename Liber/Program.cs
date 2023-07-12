using Liber.Forms;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace Liber;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        Settings settings = ObjectLoader.LoadSettings();

        if (args.Length > 0)
        {
            settings.AddRecentItem(args[0]);
        }

        if (settings.Culture != null)
        {
            CultureInfo.CurrentCulture = settings.Culture;
            CultureInfo.CurrentUICulture = settings.Culture;
        }

        Application.Run(new MainForm(settings));
    }
}
