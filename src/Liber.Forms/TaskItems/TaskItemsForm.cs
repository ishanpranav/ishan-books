// TaskItemsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Windows.Forms;
using Humanizer;
using Liber.Forms.Forms;
using Liber.Forms.Properties;
using Liber.Forms.TaskItems.Reconciled;

namespace Liber.Forms.TaskItems;

internal partial class TaskItemsForm : Form
{
    private readonly Company _company;
    private readonly FormFactory _factory;

    public TaskItemsForm(Company company, FormFactory factory)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _company = company;
        _factory = factory;

        InitializeTaskItems();
    }

    private void InitializeTaskItems()
    {
        _listView.BeginUpdate();

        try
        {
            _listView.Items.Clear();

            foreach (Account account in _company.Accounts)
            {
                InitializeAccount(account);
            }

            _listView.AutoResizeColumns();
            _listView.Sort();
        }
        finally
        {
            _listView.EndUpdate();
        }

        countStatusLabel.Text = Properties.Resources.TaskItems.ToQuantity(_listView.Items.Count);
    }

    private void AddTaskItem(TaskItem value)
    {
        ListViewItem item = _listView.Items.Add(value.Description);

        item.Tag = value;

        item.SubItems.Add(value.Priority.ToString());
    }

    private void InitializeAccount(Account value)
    {
        if (value.Type.IsTemporary() || value.ReadOnly)
        {
            return;
        }

        DateTime? reconciled = value.Reconciled;

        if (reconciled == null)
        {
            if (value.Lines.Count > 0)
            {
                AddTaskItem(new NullReconciledTaskItem(_company, _factory, value));
            }
        }
        else if (value.Lines.Any(x => x.Reconciled == null))
        {
            TimeSpan overdue = DateTime.Today - reconciled.Value;

            if (overdue > Settings.Default.OverdueReconciled)
            {
                AddTaskItem(new OverdueReconciiledTaskItem(_company, _factory, value, overdue));
            }
        }
        else
        {
            TimeSpan overdue = DateTime.Today - value.Lines.Max(x => x.Transaction.Posted);

            if (overdue > Settings.Default.OverduePosted)
            {
                AddTaskItem(new OverduePostedTaskItem(_company, _factory, value, overdue));
            }
        }
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        if (_listView.SelectedItems.Count > 0)
        {
            TaskItem taskItem = (TaskItem)_listView.SelectedItems[0].Tag!;

            taskItem.Begin();
        }
    }

    private void OnRefreshToolStripMenuItemClick(object sender, EventArgs e)
    {
        InitializeTaskItems();
    }
}
