using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal abstract partial class AccountForm : Form
{
    protected AccountForm(Company company, Guid id)
    {
        InitializeComponent();

        ComponentResourceManager resourceManager = new ComponentResourceManager(GetType());

        resourceManager.ApplyResources(this, "$this");

        Company = company;
        Company.AccountAdded += OnCompanyAccountAdded;
        Company.AccountRemoved += OnCompanyAccountRemoved;
        Id = id;
        DialogResult = DialogResult.Cancel;
        typeComboBox.DataSource = Enum.GetValues<AccountType>();
        companionComboBox.ValueMember = nameof(AccountView.Id);
        companionComboBox.DisplayMember = nameof(AccountView.DisplayName);

        foreach (KeyValuePair<Guid, Account> account in Company.Accounts)
        {
            InitializeCompanion(account.Key, account.Value);
        }
    }

    public Company Company { get; }
    public Guid Id { get; }

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

    public string AccountName
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

    public bool Locked
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

    public Guid CompanionId
    {
        get
        {
            if (companionComboBox.SelectedValue == null)
            {
                return Guid.Empty;
            }

            return (Guid)companionComboBox.SelectedValue;
        }
        set
        {
            companionComboBox.SelectedValue = value;
        }
    }
    private void InitializeCompanion(Guid id, Account value)
    {
        if (value.Locked)
        {
            _ = companionComboBox.Items.Add(new AccountView(id, value));
        }
    }

    private void OnCompanyAccountAdded(object? sender, AccountEventArgs e)
    {
        InitializeCompanion(e.Id, e.Account);
    }

    private void OnCompanyAccountRemoved(object? sender, AccountEventArgs e)
    {
        if (e.Account.Locked)
        {
            companionComboBox.Items.Remove(e.Id);
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
            Company.AddOrUpdateAccount(Id, Number, AccountName, Type, Locked, CompanionId);
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
