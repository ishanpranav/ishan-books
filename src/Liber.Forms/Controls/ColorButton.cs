// ColorButton.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Liber;
using Liber.Forms;
using Liber.Forms.Properties;

namespace System.Windows.Forms;

internal sealed class ColorButton : Button
{
    protected override void OnClick(EventArgs e)
    {
        using ColorDialog colorDialog = new ColorDialog()
        {
            SolidColorOnly = true
        };

        colorDialog.CustomColors = JsonSerializer.Deserialize<int[]>(Settings.Default.CustomColors, FormattedStrings.JsonOptions);

        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            BackColor = colorDialog.Color;
            ForeColor = Colors.GetForeColor(colorDialog.Color);
            Settings.Default.CustomColors = JsonSerializer.Serialize(colorDialog.CustomColors);

            Settings.Default.Save();
        }

        base.OnClick(e);
    }
}
