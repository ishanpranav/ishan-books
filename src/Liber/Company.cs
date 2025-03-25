// Company.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Liber.Properties;
using MessagePack;
using MessagePack.Formatters;

namespace Liber;

/// <summary>
/// Represents an individual sole proprietorship, partnership, or stock corporation, or other entity.
/// </summary>
[MessagePackObject]
public sealed class Company
{
    private readonly Dictionary<Guid, Account> _accounts;
    private readonly SortedSet<Transaction> _transactions;
    private readonly SortedSet<string> _names = new SortedSet<string>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Company"/> class.
    /// </summary>
    public Company()
    {
        _accounts = new Dictionary<Guid, Account>();
        _transactions = new SortedSet<Transaction>();
        EquityAccountId = AddAccount(new Account()
        {
            Name = Resources.DefaultEquityAccountName,
            Type = AccountType.Equity
        }, Guid.Empty);
        OtherEquityAccountId = AddAccount(new Account()
        {
            Name = Resources.DefaultOtherEquityAccountName,
            Type = AccountType.Equity
        }, Guid.Empty);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Company"/> class with the specified accounts, journal entries, account number sequence, and journal entry number sequence.
    /// </summary>
    /// <param name="accounts">The account dictionary.</param>
    /// <param name="transactions">The transaction collection.</param>
    /// <param name="nextAccountNumber">The next account number to be assigned.</param>
    /// <param name="nextTransactionNumber">The next journal entry number to be assigned.</param>
    [JsonConstructor]
    [SerializationConstructor]
    public Company(
        IReadOnlyDictionary<Guid, Account> accounts,
        IReadOnlyCollection<Transaction> transactions,
        decimal nextAccountNumber,
        decimal nextTransactionNumber)
    {
        _accounts = new Dictionary<Guid, Account>(accounts);
        _transactions = new SortedSet<Transaction>(transactions);
        NextAccountNumber = nextAccountNumber;
        NextTransactionNumber = nextTransactionNumber;

        foreach (KeyValuePair<Guid, Account> account in accounts)
        {
            if (account.Value.ParentId != Guid.Empty)
            {
                accounts[account.Value.ParentId].children.Add(account.Value);
            }
        }

        foreach (Transaction transaction in transactions)
        {
            AddName(transaction);

            foreach (Line line in transaction.Lines)
            {
                accounts[line.AccountId].lines.Add(line);
                line.Transaction = transaction;
            }
        }
    }

    [Key(0)]
    public IReadOnlyDictionary<Guid, Account> Accounts
    {
        get
        {
            return _accounts;
        }
    }

    [IgnoreMember]
    [JsonIgnore]
    public IEnumerable<KeyValuePair<Guid, Account>> OrderedAccounts
    {
        get
        {
            return _accounts
                .OrderBy(x => x.Value.Number)
                .ThenBy(x => x.Value.Name)
                .ThenBy(x => x.Value.Type)
                .ThenByDescending(x => x.Value.Balance);
        }
    }

    [Key(1)]
    public IReadOnlyCollection<Transaction> Transactions
    {
        get
        {
            return _transactions;
        }
    }

    [Key(2)]
    public decimal NextAccountNumber { get; private set; } = 1;

    [Key(3)]
    public decimal NextTransactionNumber { get; private set; } = 1;

    [Key(4)]
    public string? Name { get; set; }

    [IgnoreMember]
    [JsonIgnore]
    public string DisplayName
    {
        get
        {
            return Name ?? Resources.DefaultCompanyName;
        }
    }

    [Key(5)]
    public CompanyType Type { get; set; }

    [Key(6)]
    [MessagePackFormatter(typeof(MessagePackColorFormatter))]
    public Color Color { get; set; } = Color.FromArgb(221, 237, 224);

    [Key(7)]
    public Guid EquityAccountId { get; set; }

    [Key(8)]
    public Guid OtherEquityAccountId { get; set; }

    [IgnoreMember]
    [JsonIgnore]
    public string? Password { get; set; }

    [IgnoreMember]
    [JsonIgnore]
    public Transaction? LastTransaction
    {
        get
        {
            return _transactions.Max;
        }
    }

    public event EventHandler<GuidEventArgs>? AccountAdded;
    public event EventHandler<GuidEventArgs>? AccountUpdated;
    public event EventHandler<GuidEventArgs>? AccountRemoved;

    public string[] GetNames()
    {
        string[] result = new string[_names.Count];

        _names.CopyTo(result);

        return result;
    }

    private void AddChild(Account value, Guid parentId)
    {
        value.ParentId = parentId;

        if (parentId != Guid.Empty)
        {
            Account parent = _accounts[parentId];

            parent.children.Add(value);
        }
    }

    private void RemoveChild(Account value)
    {
        if (value.ParentId != Guid.Empty)
        {
            Account parent = _accounts[value.ParentId];

            parent.children.Remove(value);

            value.ParentId = Guid.Empty;
        }
    }

    public Guid AddAccount(Account value, Guid parentId)
    {
        Guid result = Guid.NewGuid();

        AddChild(value, parentId);
        NextAccountNumber = Math.Max(value.Number, NextAccountNumber) + 1;
        _accounts.Add(result, value);
        AccountAdded?.Invoke(sender: this, new GuidEventArgs(result));

        return result;
    }

    public void UpdateAccount(Guid id, Guid parentId)
    {
        Account value = _accounts[id];

        if (parentId != value.ParentId)
        {
            RemoveChild(value);
            AddChild(value, parentId);
        }

        NextAccountNumber = Math.Max(value.Number, NextAccountNumber);

        AccountUpdated?.Invoke(sender: this, new GuidEventArgs(id));
    }

    public void RemoveAccount(Guid id)
    {
        if (EquityAccountId == id || OtherEquityAccountId == id)
        {
            return;
        }

        Account value = _accounts[id];

        if (value.Children.Count > 0 || value.Lines.Count > 0)
        {
            return;
        }

        RemoveChild(value);
        _accounts.Remove(id);

        AccountRemoved?.Invoke(sender: this, new GuidEventArgs(id));
    }

    public Transaction? GetTransactionBefore(Transaction value)
    {
        if (_transactions.Min == null || _transactions.Min >= value)
        {
            return null;
        }

        return _transactions
            .GetViewBetween(_transactions.Min, value)
            .Reverse()
            .Take(2)
            .Last();
    }

    public Transaction? GetTransactionAfter(Transaction value)
    {
        if (LastTransaction == null || LastTransaction <= value)
        {
            return null;
        }

        return _transactions
            .GetViewBetween(value, LastTransaction)
            .Take(2)
            .Last();
    }

    /// <summary>
    /// Retrieves a collection of transactions that fall within a specified date range.
    /// </summary>
    /// <param name="started">The start date of the date range.</param>
    /// <param name="posted">The end date of the date range.</param>
    /// <returns>A collection of transactions within the specified date range.</returns>
    public IReadOnlySet<Transaction> GetTransactionsBetween(DateTime started, DateTime posted)
    {
        return _transactions.GetViewBetween(
            new Transaction() { Posted = started },
            new Transaction() { Posted = posted.AddDays(1) });
    }

    public void AddTransaction(Transaction value)
    {
        foreach (Line line in value.Lines)
        {
            Account account = _accounts[line.AccountId];

            line.Transaction = value;
            account.lines.Add(line);
        }

        AddName(value);
        _transactions.Add(value);

        NextTransactionNumber = Math.Max(value.Number, NextTransactionNumber) + 1;

        if (string.IsNullOrWhiteSpace(value.Memo))
        {
            value.Memo = GetSuggestedMemo(value);
        }
    }

    private string? GetSuggestedMemo(Transaction value)
    {
        if (value.Lines.All(x => _accounts[x.AccountId].Type.IsAsset()))
        {
            return Resources.TransferMemo;
        }

        List<Line> bankLines = value.Lines
            .Where(x => _accounts[x.AccountId].Type == AccountType.Bank)
            .ToList();

        if (bankLines.TrueForAll(x => x.Balance > 0))
        {
            return Resources.DepositMemo;
        }

        return null;
    }

    public void RemoveTransaction(Transaction value)
    {
        foreach (Line line in value.Lines)
        {
            line.Transaction = null;
            _accounts[line.AccountId].lines.Remove(line);
        }

        _transactions.Remove(value);
    }

    private void AddName(Transaction value)
    {
        if (string.IsNullOrWhiteSpace(value.Name))
        {
            value.Name = null;
        }
        else
        {
            value.Name = value.Name.Trim();
            _names.Add(value.Name);
        }
    }

    public IEnumerable<Transaction> GetTransfers()
    {
        foreach (Transaction transaction in _transactions)
        {
            if (transaction.Lines.All(x => _accounts[x.AccountId].Type.IsAsset()))
            {
                yield return transaction;
            }
        }
    }

    public IEnumerable<Line> GetChecks()
    {
        foreach (Account account in _accounts.Values)
        {
            if (account.Type != AccountType.Bank)
            {
                continue;
            }

            foreach (Line line in account.lines)
            {
                if (line.Transaction?.Name != null && line.Balance < 0)
                {
                    yield return line;
                }
            }
        }
    }

    public IEnumerable<Line> GetDeposits()
    {
        foreach (Account account in _accounts.Values)
        {
            if (account.Type != AccountType.Bank)
            {
                continue;
            }

            foreach (Line line in account.lines)
            {
                if (line.Transaction?.Name != null && line.Balance > 0)
                {
                    yield return line;
                }
            }
        }
    }

    public IEnumerable<Line> GetPayments()
    {
        foreach (Account account in _accounts.Values)
        {
            if (account.Type != AccountType.CreditCard)
            {
                continue;
            }

            foreach (Line line in account.lines)
            {
                if (line.Balance > 0)
                {
                    yield return line;
                }
            }
        }
    }

    public IEnumerable<Line> GetCharges()
    {
        foreach (Account account in _accounts.Values)
        {
            if (account.Type != AccountType.CreditCard)
            {
                continue;
            }

            foreach (Line line in account.lines)
            {
                if (line.Balance < 0)
                {
                    yield return line;
                }
            }
        }
    }

    public decimal GetEquity(DateTime posted, Regex filter)
    {
        decimal result = 0;

        foreach (Account account in _accounts.Values)
        {
            if (account.Type.IsTemporary())
            {
                result += account.GetBalance(posted, filter);
            }
        }

        return result;
    }

    public decimal GetEquity(DateTime started, DateTime posted, Regex filter)
    {
        decimal result = 0;

        foreach (Account account in _accounts.Values)
        {
            if (account.Type.IsTemporary())
            {
                result += account.GetBalance(started, posted, filter);
            }
        }

        return result;
    }

    /// <summary>
    /// Copies the data from the current <see cref="Company"/> instance to another <see cref="Company"/> instance.
    /// </summary>
    /// <param name="other">The target <see cref="Company"/> instance to which the data will be copied.</param>
    public void CopyTo(Company other)
    {
        other.Name = Name;
        other.NextAccountNumber = NextAccountNumber;
        other.NextTransactionNumber = NextTransactionNumber;
        other.Color = Color;
        other.EquityAccountId = EquityAccountId;
        other.OtherEquityAccountId = OtherEquityAccountId;
        other.Password = Password;
        other.Type = Type;

        other._accounts.Clear();
        other._transactions.Clear();
        other._names.Clear();

        foreach (KeyValuePair<Guid, Account> account in _accounts)
        {
            other._accounts[account.Key] = account.Value;
        }

        foreach (Transaction transaction in _transactions)
        {
            other._transactions.Add(transaction);
        }

        foreach (string name in _names)
        {
            other._names.Add(name);
        }
    }
}
