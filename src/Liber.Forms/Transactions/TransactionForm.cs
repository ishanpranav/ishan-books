// TransactionForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Liber.Forms.AccountViews;
using Liber.Forms.Properties;
using Liber.MathEngine.Expressions;

namespace Liber.Forms.Transactions;

internal partial class TransactionForm : Form
{
    private readonly Company _company;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowApplyButton
    {
        get
        {
            return applyButton.Enabled;
        }
        set
        {
            applyButton.Enabled = value;
        }
    }

    public Transaction? Value { get; private set; }

    public TransactionForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        accountColumn.DataSource = new AccountViewBindingList(company, x => !x.ReadOnly);
        accountColumn.ValueMember = nameof(AccountView.Id);
        accountColumn.DisplayMember = nameof(AccountView.DisplayName);
        _dataGridView.SetCompanyColor(company.Color);
        _dataGridView.DebitColumnIndex = debitColumn.Index;
        _dataGridView.CreditColumnIndex = creditColumn.Index;
        _dataGridView.Remainder = GetRemainder;
        company.TransactionUpdated += OnCompanyTransactionUpdated;
        company.TransactionReconciled += OnCompanyTransactionReconciled;
        company.TransactionRemoved += OnCompanyTransactionRemoved;
        _company = company;
        DialogResult = DialogResult.Cancel;
        numberNumericUpDown.Maximum = decimal.MaxValue;
        nameComboBox.DataSource = company.GetNames();
        _dataGridView.AutoResizeColumns();
        CreateNew();
    }

    private void OnCompanyTransactionUpdated(object? sender, GuidEventArgs e)
    {
        if (Value != null && e.Id == Value.Id)
        {
            InitializeTransaction(_company.GetTransaction(e.Id));
        }
    }

    private void OnCompanyTransactionReconciled(object? sender, GuidEventArgs e)
    {
        if (Value != null && e.Id != Value.Id)
        {
            InitializeTransaction(_company.GetTransaction(e.Id));
        }
    }

    private void OnCompanyTransactionRemoved(object? sender, GuidEventArgs e)
    {
        if (Value != null && e.Id == Value.Id)
        {
            CreateNew();
        }
    }

    [MemberNotNull(nameof(Value))]
    public void InitializeTransaction(Transaction transaction)
    {
        Value = transaction;
        numberNumericUpDown.Value = transaction.Number;
        postedDateTimePicker.Value = transaction.Posted;
        nameComboBox.Text = transaction.Name;
        memoTextBox.Text = transaction.Memo;

        _dataGridView.Rows.Clear();

        foreach (Line line in transaction.Lines)
        {
            _dataGridView.Rows.Add(
                line.AccountId,
                new DecimalExpression(line.Debit),
                new DecimalExpression(line.Credit),
                line.Description ?? string.Empty,
                line.Reconciled != null);
        }

        _dataGridView.AutoResizeColumns();
    }

    private decimal GetRemainder()
    {
        decimal result = 0;

        foreach (DataGridViewRow row in _dataGridView.Rows)
        {
            if (row.IsNewRow || row.ErrorText != string.Empty || !_dataGridView.TryGetBalance(row, out decimal balance))
            {
                continue;
            }

            result -= balance;
        }

        return result;
    }

    [MemberNotNullWhen(true, nameof(Value))]
    private bool Save()
    {
        bool addingNew;
        Transaction transaction;

        if (Value == null)
        {
            transaction = new Transaction();
            addingNew = true;
        }
        else
        {
            transaction = Value;
            addingNew = false;
        }

        transaction.Memo = memoTextBox.Text;

        string? name = nameComboBox.Text;
        List<Line> lines = new List<Line>();
        decimal trialBalance = 0;

        foreach (DataGridViewRow row in _dataGridView.Rows)
        {
            row.ErrorText = string.Empty;

            if (row.IsNewRow)
            {
                continue;
            }

            if (row.Cells[accountColumn.Index].Value is not Guid accountId ||
                accountId == Guid.Empty)
            {
                row.ErrorText = Resources.InvalidAccountError;

                return false;
            }

            if (!_dataGridView.TryGetBalance(row, out decimal balance))
            {
                return false;
            }

            trialBalance += balance;
            lines.Add(new Line(
                accountId,
                balance,
                (string?)row.Cells[descriptionColumn.Index].Value,
                reconciled: null));
        }

        if (trialBalance != 0)
        {
            _dataGridView.Rows[_dataGridView.NewRowIndex].ErrorText = Resources.ImbalanceError;

            return false;
        }

        if (addingNew)
        {
            _company.AddTransaction(transaction, name, lines);
        }

        _company.UpdateTransaction(transaction.Id, numberNumericUpDown.Value, name, postedDateTimePicker.Value, lines);
        InitializeTransaction(transaction);

        return true;
    }

    private void Clear()
    {
        DateTime lastPosted = Settings.Default.LastPosted;

        postedDateTimePicker.Value = lastPosted == default ? DateTime.Today : lastPosted;
        nameComboBox.Text = string.Empty;
        nameComboBox.SelectedItem = null;

        memoTextBox.Clear();
        _dataGridView.Rows.Clear();
    }

    private void CreateNew()
    {
        Clear();

        Value = null;
        numberNumericUpDown.Value = _company.NextTransactionNumber;
    }

    private void OnCloseToolStripButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private void OnNewToolStripButtonClick(object sender, EventArgs e)
    {
        CreateNew();
    }

    private void OnSaveToolStripButtonClick(object sender, EventArgs e)
    {
        Save();
        TransactionHelpers.Post(postedDateTimePicker.Value);
    }

    private void OnCopyToolStripButtonClick(object sender, EventArgs e)
    {
        if (!Save())
        {
            return;
        }

        Transaction? clone = Value.Clone();

        _company.AddTransaction(clone, clone.Name, clone.Lines);
        _company.UpdateTransaction(clone.Id, _company.NextTransactionNumber, clone.Name, clone.Posted, clone.Lines);
        InitializeTransaction(clone);
        TransactionHelpers.Post(postedDateTimePicker.Value);
    }

    private void OnRemoveToolStripButton(object sender, EventArgs e)
    {
        if (Value == null || Value.Id == Guid.Empty)
        {
            return;
        }

        if (FormattedStrings.ShowDeleteTransactionMessage() != DialogResult.OK)
        {
            return;
        }

        Transaction remove = Value;
        Transaction? next = _company.GetTransactionAfter(remove);

        if (next == null)
        {
            CreateNew();
        }
        else
        {
            InitializeTransaction(next);
        }

        _company.RemoveTransaction(remove.Id);
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        if (!Save())
        {
            return;
        }

        TransactionHelpers.Post(postedDateTimePicker.Value);
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnApplyButtonClick(object sender, EventArgs e)
    {
        if (Save())
        {
            TransactionHelpers.Post(postedDateTimePicker.Value);
            CreateNew();
        }
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void OnFirstButtonClick(object sender, EventArgs e)
    {
        Transaction? first = _company.FirstTransaction;

        if (first == null)
        {
            CreateNew();

            return;
        }

        InitializeTransaction(first);
    }

    private void OnLastButtonClick(object sender, EventArgs e)
    {
        Transaction? last = _company.LastTransaction;

        if (last == null)
        {
            CreateNew();

            return;
        }

        InitializeTransaction(last);
    }

    private void OnPreviousButtonClick(object sender, EventArgs e)
    {
        Transaction? previous;

        if (Value == null)
        {
            previous = _company.LastTransaction;
        }
        else
        {
            previous = _company.GetTransactionBefore(Value);
        }

        if (previous != null)
        {
            InitializeTransaction(previous);
        }
    }

    private void OnNextButtonClick(object sender, EventArgs e)
    {
        if (Value == null)
        {
            CreateNew();

            return;
        }

        Transaction? next = _company.GetTransactionAfter(Value);

        if (next == null)
        {
            CreateNew();

            return;
        }

        InitializeTransaction(next);
    }

    private void OnDataGridViewDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
    {
        _dataGridView.SetBalance(e.Row, GetRemainder());
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _company.TransactionUpdated -= OnCompanyTransactionUpdated;
            _company.TransactionRemoved -= OnCompanyTransactionRemoved;
            _company.TransactionReconciled -= OnCompanyTransactionReconciled;

            if (components != null)
            {
                components.Dispose();

                components = null;
            }
        }

        base.Dispose(disposing);
    }
}
