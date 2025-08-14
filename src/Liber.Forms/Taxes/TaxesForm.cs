// TaxesForm.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Windows.Forms;
using Liber.TaxNodes;

namespace Liber.Forms.Taxes;

public partial class TaxesForm : Form
{
    private TaxNode[] _lines = new TaxNode[]
    {
        new NotImplementedTaxNode("1a", "Total amount from Form(s) W-2, box 1")
    };
    private TaxComponent _f1040;
    private TaxComponent[] _forms;

    public TaxesForm()
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _f1040 = new TaxComponent("2023 Form 1040", "U.S. Individual Income Tax Return", _lines.Take(1).ToList());
        _forms = new TaxComponent[]
        {
            _f1040
        };

        foreach (TaxComponent form in _forms)
        {
            TabPage page = new TabPage(form.Name)
            {
                Tag = form
            };
            DataGridView grid = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToOrderColumns = false
            };
            DataGridViewTextBoxColumn valueColumn = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Value",
                ReadOnly = true
            };

            valueColumn.ValueType = typeof(decimal);
            valueColumn.DefaultCellStyle.Format = DecimalExtensions.Format;

            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Frozen = true,
                HeaderText = "Line",
                ReadOnly = true
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Description",
                ReadOnly = true
            });
            grid.Columns.Add(valueColumn);
            grid.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "Action"
            });

            page.Controls.Add(grid);
            componentTabControl.TabPages.Add(page);

            foreach (TaxNode line in form.Lines)
            {
                grid.Rows.Add(line.Name, line.Description);
            }

            grid.AutoResizeColumns();
            grid.Refresh();
        }

        TabPage swap = componentTabControl.TabPages[0];

        componentTabControl.TabPages.RemoveAt(0);
        componentTabControl.TabPages.Add(swap);
        SelectTab(componentTabControl.TabPages[0]);
    }

    private void Evaluate()
    {
        foreach (TaxNode line in _lines)
        {
            line.Evaluate();
        }
    }

    private static void SelectTab(TabPage page)
    {
        if (page.Tag is TaxComponent form)
        {
            page.Text = form.FullName;
        }
    }

    private void OnComponentTabControlSelected(object sender, TabControlEventArgs e)
    {
        if (e.TabPage == null)
        {
            return;
        }

        SelectTab(e.TabPage);
    }

    private void OnComponentTabControlDeselected(object sender, TabControlEventArgs e)
    {
        if (e.TabPage == null || e.TabPage.Tag is not TaxComponent form)
        {
            return;
        }

        e.TabPage.Text = form.Name;
    }
}
