// DataGridViewColorColumn.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

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
            if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewColorCell)))
            {
                throw new InvalidCastException("Must be a DataGridViewColorCell");
            }

            base.CellTemplate = value;
        }
    }

    public DataGridViewColorColumn() : base(new DataGridViewColorCell()) { }
}
