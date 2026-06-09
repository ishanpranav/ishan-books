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
        DefaultCellStyle.SelectionBackColor = Colors.Primary.Tint(0.15);
        DefaultCellStyle.SelectionForeColor = Colors.Black;

        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
    {
        base.OnColumnAdded(e);

        if (e.Column is DataGridViewColorColumn || e.Column.ValueType != typeof(Color))
        {
            return;
        }

        int index = e.Column.Index;
        string name = e.Column.Name;
        string headerText = e.Column.HeaderText;
        string dataPropertyName = e.Column.DataPropertyName;
        bool visible = e.Column.Visible;
        bool readOnly = e.Column.ReadOnly;
        int width = e.Column.Width;

        Columns.RemoveAt(index);

        Columns.Insert(index, new DataGridViewColorColumn()
        {
            Name = name,
            HeaderText = headerText,
            DataPropertyName = dataPropertyName,
            ReadOnly = readOnly,
            Visible = visible,
            Width = width,
        });
    }
}
