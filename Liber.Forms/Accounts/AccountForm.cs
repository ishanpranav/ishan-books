using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal abstract partial class AccountForm : Form
{
    private Color _color;

    protected AccountForm(Company company)
    {
        InitializeComponent();

        new ComponentResourceManager(GetType()).ApplyResources(this, "$this");

        Company = company;
        Company.AccountAdded += OnCompanyAccountAdded;
        Company.AccountUpdated += OnCompanyAccountUpdated;
        Company.AccountRemoved += OnCompanyAccountRemoved;
        DialogResult = DialogResult.Cancel;
        typeComboBox.DataSource = Enum.GetValues<AccountType>();
        taxTypeComboBox.DataSource = Enum.GetValues<TaxType>();

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

    protected Guid ParentKey
    {
        get
        {
            if (parentComboBox.SelectedItem == null)
            {
                return Guid.Empty;
            }

            return ((AccountView)parentComboBox.SelectedItem).Key;
        }
        set
        {
            if (value == Guid.Empty)
            {
                parentComboBox.SelectedItem = null;

                return;
            }

            parentComboBox.SelectedItem = new AccountView(value, Company.Accounts[value]);
        }
    }

    protected Color Color
    {
        get
        {
            return _color;
        }
        set
        {
            _color = value;
            colorButton.BackColor = value;
        }
    }

    protected virtual bool IsValid(Guid parentKey)
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
        account.Notes = notesTextBox.Text;
        account.Color = Color;
        account.TaxType = TaxType;
    }

    private void InitializeParent(Guid key, Account value)
    {
        AccountView accountView = new AccountView(key, value);

        parentComboBox.Items.Add(accountView);
    }

    private void OnCompanyAccountAdded(object? sender, KeyEventArgs e)
    {
        if (IsValid(e.Key))
        {
            InitializeParent(e.Key, Company.Accounts[e.Key]);
        }
    }

    private void OnCompanyAccountUpdated(object? sender, KeyEventArgs e)
    {
        if (IsValid(e.Key))
        {
            parentComboBox.Refresh();
        }
    }

    private void OnCompanyAccountRemoved(object? sender, KeyEventArgs e)
    {
        if (IsValid(e.Key))
        {
            parentComboBox.Items.Remove(e.Key);
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

    private void OnColorButtonClick(object sender, EventArgs e)
    {
        if (_colorDialog.ShowDialog() == DialogResult.OK)
        {
            Color = _colorDialog.Color;
        }
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
