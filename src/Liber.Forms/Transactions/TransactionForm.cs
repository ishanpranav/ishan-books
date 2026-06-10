// TransactionForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.AccountViews;
using Liber.Forms.Components;
using Liber.Forms.Properties;
using Liber.MathEngine.Expressions;
using MS.WindowsAPICodePack.Internal;
using SQLitePCL;

namespace Liber.Forms.Transactions;

internal sealed partial class TransactionForm : Form
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

        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
        _company = company;
        DialogResult = DialogResult.Cancel;
        accountColumn.ValueMember = nameof(IAccountView.Id);
        accountColumn.DisplayMember = nameof(IAccountView.DisplayName);
        numberNumericUpDown.Maximum = decimal.MaxValue;
        nameComboBox.DataSource = company.GetNames();
        debitColumn.ValueType = typeof(IExpression);
        debitColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        creditColumn.ValueType = typeof(IExpression);
        creditColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        _dataGridView.AlternatingRowsDefaultCellStyle.BackColor = company.Color;
        _dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = company.Color.GetForeColor();

        if (company.Color == _dataGridView.DefaultCellStyle.SelectionBackColor)
        {
            _dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = _dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor.Tint(0.15);
            _dataGridView.DefaultCellStyle.SelectionBackColor = _dataGridView.DefaultCellStyle.SelectionBackColor.Tint(0.15);
        }

        foreach (KeyValuePair<Guid, Account> account in company.OrderedAccounts)
        {
            if (!account.Value.ReadOnly)
            {
                InitializeAccount(account.Key, account.Value);
            }
        }

        accountColumn.Items.Add(NewAccountView.Value);
        _dataGridView.AutoResizeColumns();
        CreateNew();
    }

    private void InitializeAccount(Guid key, Account value)
    {
        accountColumn.Items.Add(new AccountView(key, value));
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

        foreach (Line line in transaction.OrderedLines)
        {
            _dataGridView.Rows.Add(
                line.AccountId,
                new DecimalExpression(line.Debit),
                new DecimalExpression(line.Credit),
                line.Description ?? string.Empty);
        }

        _dataGridView.AutoResizeColumns();
    }

    private static bool TryEvaluateExpression(DataGridViewRow row, int columnIndex, out decimal value)
    {
        object? cellValue = row.Cells[columnIndex].Value;

        if (cellValue == null || cellValue is DBNull)
        {
            value = 0;

            return true;
        }

        try
        {
            value = ((IExpression)cellValue).Evaluate();

            return true;
        }
        catch (DivideByZeroException divideByZeroException)
        {
            row.ErrorText = divideByZeroException.Message;
            value = 0;

            return false;
        }
    }

    private decimal GetRemainder()
    {
        decimal result = 0;

        foreach (DataGridViewRow row in _dataGridView.Rows)
        {
            if (row.IsNewRow || row.ErrorText != string.Empty || !TryGetBalance(row, out decimal balance))
            {
                continue;
            }

            result -= balance;
        }

        return result;
    }

    private bool TryGetBalance(DataGridViewRow row, out decimal result)
    {
        if (!TryEvaluateExpression(row, debitColumn.Index, out decimal debit) ||
            !TryEvaluateExpression(row, creditColumn.Index, out decimal credit))
        {
            result = 0;

            return false;
        }

        result = debit - credit;

        return true;
    }

    private void SetBalance(DataGridViewRow row, decimal value)
    {
        if (value >= 0)
        {
            row.Cells[debitColumn.Index].Value = new DecimalExpression(value);
            row.Cells[creditColumn.Index].Value = null;
        }
        else
        {
            row.Cells[debitColumn.Index].Value = null;
            row.Cells[creditColumn.Index].Value = new DecimalExpression(-value);
        }
    }

    [MemberNotNullWhen(true, nameof(Value))]
    private bool Save()
    {
        bool restoreNextTransactionNumber = false;
        decimal transactionNumber = _company.NextTransactionNumber;

        if (Value != null)
        {
            _company.RemoveTransaction(Value);

            restoreNextTransactionNumber = true;
        }

        Transaction transaction = new Transaction()
        {
            Id = Guid.NewGuid(),
            Number = numberNumericUpDown.Value,
            Posted = postedDateTimePicker.Value,
            Name = nameComboBox.Text,
            Memo = memoTextBox.Text
        };

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

            if (!TryGetBalance(row, out decimal balance))
            {
                return false;
            }

            transaction.Lines.Add(new Line()
            {
                AccountId = accountId,
                Balance = balance,
                Description = (string?)row.Cells[descriptionColumn.Index].Value
            });
        }

        if (transaction.Balance != 0)
        {
            _dataGridView.Rows[_dataGridView.NewRowIndex].ErrorText = Resources.ImbalanceError;

            return false;
        }

        _company.AddTransaction(transaction);

        if (restoreNextTransactionNumber)
        {
            _company.NextTransactionNumber = transactionNumber;
        }

        Settings.Default.LastPosted = postedDateTimePicker.Value;

        Settings.Default.Save();
        InitializeTransaction(transaction);
        SystemSounds.Asterisk.Play();

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

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        InitializeAccount(e.Id, _company.Accounts[e.Id]);
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        accountColumn.Items.Remove(e.Id);

        if (!_company!.Accounts[e.Id].Placeholder)
        {
            accountColumn.Items.Add(e.Id);
        }

        Refresh();
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        accountColumn.Items.Remove(e.Id);
    }

    private void OnNewToolStripButtonClick(object sender, EventArgs e)
    {
        CreateNew();
    }

    private void OnSaveToolStripButtonClick(object sender, EventArgs e)
    {
        Save();
    }

    private void OnCopyToolStripButtonClick(object sender, EventArgs e)
    {
        if (!Save())
        {
            return;
        }

        Transaction clone = new Transaction()
        {
            Id = Guid.NewGuid(),
            Posted = DateTime.Today,
            Number = _company.NextTransactionNumber,
            Name = Value.Name,
            Memo = Value.Memo
        };

        foreach (Line line in Value.Lines)
        {
            clone.Lines.Add(new Line()
            {
                AccountId = line.AccountId,
                Balance = line.Balance,
                Description = line.Description
            });
        }

        _company.AddTransaction(clone);
        InitializeTransaction(clone);
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        if (!Save())
        {
            return;
        }

        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnApplyButtonClick(object sender, EventArgs e)
    {
        if (Save())
        {
            CreateNew();
        }
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Clear();
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

    private void OnPrintToolStripButtonClick(object sender, EventArgs e)
    {
        // TODO: Print transaction
    }

    private void OnDataGridViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == -1 || e.RowIndex == -1)
        {
            return;
        }

        AccountCellEndEdit(e.ColumnIndex, e.RowIndex);
        ExpressionCellEndEdit(e.ColumnIndex, e.RowIndex);
    }

    private void OnDataGridViewDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
    {
        SetBalance(e.Row, GetRemainder());
    }

    private void AccountCellEndEdit(int columnIndex, int rowIndex)
    {
        if (_dataGridView[columnIndex, rowIndex].Value is not Guid accountId ||
            accountId != Guid.Empty)
        {
            return;
        }

        NewAccountForm form = new NewAccountForm(_company);

        if (form.ShowDialog() == DialogResult.OK)
        {
            _dataGridView[columnIndex, rowIndex].Value = form.Id;
        }
    }

    private void ExpressionCellEndEdit(int columnIndex, int rowIndex)
    {
        DataGridViewColumn column = _dataGridView.Columns[columnIndex];

        if (column.ValueType != typeof(IExpression) ||
            _dataGridView[columnIndex, rowIndex].Value is not IExpression expression)
        {
            return;
        }

        decimal value;

        try
        {
            value = expression.Evaluate();

            _dataGridView[columnIndex, rowIndex].Value = new DecimalExpression(value);
        }
        catch (DivideByZeroException)
        {
            return;
        }

        if (column != debitColumn && column != creditColumn)
        {
            return;
        }

        DataGridViewRow row = _dataGridView.Rows[rowIndex];

        if (!TryEvaluateExpression(row, debitColumn.Index, out decimal debit) ||
            !TryEvaluateExpression(row, creditColumn.Index, out decimal credit))
        {
            return;
        }

        decimal remainder = GetRemainder() + debit - credit;

        if (debit == remainder && credit != 0)
        {
            SetBalance(row, -credit);

            return;
        }

        if (-credit == remainder && debit != 0)
        {
            SetBalance(row, debit);

            return;
        }

        SetBalance(_dataGridView.Rows[rowIndex], debit - credit);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _company.AccountAdded -= OnCompanyAccountAdded;
            _company.AccountUpdated -= OnCompanyAccountUpdated;
            _company.AccountRemoved -= OnCompanyAccountRemoved;

            if (components != null)
            {
                components.Dispose();
                components = null;
            }
        }

        base.Dispose(disposing);
    }
}
