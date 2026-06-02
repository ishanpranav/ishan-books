// ColorButton.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using Liber.Forms.Controls;

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
            ForeColor = value.GetForeColor();

            Liber.Forms.Design.PostSetButtonBackColor(this, value);
        }
    }

    protected override void OnClick(EventArgs e)
    {
        Color backColor = BackColor;

        ColorDialogManager.ShowDialog(ref backColor);

        BackColor = backColor;

        base.OnClick(e);
    }
}
