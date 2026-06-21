// SplashScreen.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Liber.Forms.Help;

internal partial class SplashScreen : Form
{
    private bool _mouseDown;
    private Point _lastLocation;

    public SplashScreen()
    {
        InitializeComponent();

        ForeColor = Colors.Primary.GetForeColor();
        applicationNameLabel.Text = SystemFeatures.ApplicationName;
        companyLabel.Text = SystemFeatures.ApplicationCompany;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        _mouseDown = true;
        _lastLocation = e.Location;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (_mouseDown)
        {
            Location = new Point(Location.X - _lastLocation.X + e.X, Location.Y - _lastLocation.Y + e.Y);

            Update();
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        _mouseDown = false;
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        using (LinearGradientBrush brush = new LinearGradientBrush(
            ClientRectangle,
            Colors.Primary,
            Colors.Primary.Shade(0.15),
            LinearGradientMode.ForwardDiagonal))
        {
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }
    }
}
