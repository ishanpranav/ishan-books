// TaxesForm.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.TaxNodes;

namespace Liber.Forms.Taxes;

internal partial class TaxesForm : Form
{
    private Tax? _tax;
    private Company _company;
    
    public TaxesForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _company = company;
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        using (FileStream input = File.OpenRead(Path.Combine("data", Path.ChangeExtension("tax", "json"))))
        {
            _tax = await JsonSerializer.DeserializeAsync<Tax>(input, new JsonSerializerOptions(FormattedStrings.JsonOptions)
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
        }

        if (_tax == null)
        {
            _tax = new Tax();
        }

        TaxView view = new TaxView(_tax);

        _propertyGrid.SelectedObject = view;

        foreach (TaxComponent form in _tax.Forms)
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
            grid.CellContentClick += OnDataGridViewCellContentClick;

            page.Controls.Add(grid);
            _tabControl.TabPages.Add(page);

            foreach (TaxNode line in form.Lines)
            {
                int index = grid.Rows.Add(line.Name, line.Description, null, GetEditString(line));

                line.Evaluated += (sender, e) =>
                {
                    grid.Rows[index].Cells[2].Value = e.Value;
                };

                grid.Rows[index].Tag = line;

                if (line is AccountTaxNode accountLine && accountLine.TaxType != null)
                {
                    accountLine.Accounts = _company.Accounts.Values
                        .Where(x => x.TaxType == accountLine.TaxType)
                        .ToHashSet();
                }
            }

            grid.AutoResizeColumns();
            grid.Refresh();
        }

        TabPage swap = _tabControl.TabPages[0];

        _tabControl.TabPages.RemoveAt(0);
        _tabControl.TabPages.Add(swap);
        SelectTab(_tabControl.TabPages[0]);
        Evaluate();
    }

    private void Evaluate()
    {
        TaxView view = (TaxView)_propertyGrid.SelectedObject;

        _tax?.Evaluate(view.Started, view.Posted);
    }

    private static void SelectTab(TabPage page)
    {
        if (page.Tag is TaxComponent form)
        {
            page.Text = form.FullName;
        }
    }

    private static string GetEditString(TaxNode line)
    {
        if (line is AccountTaxNode)
        {
            return "Edit";
        }

        return "Override";
    }

    private void OnTabControlSelected(object sender, TabControlEventArgs e)
    {
        if (e.TabPage == null)
        {
            return;
        }

        SelectTab(e.TabPage);
    }

    private void OnTabControlDeselected(object sender, TabControlEventArgs e)
    {
        if (e.TabPage == null || e.TabPage.Tag is not TaxComponent form)
        {
            return;
        }

        e.TabPage.Text = form.Name;
    }

    private void OnPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
    {
        Evaluate();
    }

    private void OnDataGridViewCellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != 3)
        {
            return;
        }

        DataGridView grid = (DataGridView)sender!;
        TaxNode line = (TaxNode)grid.Rows[e.RowIndex].Tag!;

        if (line is AccountTaxNode accountLine)
        {
            using AccountsDialog form = new AccountsDialog(new AccountsView(_company, accountLine.Accounts));

            if (form.ShowDialog() == DialogResult.OK)
            {
                accountLine.Accounts = form.Value.Values;

                Evaluate();
            }
        }
    }
}
