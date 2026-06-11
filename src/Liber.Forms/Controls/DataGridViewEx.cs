// DataGridViewEx.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using Liber.Forms;

namespace System.Windows.Forms;

internal class DataGridViewEx : DataGridView
{
    public DataGridViewEx()
    {
        AllowUserToResizeColumns = true;
        AlternatingRowsDefaultCellStyle.BackColor = Color.Black.Tint(0.05);
        AlternatingRowsDefaultCellStyle.SelectionBackColor = Colors.Primary.Tint(0.15);
        AlternatingRowsDefaultCellStyle.SelectionForeColor = Colors.Black;
        BackgroundColor = Colors.Light;
        ColumnHeadersDefaultCellStyle.Font = new Font(Font, Drawing.FontStyle.Bold);
        DefaultCellStyle.SelectionBackColor = Colors.Primary.Tint(0.15);
        DefaultCellStyle.SelectionForeColor = Colors.Black;
        EditMode = DataGridViewEditMode.EditOnEnter;
        GridColor = Colors.Dark;

        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
    {
        base.OnColumnAdded(e);

        DataGridViewColumn column = e.Column;

        if (column is not DataGridViewTextBoxColumn)
        {
            return;
        }

        Type? valueType = column.ValueType;
        DataGridViewColumn newColumn;

        if (valueType == typeof(Color))
        {
            newColumn = new DataGridViewColorColumn();
        }
        else if (valueType == typeof(DateTime))
        {
            newColumn = new CalendarColumn();
        }
        else
        {
            return;
        }

        int index = column.Index;

        newColumn.Name = column.Name;
        newColumn.HeaderText = column.HeaderText;
        newColumn.DataPropertyName = column.DataPropertyName;
        newColumn.Visible = column.Visible;
        newColumn.ReadOnly = column.ReadOnly;
        newColumn.Width = column.Width;

        Columns.RemoveAt(index);
        Columns.Insert(index, newColumn);
    }
}
