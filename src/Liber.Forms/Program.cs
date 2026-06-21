// Program.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.Help;
using Liber.Forms.Properties;

namespace Liber.Forms;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        try
        {
            ApplicationConfiguration.Initialize();
            SystemFeatures.Initialize();

            if (!string.IsNullOrWhiteSpace(Settings.Default.Culture))
            {
                CultureInfo culture = new CultureInfo(Settings.Default.Culture);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
                Application.CurrentCulture = culture;
            }

            SplashScreen splashScreen = new SplashScreen();

            splashScreen.Show();
            splashScreen.Refresh();

            IReadOnlyList<string> arguments;

            if (SystemFeatures.IsNetworkDeployed)
            {
                arguments = SystemFeatures.GetArguments();
            }
            else
            {
                arguments = args;
            }

            MainForm mainForm = arguments.Count > 0 ? new MainForm(arguments[0]) : new MainForm();

            mainForm.Loaded += (_, _) => splashScreen.Close();

            Application.Run(mainForm);
        }
        finally
        {
            AccountImageListManager.ImageList.Dispose();
        }
    }
}
