using Liber.Forms.Accounts;
using Liber.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Liber.Forms;

internal sealed partial class JournalForm : Form
{
    private readonly MainContext _context;

    public JournalForm(MainContext context, Guid _)
    {
        InitializeComponent();

        _context = context;
        _context.Company.AccountAdded += OnCompanyAccountAdded;
        _context.Company.AccountRemoved += OnCompanyAccountRemoved;
        DialogResult = DialogResult.Cancel;
        accountColumn.ValueMember = nameof(AccountView.Id);
        accountColumn.DisplayMember = nameof(AccountView.DisplayName);

        foreach (KeyValuePair<Guid, Account> account in _context.Company.Accounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        debitColumn.ValueType = typeof(decimal);
        creditColumn.ValueType = typeof(decimal);

        _dataGridView.AutoResizeColumns();
        CreateNew();
    }

    private int Number
    {
        get
        {
            return (int)numberNumericUpDown.Value;
        }
        set
        {
            numberNumericUpDown.Value = value;
        }
    }

    private DateOnly Posted
    {
        get
        {
            return DateOnly.FromDateTime(postedDateTimePicker.Value);
        }
        set
        {
            postedDateTimePicker.Value = value.ToDateTime(TimeOnly.MinValue);
        }
    }

    private void InitializeAccount(Guid id, Account value)
    {
        _ = accountColumn.Items.Add(new AccountView(id, value));
    }

    private void InitializeJournal(IEnumerable<Transaction> value)
    {
        Posted = value.FirstOrDefault()?.Posted ?? DateOnly.FromDateTime(DateTime.Today);

        _dataGridView.Rows.Clear();

        foreach (Transaction transaction in value)
        {
            object? debit;
            object? credit;

            if (transaction.Debit > 0)
            {
                debit = transaction.Debit;
                credit = null;
            }
            else
            {
                debit = null;
                credit = -transaction.Debit;
            }

            _dataGridView.Rows.Add(transaction.AccountId, debit, credit, transaction.Description);
        }

        _dataGridView.AutoResizeColumns();
    }

    private static decimal ToDecimal(object cellValue)
    {
        if (cellValue == null || cellValue is DBNull)
        {
            return 0m;
        }

        return (decimal)cellValue;
    }

    private bool Save()
    {
        _context.Company.RemoveJournal(Number);

        List<Transaction> journal = new List<Transaction>();

        foreach (DataGridViewRow row in _dataGridView.Rows)
        {
            row.ErrorText = null;

            if (row.IsNewRow)
            {
                continue;
            }

            if (row.Cells[accountColumn.Index].Value == null)
            {
                row.ErrorText = Resources.InvalidAccountException;

                return false;
            }

            Guid accountId = (Guid)row.Cells[accountColumn.Index].Value;
            decimal debit = ToDecimal(row.Cells[debitColumn.Index].Value);
            decimal credit = ToDecimal(row.Cells[creditColumn.Index].Value);

            journal.Add(new Transaction()
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                Number = Number,
                Posted = Posted,
                Description = (string?)row.Cells[descriptionColumn.Index].Value,
                Debit = debit - credit
            });
        }

        try
        {
            _context.Company.AddTransactions(journal);
            _context.Company.UpdateJournal(Number);
        }
        catch (ArgumentException exception)
        {
            _dataGridView.Rows[_dataGridView.NewRowIndex].ErrorText = exception.Message;

            return false;
        }

        InitializeJournal(journal);

        return true;
    }

    private void Clear()
    {
        Posted = DateOnly.FromDateTime(DateTime.Today);

        _dataGridView.Rows.Clear();
    }

    private void CreateNew()
    {
        Clear();

        Number = _context.Company.NextJournalNumber;
    }

    private void OnCompanyAccountAdded(object? sender, AccountEventArgs e)
    {
        InitializeAccount(e.Id, e.Account);
    }

    private void OnCompanyAccountRemoved(object? sender, AccountEventArgs e)
    {
        accountColumn.Items.Remove(e.Id);
    }

    private void OnNumberNumericUpDownValueChanged(object sender, EventArgs e)
    {
        InitializeJournal(_context.Company.Journal(Number));
    }

    private void OnNumberNumericUpDownValidating(object sender, CancelEventArgs e)
    {
        _ = Save();
    }

    private void OnNewToolStripButtonClick(object sender, EventArgs e)
    {
        CreateNew();
    }

    private void OnSaveToolStripButtonClick(object sender, EventArgs e)
    {
        _ = Save();
    }

    private void OnCopyToolStripButtonClick(object sender, EventArgs e)
    {
        if (!Save())
        {
            return;
        }

        List<Transaction> clones = new List<Transaction>();
        _context.Company.UpdateJournal(Number);

        int number = _context.Company.NextJournalNumber;

        foreach (Transaction transaction in _context.Company.Journal(Number))
        {
            Transaction clone = transaction.Clone();

            clone.Id = Guid.NewGuid();
            clone.Number = number;

            clones.Add(clone);
        }

        _context.Company.AddTransactions(clones);
        _context.Company.UpdateJournal(number);

        Number = number;
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

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();

            _context.Company.AccountAdded -= OnCompanyAccountAdded;
            _context.Company.AccountRemoved -= OnCompanyAccountRemoved;
        }

        base.Dispose(disposing);
    }
}
