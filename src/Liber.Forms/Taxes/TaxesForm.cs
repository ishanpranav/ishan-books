// TaxesForm.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Windows.Forms;

namespace Liber.Forms.Taxes;

public partial class TaxesForm : Form
{
    private TaxComponent _f1040 = new TaxComponent("2023 Form 1040", "U.S. Individual Income Tax Return");
    private TaxComponent[] _forms;

    public TaxesForm()
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

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
                Dock = DockStyle.Fill
            };

            page.Controls.Add(grid);
            componentTabControl.TabPages.Add(page);

            foreach (TaxNode line in form.Lines)
            {

            }
        }
    }

    private void OnComponentTabControlSelected(object sender, TabControlEventArgs e)
    {
        if (e.TabPage == null)
        {
            return;
        }

        e.TabPage.Text = ((TaxComponent)e.TabPage.Tag!).FullName;
    }

    private void OnComponentTabControlDeselected(object sender, TabControlEventArgs e)
    {
        if (e.TabPage == null)
        {
            return;
        }

        e.TabPage.Text = ((TaxComponent)e.TabPage.Tag!).Name;
    }
}
