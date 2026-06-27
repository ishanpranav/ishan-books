// ColorDialogManager.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;
using Liber.Forms.Properties;

namespace Liber.Forms.Controls;

internal static class ColorDialogManager
{
    public static DialogResult ShowDialog(ref Color value)
    {
        using ColorDialog colorDialog = new ColorDialog()
        {
            Color = value,
            CustomColors = JsonSerializer.Deserialize<int[]>(Settings.Default.CustomColors, SerializationOptions.Json),
            FullOpen = true,
            SolidColorOnly = true
        };

        DialogResult result = colorDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            value = colorDialog.Color;

            Settings.Default.CustomColors = JsonSerializer.Serialize(colorDialog.CustomColors);

            Settings.Default.Save();
        }

        return result;
    }
}
