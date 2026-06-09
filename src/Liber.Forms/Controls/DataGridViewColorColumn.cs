// DataGridViewColorColumn.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;

namespace System.Windows.Forms;

public class DataGridViewColorColumn : DataGridViewColumn
{
    public override DataGridViewCell? CellTemplate
    {
        get
        {
            return base.CellTemplate;
        }
        set
        {
            if (value is not DataGridViewColorCell)
            {
                throw new InvalidOperationException();
            }

            base.CellTemplate = value;
        }
    }

    public DataGridViewColorColumn() : base(new DataGridViewColorCell())
    {
        ValueType = typeof(Color);
        Width = 80;
        SortMode = DataGridViewColumnSortMode.NotSortable;
    }
}
