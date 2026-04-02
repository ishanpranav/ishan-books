// Design.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using System.Windows.Forms;
using Liber.Forms.ProfessionalColorTables;

namespace Liber.Forms;

internal static class Design
{
    public static void Initialize()
    {
        ToolStripManager.Renderer = new ToolStripProfessionalRenderer(DesignProfessionalColorTable.Default);
    }

    public static void ApplyStyles(Control control)
    {
        control.BackColor = Colors.White;

        //if (control is TextBoxBase textBox)
        //{
        //    textBox.BorderStyle = BorderStyle.FixedSingle;
        //}

        //if (control is ButtonBase button)
        //{
        //    button.BackColor = Colors.Primary;
        //    button.ForeColor = Colors.Light;
        //    button.FlatStyle = FlatStyle.Flat;
        //    button.FlatAppearance.BorderColor = Colors.ButtonActiveBackground;
        //    button.FlatAppearance.CheckedBackColor = Colors.ButtonActiveBackground;
        //    button.FlatAppearance.MouseOverBackColor = Colors.ButtonHoverBackground;
        //    button.FlatAppearance.MouseDownBackColor = Colors.ButtonActiveBackground;
        //    button.FlatAppearance.BorderSize = 1;
        //}

        if (control is DataGridView dataGridView)
        {
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Black.Tint(0.05);
            dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = Colors.Primary.Tint(0.15);
            dataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = Colors.Black;
            dataGridView.BackgroundColor = Colors.Light;
            dataGridView.DefaultCellStyle.SelectionBackColor = Colors.Primary.Tint(0.15);
            dataGridView.DefaultCellStyle.SelectionForeColor = Colors.Black;
        }

        if (control is PropertyGrid propertyGrid)
        {
            propertyGrid.SelectedItemWithFocusBackColor = Colors.Primary.Tint(0.15);
            propertyGrid.SelectedItemWithFocusForeColor = Colors.Black;
        }

        if (control is ToolStrip toolStrip)
        {
            toolStrip.Renderer = new ToolStripProfessionalRenderer(DesignProfessionalColorTable.Default);

            if (toolStrip is MenuStrip || toolStrip is StatusStrip)
            {
                toolStrip.BackColor = Colors.Primary;
                toolStrip.ForeColor = Colors.Light;
            }
        }

        //if (control is TabControl tabControl)
        //{
        //    foreach (TabPage tabPage in tabControl.TabPages)
        //    {
        //        ApplyStyles(tabPage);
        //    }

        //    return;
        //}

        foreach (Control child in control.Controls)
        {
            ApplyStyles(child);
        }
    }

    public static void PostSetButtonBackColor(ButtonBase button, Color value)
    {
        //Color activeBackground = value.Shade(0.2);

        //button.ForeColor = value.GetForeColor();
        //button.FlatAppearance.BorderColor = activeBackground;
        //button.FlatAppearance.CheckedBackColor = activeBackground;
        //button.FlatAppearance.MouseOverBackColor = value.Shade(0.15);
        //button.FlatAppearance.MouseDownBackColor = activeBackground;
    }
}
