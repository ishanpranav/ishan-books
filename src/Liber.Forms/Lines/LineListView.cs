// LineListView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Liber.Properties;

namespace Liber.Forms.Lines;

internal class LineListView : ListViewEx
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ListViewItemCollection Items
    {
        get
        {
            return base.Items;
        }
    }

    public LineListView()
    {
        View = View.Details;
        FullRowSelect = true;

        Columns.Add(Properties.Resources.Posted);
        Columns.Add(Resources.Number);
        Columns.Add(Resources.Name);
        Columns.Add(Resources.DebitCredit);
    }

    public void Initialize(IEnumerable<Line> lines, AccountType representativeType, Func<Line, (string, string)>? grouping)
    {
        BeginUpdate();

        try
        {
            Items.Clear();

            foreach (Line line in lines)
            {
                Transaction transaction = line.Transaction;
                ListViewItem item = Items.Add(transaction.Posted.ToShortDateString());

                item.Tag = line;

                if (grouping != null)
                {
                    (string key, string name) = grouping(line);

                    item.Group = Groups[key] ?? Groups.Add(key, name);
                }

                item.SubItems.Add(transaction.Number.ToStringOrEmpty()).Tag = transaction.Number;
                item.SubItems.Add(transaction.Name).Tag = transaction.Name;
                item.SubItems.Add(representativeType.Toggle(line.Balance).ToLocalizedString()).Tag = line.Balance;
            }

            AutoResizeColumns();
        }
        finally
        {
            EndUpdate();
        }
    }

    public bool TryGetSelection([NotNullWhen(true)] out Line? value)
    {
        if (SelectedItems.Count < 1)
        {
            value = null;

            return false;
        }

        value = (Line)SelectedItems[0].Tag!;

        return true;
    }
}
