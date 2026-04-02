// DesignProfessionalColorTable.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using System.Windows.Forms;

namespace Liber.Forms.ProfessionalColorTables;

internal sealed class DesignProfessionalColorTable : ProfessionalColorTable
{
    private static DesignProfessionalColorTable? s_instance;

    public static DesignProfessionalColorTable Default
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new DesignProfessionalColorTable();
            }

            return s_instance;
        }
    }

    public override Color MenuStripGradientBegin
    {
        get
        {
            return Colors.Primary;
        }
    }

    public override Color MenuStripGradientEnd
    {
        get
        {
            return Colors.Primary;
        }
    }

    public override Color StatusStripGradientBegin
    {
        get
        {
            return Colors.Primary;
        }
    }

    public override Color StatusStripGradientEnd
    {
        get
        {
            return Colors.Primary;
        }
    }

    public override Color ButtonSelectedGradientBegin
    {
        get
        {
            return Colors.Primary.Tint(0.15);
        }
    }

    public override Color ButtonSelectedGradientMiddle
    {
        get
        {
            return Colors.Primary.Tint(0.15);
        }
    }

    public override Color ButtonSelectedGradientEnd
    {
        get
        {
            return Colors.Primary.Tint(0.15);
        }
    }

    public override Color ButtonPressedBorder
    {
        get
        {
            return Colors.Primary;
        }
    }

    public override Color ButtonPressedGradientBegin
    {
        get
        {
            return Colors.Primary.Tint(0.2);
        }
    }

    public override Color ButtonPressedGradientMiddle
    {
        get
        {
            return Colors.Primary.Tint(0.2);
        }
    }

    public override Color ButtonPressedGradientEnd
    {
        get
        {
            return Colors.Primary.Tint(0.2);
        }
    }

    public override Color MenuItemBorder
    {
        get
        {
            return Colors.Primary;
        }
    }

    public override Color MenuItemSelected
    {
        get
        {
            return Colors.Primary.Tint(0.15);
        }
    }

    public override Color MenuItemSelectedGradientBegin
    {
        get
        {
            return Colors.Primary.Tint(0.15);
        }
    }

    public override Color MenuItemSelectedGradientEnd
    {
        get
        {
            return Colors.Primary.Tint(0.15);
        }
    }

    public override Color MenuItemPressedGradientBegin
    {
        get
        {
            return Colors.ButtonActiveBackground;
        }
    }

    public override Color MenuItemPressedGradientMiddle
    {
        get
        {
            return Colors.ButtonActiveBackground;
        }
    }

    public override Color MenuItemPressedGradientEnd
    {
        get
        {
            return Colors.ButtonActiveBackground;
        }
    }

    public override Color MenuBorder
    {
        get
        {
            return Colors.Primary;
        }
    }

    public override Color SeparatorLight
    {
        get
        {
            return Colors.Light;
        }
    }

    public override Color SeparatorDark
    {
        get
        {
            return Colors.Gray;
        }
    }
}
