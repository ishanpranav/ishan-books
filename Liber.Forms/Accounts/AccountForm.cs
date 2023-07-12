using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal abstract partial class AccountForm : Form
{
    protected AccountForm(Company company)
    {
        InitializeComponent();

        new ComponentResourceManager(GetType()).ApplyResources(this, "$this");

        Company = company;
        Company.AccountAdded += OnCompanyAccountAdded;
        Company.AccountRemoved += OnCompanyAccountRemoved;
        DialogResult = DialogResult.Cancel;
        typeComboBox.DataSource = Enum.GetValues<AccountType>();

        foreach (KeyValuePair<Guid, Account> account in Company.Accounts)
        {
            InitializeParent(account.Key, account.Value);
        }
    }

    public Company Company { get; }

    public decimal Number
    {
        get
        {
            return numberNumericUpDown.Value;
        }
        set
        {
            numberNumericUpDown.Value = value;
        }
    }

    public string? AccountName
    {
        get
        {
            return nameTextBox.Text;
        }
        set
        {
            nameTextBox.Text = value;
        }
    }

    public AccountType Type
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

    public bool Placeholder
    {
        get
        {
            return lockedCheckBox.Checked;
        }
        set
        {
            lockedCheckBox.Checked = value;
        }
    }

    public Guid ParentKey
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

    protected virtual bool IsValid(Guid parentKey)
    {
        return true;
    }

    protected abstract void CommitChanges();

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

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        _errorProvider.SetError(numberNumericUpDown, null);
        _errorProvider.SetError(nameTextBox, null);

        try
        {
            CommitChanges();
        }
        catch (ArgumentException argumentException)
        {
            _errorProvider.SetError(nameTextBox, argumentException.Message);

            return;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            _errorProvider.SetError(numberNumericUpDown, invalidOperationException.Message);

            return;
        }

        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
