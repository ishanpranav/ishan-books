// Program.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Liber.Forms.Properties;

namespace Liber.Forms;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        if (!string.IsNullOrWhiteSpace(Settings.Default.Culture))
        {
            CultureInfo.CurrentUICulture = new CultureInfo(Settings.Default.Culture);
        }

        IReadOnlyList<string> arguments;

        if (ClickOnce.IsNetworkDeployed)
        {
            arguments = ClickOnce.GetArguments();
        }
        else
        {
            arguments = args;
        }

        if (arguments.Count > 0)
        {
            Application.Run(new MainForm(arguments[0]));
        }
        else
        {
            Application.Run(new MainForm());
        }
    }
}
