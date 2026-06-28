// FilterControl.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Liber.Filters;

namespace Liber.Forms.Filters;

internal partial class FilterControl : UserControl
{
    private Filter _value = new ConjunctionFilter();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Filter Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;

            InitializeFilter();
        }
    }

    public FilterControl()
    {
        InitializeComponent();
        Design.ApplyStyles(_contextMenu);

        Filter?[] filters = new Filter?[]
        {
            new TransactionNumberFilter(),
            new TransactionNameFilter(),
            new TransactionMemoFilter(),
            null,
            new BalanceFilter(),
            new DescriptionFilter(),
            null,
            new ReconciledFilter()
            {
                Value = true
            },
            new ReconciledFilter()
            {
                Value = false
            },
            null,
            new ConjunctionFilter()
            {
                Conjunction = Conjunction.And
            },
            new ConjunctionFilter()
            {
                Conjunction = Conjunction.Or
            },
        };

        foreach (Filter? filter in filters)
        {
            if (filter == null)
            {
                addToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

                continue;
            }

            ToolStripItem item = addToolStripMenuItem.DropDownItems.Add(filter.Name);

            item.Tag = filter;
            item.Click += OnAddToolStripMenuItemClick;
        }

        InitializeFilter();
    }

    private void InitializeFilter()
    {
        if (_value == null)
        {
            _treeView.Nodes.Clear();

            return;
        }

        _treeView.BeginUpdate();

        try
        {
            _treeView.Nodes.Clear();
            InitializeFilterInternal(_treeView.Nodes, _value);
            _treeView.ExpandAll();
        }
        finally
        {
            _treeView.EndUpdate();
        }
    }

    private static TreeNode InitializeFilterInternal(TreeNodeCollection nodes, Filter value)
    {
        TreeNode result = nodes.Add(value.Name);

        result.Tag = value;

        if (value is ConjunctionFilter parent)
        {
            foreach (Filter child in parent.Children)
            {
                InitializeFilterInternal(result.Nodes, child);
            }
        }

        return result;
    }

    private bool TryGetSelection([NotNullWhen(true)] out Filter? filter)
    {
        TreeNode? node = _treeView.SelectedNode;

        if (node == null)
        {
            filter = null;

            return false;
        }

        filter = (Filter)node.Tag!;

        return true;
    }

    private void OnAddToolStripMenuItemClick(object? sender, EventArgs e)
    {
        if (!TryGetSelection(out Filter? filter) || filter is not ConjunctionFilter parent)
        {
            return;
        }

        Filter child = ((Filter)((ToolStripItem)sender!).Tag!).Clone();

        parent.Children.Add(child);

        TreeNode node = _treeView.SelectedNode!;
        TreeNode childNode = InitializeFilterInternal(node.Nodes, child);

        node.Expand();

        _treeView.SelectedNode = childNode;
    }

    private void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
    {
        _propertyGrid.SelectedObject = e.Node?.Tag;
    }

    private void OnTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        _treeView.SelectedNode = e.Node;
    }

    private void OnEditToolStripMenuItemClick(object sender, EventArgs e)
    {
        _propertyGrid.SelectedObject = _treeView.SelectedNode?.Tag;
    }

    private void OnRemoveToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!TryGetSelection(out Filter? child) || _treeView.SelectedNode?.Parent?.Tag is not ConjunctionFilter parent)
        {
            return;
        }

        parent.Children.Remove(child);
        _treeView.SelectedNode!.Remove();
    }

    private void OnContextMenuOpening(object sender, CancelEventArgs e)
    {
        bool selected = TryGetSelection(out Filter? filter);

        addToolStripMenuItem.Enabled = selected && filter is ConjunctionFilter;
        editToolStripMenuItem.Enabled = selected;
        removeToolStripMenuItem.Enabled = selected && _treeView.SelectedNode?.Parent?.Tag is ConjunctionFilter;
    }

    private void OnPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        if (!TryGetSelection(out Filter? filter))
        {
            return;
        }

        TreeNode node = _treeView.SelectedNode!;

        node.Text = filter.Name;
    }
}
