// ColorButton.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using System.Text.Json;
using Liber.Forms;
using Liber.Forms.Properties;

namespace System.Windows.Forms;

internal sealed class ColorButton : Button
{
    public override Color BackColor
    {
        get
        {
            return base.BackColor;
        }
        set
        {
            base.BackColor = value;

            Liber.Forms.Design.PostSetButtonBackColor(this, value);
        }
    }

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
            Settings.Default.CustomColors = JsonSerializer.Serialize(colorDialog.CustomColors);

            Settings.Default.Save();
        }

        base.OnClick(e);
    }
}
