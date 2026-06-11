// CalendarEditingControl.cs
// Licensed under the MIT License.

/*
 The MIT License (MIT)

 Copyright (c) .NET Foundation and Contributors

 All rights reserved.

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
*/

using System.ComponentModel;

namespace System.Windows.Forms;

#nullable disable
internal class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataGridView EditingControlDataGridView { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object EditingControlFormattedValue
    {
        get
        {
            return Value.ToShortDateString();
        }
        set
        {
            if (value is string text && DateTime.TryParse(text, out DateTime result))
            {
                Value = result;
            }
            else
            {
                Value = DateTime.Now;
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int EditingControlRowIndex { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EditingControlValueChanged { get; set; }

    public Cursor EditingPanelCursor
    {
        get
        {
            return Cursor;
        }
    }

    public bool RepositionEditingControlOnValueChange
    {
        get
        {
            return false;
        }
    }

    public CalendarEditingControl()
    {
        Format = DateTimePickerFormat.Short;
    }

    public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
        Font = dataGridViewCellStyle.Font;
        CalendarForeColor = dataGridViewCellStyle.ForeColor;
        CalendarMonthBackground = dataGridViewCellStyle.BackColor;
    }

    public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
    {
        switch (key & Keys.KeyCode)
        {
            case Keys.Left:
            case Keys.Up:
            case Keys.Down:
            case Keys.Right:
            case Keys.Home:
            case Keys.End:
            case Keys.PageDown:
            case Keys.PageUp:
                return true;
        }

        return !dataGridViewWantsInputKey;
    }

    public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {
        return EditingControlFormattedValue;
    }

    public void PrepareEditingControlForEdit(bool selectAll) { }

    protected override void OnValueChanged(EventArgs eventargs)
    {
        EditingControlValueChanged = true;

        EditingControlDataGridView.NotifyCurrentCellDirty(true);
        base.OnValueChanged(eventargs);
    }
}
#nullable enable
