using Liber.Forms.Properties;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace Liber.Forms;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        if (!string.IsNullOrWhiteSpace(Settings.Default.Culture))
        {
            CultureInfo culture = new CultureInfo(Settings.Default.Culture);

            CultureInfo.CurrentUICulture = culture;
        }

        Application.Run(new MainForm(args));
    }
}
