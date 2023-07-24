// Program.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
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
            CultureInfo culture = new CultureInfo(Settings.Default.Culture);

            CultureInfo.CurrentUICulture = culture;
        }

        Application.Run(new MainForm(args));
    }
}
