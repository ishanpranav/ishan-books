// AccountForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal abstract partial class AccountForm : Form
{
    protected AccountForm(Company company)
    {
        InitializeComponent();
        ClickOnce.Initialize(this);

        new ComponentResourceManager(GetType()).ApplyResources(this, "$this");

        Company = company;
        Company.AccountAdded += OnCompanyAccountAdded;
        Company.AccountUpdated += OnCompanyAccountUpdated;
        Company.AccountRemoved += OnCompanyAccountRemoved;
        DialogResult = DialogResult.Cancel;
        typeComboBox.DataSource = Enum
            .GetValues<AccountType>()
            .OrderBy(x => Math.Abs((short)x))
            .ToList();
        taxTypeComboBox.DataSource = Enum.GetValues<TaxType>();
        numberNumericUpDown.Maximum = decimal.MaxValue;

        foreach (KeyValuePair<Guid, Account> account in Company.Accounts)
        {
            InitializeParent(account.Key, account.Value);
        }
    }

    public Company Company { get; }

    protected AccountType Type
    {
        get
        {
            return (AccountType)typeComboBox.SelectedItem;
        }
        set
        {
            typeComboBox.SelectedItem = value;
        }
    }

    protected TaxType TaxType
    {
        get
        {
            return (TaxType)taxTypeComboBox.SelectedItem;
        }
        set
        {
            taxTypeComboBox.SelectedItem = value;
        }
    }

    protected Guid ParentId
    {
        get
        {
            if (parentComboBox.SelectedItem == null)
            {
                return Guid.Empty;
            }

            return ((AccountView)parentComboBox.SelectedItem).Id;
        }
        set
        {
            if (value == Guid.Empty)
            {
                parentComboBox.SelectedItem = null;

                return;
            }

            parentComboBox.SelectedItem = value;
        }
    }

    protected virtual bool IsValid(Guid parentId)
    {
        return true;
    }

    protected abstract void CommitChanges();

    protected void ApplyChanges(Account account)
    {
        account.Number = numberNumericUpDown.Value;
        account.Name = nameTextBox.Text;
        account.Type = Type;
        account.Placeholder = placeholderCheckBox.Checked;
        account.Description = descriptionTextBox.Text;
        account.Memo = memoTextBox.Text;
        account.Color = _colorButton.BackColor;
        account.TaxType = TaxType;
    }

    private void InitializeParent(Guid id, Account value)
    {
        AccountView accountView = new AccountView(id, value);

        parentComboBox.Items.Add(accountView);
    }

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        if (IsValid(e.Id))
        {
            InitializeParent(e.Id, Company.Accounts[e.Id]);
        }
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        if (IsValid(e.Id))
        {
            parentComboBox.Refresh();
        }
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        if (IsValid(e.Id))
        {
            parentComboBox.Items.Remove(e.Id);
        }
    }

    private void OnTypeComboBoxFormat(object sender, ListControlConvertEventArgs e)
    {
        e.Value = ((AccountType)e.ListItem!).ToLocalizedString();
    }

    private void OnTaxTypeComboBoxFormat(object sender, ListControlConvertEventArgs e)
    {
        e.Value = ((TaxType)e.ListItem!).ToLocalizedString();
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;

        CommitChanges();
        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Company.AccountAdded -= OnCompanyAccountAdded;
            Company.AccountUpdated -= OnCompanyAccountUpdated;
            Company.AccountRemoved -= OnCompanyAccountRemoved;

            components?.Dispose();
        }

        base.Dispose(disposing);
    }
}
