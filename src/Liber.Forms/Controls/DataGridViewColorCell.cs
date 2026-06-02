// DataGridViewColorCell.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using Liber.Forms;
using Liber.Forms.Controls;

namespace System.Windows.Forms;

internal class DataGridViewColorCell : DataGridViewTextBoxCell
{
    public override void InitializeEditingControl(int rowIndex, object? initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle) { }

    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object? value, object? formattedValue, string? errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue,
            errorText, cellStyle, advancedBorderStyle,
            paintParts & ~DataGridViewPaintParts.ContentForeground);

        if (value is not Color color)
        {
            return;
        }

        Rectangle swatch = new Rectangle(
            cellBounds.X + 4,
            cellBounds.Y + (cellBounds.Height - 16) / 2,
            width: 16,
            height: 16);

        using Brush brush = new SolidBrush(color);

        graphics.FillRectangle(brush, swatch);

        using Pen pen = new Pen(Colors.Gray);

        graphics.DrawRectangle(pen, swatch);

        ButtonRenderer.DrawButton(
            graphics,
            GetButtonBounds(cellBounds),
            "…",
            DataGridView?.Font,
            focused: false,
            VisualStyles.PushButtonState.Normal);
    }

    private static Rectangle GetButtonBounds(Rectangle cellBounds)
    {
        return new Rectangle(
            cellBounds.Right - 24,
            cellBounds.Y + 1,
            width: 23,
            height: cellBounds.Height - 2);
    }

    protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (e.RowIndex < 0 || DataGridView == null || e.Button != MouseButtons.Left)
        {
            return;
        }

        try
        {
            Color value = (Color?)GetValue(e.RowIndex) ?? Color.Empty;

            ColorDialogManager.ShowDialog(ref value);

            SetValue(e.RowIndex, value);
        }
        catch { }
        finally
        {
            DataGridView.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }
    }
}
