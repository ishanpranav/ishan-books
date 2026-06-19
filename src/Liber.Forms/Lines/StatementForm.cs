// StatementForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Liber.Forms.AccountViews;

namespace Liber.Forms.Transactions;

internal partial class StatementForm : Form
{
    private readonly Company _company;

    private DateTime _lastReconciled;

    public decimal ReconciledBalance { get; private set; }

    public decimal EndingBalance
    {
        get
        {
            return _company.GetAccount(AccountId).Type.Toggle(endingBalanceNumericUpDown.Value);
        }
    }

    public DateTime Reconciled
    {
        get
        {
            return reconciledDateTimePicker.Value.Date;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Guid AccountId
    {
        get
        {
            return (Guid)accountComboBox.SelectedValue!;
        }
        set
        {
            accountComboBox.SelectedValue = value;
        }
    }

    public StatementForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        reconciledBalanceNumericUpDown.BackColor = Colors.Light;
        reconciledBalanceNumericUpDown.Minimum = decimal.MinValue;
        reconciledBalanceNumericUpDown.Maximum = decimal.MaxValue;
        endingBalanceNumericUpDown.Minimum = decimal.MinValue;
        endingBalanceNumericUpDown.Maximum = decimal.MaxValue;
        accountComboBox.DataSource = new AccountViewBindingList(company, x => !x.ReadOnly && !x.Type.IsTemporary());
        accountComboBox.ValueMember = nameof(AccountView.Id);
        accountComboBox.DisplayMember = nameof(AccountView.DisplayName);
        _company = company;
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    private void OnReconciledDateTimePickerValueChanged(object sender, EventArgs e)
    {
        Account account = _company.GetAccount(AccountId);

        InitializeEndingBalance(account);
    }

    private void OnAccountComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (Reconciled == DateTime.Today || Reconciled == _lastReconciled)
        {
            DateTime? reconciled = _company.GetAccount(AccountId).Reconciled;

            _lastReconciled = reconciled == null ? DateTime.Today : reconciled.Value.AddMonths(1);

            if (_lastReconciled > DateTime.Today)
            {
                _lastReconciled = DateTime.Today;
            }

            reconciledDateTimePicker.Value = _lastReconciled;
        }

        Account account = _company.GetAccount(AccountId);

        InitializeReconciledBalance(account);
        InitializeEndingBalance(account);
    }

    private void InitializeReconciledBalance(Account account)
    {
        decimal reconciledBalance = account.GetReconciledBalance();

        ReconciledBalance = reconciledBalance;
        reconciledBalanceNumericUpDown.Value = account.Type.Toggle(reconciledBalance);
    }

    private void InitializeEndingBalance(Account account)
    {
        endingBalanceNumericUpDown.Value = account.Type.Toggle(account.GetBalance(Reconciled, Filters.Any()));
    }
}
