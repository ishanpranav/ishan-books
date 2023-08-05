// CheckDialog.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Liber.Forms.Lines;

internal sealed partial class CheckDialog : Form
{
    public CheckDialog(CheckView value)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        Value = value;

        _listView.BeginUpdate();

        foreach (KeyValuePair<Guid, Account> account in value.Company.Accounts)
        {
            ListViewGroup group = _listView.Groups.Add(account.Key.ToString(), account.Value.Name);

            foreach (Line line in account.Value.GetChecks())
            {
                Transaction transaction = line.Transaction!;
                ListViewItem item = _listView.Items.Add(transaction.Name ?? string.Empty);

                item.Group = group;
                item.Tag = line;

                item.SubItems.Add(transaction.Posted.ToShortDateString()).Tag = transaction.Posted;
                item.SubItems.Add(transaction.Number.ToString()).Tag = transaction.Number;
                item.SubItems.Add(line.Credit.ToLocalizedString()).Tag = line.Credit;
            }
        }

        _listView.AutoResizeColumns();
        _listView.EndUpdate();
    }

    public CheckView Value { get; private set; }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        if (_listView.SelectedItems.Count != 1)
        {
            return;
        }

        Value = new CheckView(Value.Company, (Line)_listView.SelectedItems[0].Tag);
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
