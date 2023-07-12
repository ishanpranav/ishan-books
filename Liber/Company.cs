using Liber.Properties;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Liber;

/// <summary>
/// 
/// </summary>
public class Company : INotifyPropertyChanged
{
    private readonly Dictionary<Guid, Account> _accounts;
    private readonly HashSet<decimal> _accountNumbers = new HashSet<decimal>();
    private readonly HashSet<Transaction> _transactions;
    private readonly Dictionary<int, List<Transaction>> _journals = new Dictionary<int, List<Transaction>>();

    private string? _name;

    public Company()
    {
        _accounts = new Dictionary<Guid, Account>();
        _transactions = new HashSet<Transaction>();
    }

    [JsonConstructor]
    public Company(
        IReadOnlyDictionary<Guid, Account> accounts,
        IReadOnlyCollection<Transaction> transactions)
    {
        foreach (KeyValuePair<Guid, Account> account in accounts)
        {
            _ = _accountNumbers.Add(account.Value.Number);
        }

        foreach (Transaction transaction in transactions)
        {
            transaction.Id = Guid.NewGuid();
        }

        _accounts = new Dictionary<Guid, Account>(accounts);
        _transactions = new HashSet<Transaction>(transactions);

        AddTransactions(transactions);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public string? Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (_name == value)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                _name = null;
            }
            else
            {
                _name = value;
            }

            OnNameChanged();
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public string DisplayName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return FormattedStrings.GetString("CompanyName");
            }

            return Name;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public IReadOnlyDictionary<Guid, Account> Accounts
    {
        get
        {
            return _accounts;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public IReadOnlyCollection<Transaction> Transactions
    {
        get
        {
            return _transactions;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public decimal NextAccountNumber { get; set; } = 1;

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public int NextJournalNumber { get; set; } = 1;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// 
    /// </summary>
    public event EventHandler? NameChanged;

    /// <summary>
    /// 
    /// </summary>
    public event EventHandler<AccountEventArgs>? AccountAdded;

    /// <summary>
    /// 
    /// </summary>
    public event EventHandler<AccountEventArgs>? AccountRemoved;

    /// <summary>
    /// 
    /// </summary>
    protected virtual void OnNameChanged()
    {
        NameChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnAccountAdded(AccountEventArgs e)
    {
        AccountAdded?.Invoke(this, e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnAccountRemoved(AccountEventArgs e)
    {
        AccountRemoved?.Invoke(this, e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="UniqueNumberException"></exception>
    /// <exception cref="UniqueNameException"></exception>
    public void AddOrUpdateAccount(Guid id, decimal number, string name, AccountType type, bool locked, Guid companionId)
    {
        Account? previousCompanion = null;
        decimal previousNumber = 0;

        if (_accounts.TryGetValue(id, out Account? value))
        {
            previousNumber = value.Number;

            if (value.CompanionId != Guid.Empty)
            {
                previousCompanion = _accounts[value.CompanionId];
            }
        }

        if (number != previousNumber && _accountNumbers.Contains(number))
        {
            throw new InvalidOperationException(Resources.AccountNumberException);
        }

        if (id == Guid.Empty)
        {
            id = Guid.NewGuid();
        }

        if (value == null)
        {
            value = new Account();
            _accounts[id] = value;
        }

        if (previousCompanion != null)
        {
            foreach (Transaction previousTransaction in value.Transactions)
            {
                _ = previousCompanion.Transactions.Remove(previousTransaction);
            }
        }

        if (companionId != Guid.Empty)
        {
            Account companion = _accounts[companionId];

            foreach (Transaction transaction in value.Transactions)
            {
               companion.Transactions.Add(transaction);
            }
        }

        value.Name = name;
        value.Number = number;
        value.Type = type;
        value.Locked = locked;
        value.CompanionId = companionId;

        NextAccountNumber = Math.Max(NextAccountNumber, number + 1);

        _ = _accountNumbers.Add(number);

        if (previousNumber != 0)
        {
            OnAccountRemoved(new AccountEventArgs(id, value));
        }

        OnAccountAdded(new AccountEventArgs(id, value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Account RemoveAccount(Guid id)
    {
        Account value = _accounts[id];

        _ = _accounts.Remove(id);
        _ = _accountNumbers.Remove(value.Number);

        OnAccountRemoved(new AccountEventArgs(id, value));

        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void AddTransactions(IEnumerable<Transaction> values)
    {
        decimal debit = 0;

        foreach (Transaction value in values)
        {
            debit += value.Debit;
        }

        if (debit != 0)
        {
            throw new ArgumentException(Resources.JournalLinesArgumentException, nameof(values));
        }

        foreach (Transaction value in values)
        {
            if (!_journals.TryGetValue(value.Number, out List<Transaction>? journal))
            {
                journal = new List<Transaction>();
                _journals[value.Number] = journal;
            }

            journal.Add(value);

            Account account = _accounts[value.AccountId];

            account.Transactions.Add(value);

            if (_accounts.TryGetValue(account.CompanionId, out Account? companion))
            {
                companion?.Transactions.Add(value);
            }

            _ = _transactions.Add(value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public IEnumerable<Transaction> Journal(int number)
    {
        if (!_journals.TryGetValue(number, out List<Transaction>? journal))
        {
            return Enumerable.Empty<Transaction>();
        }

        return journal;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    public void RemoveJournal(int number)
    {
        if (!_journals.TryGetValue(number, out List<Transaction>? journal))
        {
            return;
        }

        foreach (Transaction transaction in journal)
        {
            Account account = _accounts[transaction.AccountId];

            if (account.Locked)
            {
                throw new InvalidOperationException();
            }

            if (_accounts.TryGetValue(account.CompanionId, out Account? companion))
            {
                if (!companion.Locked)
                {
                    throw new InvalidOperationException();
                }

                _ = companion.Transactions.Remove(transaction);
            }

            _ = account.Transactions.Remove(transaction);
            _ = _transactions.Remove(transaction);
        }

        journal.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    public void UpdateJournal(int number)
    {
        NextJournalNumber = Math.Max(number + 1, NextJournalNumber);
    }
}
