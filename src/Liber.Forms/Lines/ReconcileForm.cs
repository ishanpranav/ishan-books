// ReconcileForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Liber.Forms.Transactions;

internal partial class ReconcileForm : Form
{
    private readonly decimal _reconciledBalance;
    private readonly decimal _endingBalance;
    private readonly Company _company;
    private readonly Account _account;

    private decimal _debit;
    private decimal _credit;
    private int _debitCount;
    private int _creditCount;

    public ReconcileForm(
        Company company,
        DateTime reconciled,
        decimal reconciledBalance,
        decimal endingBalance,
        Account account)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        Text = string.Format(Text, account.Name);
        reconciledDateTimePicker.Value = reconciled;
        reconciledBalanceNumericUpDown.Minimum = decimal.MinValue;
        reconciledBalanceNumericUpDown.Maximum = decimal.MaxValue;
        reconciledBalanceNumericUpDown.Value = account.Type.Toggle(reconciledBalance);
        debitNumericUpDown.Minimum = decimal.MinValue;
        debitNumericUpDown.Maximum = decimal.MaxValue;
        creditNumericUpDown.Minimum = decimal.MinValue;
        creditNumericUpDown.Maximum = decimal.MaxValue;
        endingBalanceNumericUpDown.Minimum = decimal.MinValue;
        endingBalanceNumericUpDown.Maximum = decimal.MaxValue;
        endingBalanceNumericUpDown.Value = account.Type.Toggle(endingBalance);
        nextReconciledBalanceNumericUpDown.Minimum = decimal.MinValue;
        nextReconciledBalanceNumericUpDown.Maximum = decimal.MaxValue;
        differenceNumericUpDown.Minimum = decimal.MinValue;
        differenceNumericUpDown.Maximum = decimal.MaxValue;
        _reconciledBalance = reconciledBalance;
        _endingBalance = endingBalance;
        _company = company;
        _account = account;

        IEnumerable<Line> unreconciledLines = account.Lines.Where(x => x.Reconciled == null && x.Transaction.Posted <= reconciled);

        debitListView.Initialize(unreconciledLines.Where(x => x.Debit > 0), AccountTypeExtensions.Debit, grouping: null);
        creditListView.Initialize(unreconciledLines.Where(x => x.Credit > 0), AccountTypeExtensions.Credit, grouping: null);
        InitializeBalances();
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Line[] lines = new Line[debitListView.CheckedItems.Count + creditListView.CheckedItems.Count];
        int i = 0;

        foreach (ListViewItem item in debitListView.Items)
        {
            lines[i] = (Line)item.Tag!;
            i++;
        }

        foreach (ListViewItem item in creditListView.Items)
        {
            lines[i] = (Line)item.Tag!;
            i++;
        }

        if (_company.Reconcile(_account, reconciledDateTimePicker.Value, _endingBalance, lines) != 0)
        {
            return;
        }

        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private void OnDebitListViewItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (e.CurrentValue == CheckState.Unchecked && e.NewValue == CheckState.Checked)
        {
            Line value = (Line)debitListView.Items[e.Index].Tag!;

            _debit += value.Debit;
            _debitCount++;

            InitializeBalances();

            return;
        }

        if (e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
        {
            Line value = (Line)debitListView.Items[e.Index].Tag!;

            _debit -= value.Debit;
            _debitCount--;

            InitializeBalances();
        }
    }

    private void OnCreditListViewItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (e.CurrentValue == CheckState.Unchecked && e.NewValue == CheckState.Checked)
        {
            Line line = (Line)creditListView.Items[e.Index].Tag!;

            _credit += line.Credit;
            _creditCount++;

            InitializeBalances();

            return;
        }

        if (e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
        {
            Line line = (Line)creditListView.Items[e.Index].Tag!;

            _credit -= line.Credit;
            _creditCount--;

            InitializeBalances();
        }
    }

    private void InitializeBalances()
    {
        debitLabel.Text = FormattedStrings.GetClearedDebitCount(_debitCount);
        creditLabel.Text = FormattedStrings.GetClearedCreditCount(_creditCount);
        debitNumericUpDown.Value = _debit;
        creditNumericUpDown.Value = _credit;

        decimal nextReconciledBalance = _reconciledBalance + _debit - _credit;

        nextReconciledBalanceNumericUpDown.Value = _account.Type.Toggle(nextReconciledBalance);

        decimal difference = _endingBalance - nextReconciledBalance;

        differenceNumericUpDown.Value = _account.Type.Toggle(difference);
        acceptButton.Enabled = difference == 0;
    }
}
