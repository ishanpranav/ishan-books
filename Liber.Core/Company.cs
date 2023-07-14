using MessagePack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Liber;

[MessagePackObject]
[XmlRoot("company")]
public sealed class Company : IXmlSerializable
{
    private readonly Dictionary<Guid, Account> _accounts;
    private readonly Dictionary<Guid, Transaction> _transactions;
    private readonly SortedSet<Transaction> _sortedTransactions;

    public Company()
    {
        _accounts = new Dictionary<Guid, Account>();
        _transactions = new Dictionary<Guid, Transaction>();
        _sortedTransactions = new SortedSet<Transaction>(TransactionComparer.Default);
    }

    [JsonConstructor]
    [SerializationConstructor]
    public Company(
        IReadOnlyDictionary<Guid, Account> accounts,
        IReadOnlyDictionary<Guid, Transaction> transactions,
        decimal nextAccountNumber,
        decimal nextTransactionNumber)
    {
        _accounts = new Dictionary<Guid, Account>(accounts);
        _transactions = new Dictionary<Guid, Transaction>(transactions);
        _sortedTransactions = new SortedSet<Transaction>(transactions.Values, TransactionComparer.Default);
        NextAccountNumber = nextAccountNumber;
        NextTransactionNumber = nextTransactionNumber;

        foreach (Account account in accounts.Values)
        {
            if (account.ParentKey != Guid.Empty)
            {
                accounts[account.ParentKey].children.Add(account);
            }
        }

        foreach (Transaction transaction in transactions.Values)
        {
            foreach (Line line in transaction.Lines)
            {
                accounts[line.AccountKey].lines.Add(line);
            }
        }
    }

    [Key(0)]
    public string? Name { get; set; }

    [Key(1)]
    public decimal NextAccountNumber { get; private set; } = 1;

    [Key(2)]
    public decimal NextTransactionNumber { get; private set; } = 1;

    [Key(3)]
    public IReadOnlyDictionary<Guid, Account> Accounts
    {
        get
        {
            return _accounts;
        }
    }

    [Key(4)]
    public IReadOnlyDictionary<Guid, Transaction> Transactions
    {
        get
        {
            return _transactions;
        }
    }

    [JsonIgnore]
    public Transaction? LastTransaction
    {
        get
        {
            return _sortedTransactions.Max;
        }
    }

    public event EventHandler<KeyEventArgs>? AccountAdded;
    public event EventHandler<KeyEventArgs>? AccountUpdated;
    public event EventHandler<KeyEventArgs>? AccountRemoved;

    private void AddChild(Account value, Guid parentKey)
    {
        value.ParentKey = parentKey;

        if (parentKey != Guid.Empty)
        {
            Account parent = _accounts[parentKey];

            parent.children.Add(value);
        }
    }

    private void RemoveChild(Account value)
    {
        if (value.ParentKey != Guid.Empty)
        {
            Account parent = _accounts[value.ParentKey];

            parent.children.Remove(value);

            value.ParentKey = Guid.Empty;
        }
    }

    public void AddAccount(Account value, Guid parentKey)
    {
        Guid key = Guid.NewGuid();

        AddChild(value, parentKey);
        NextAccountNumber = Math.Max(value.Number, NextAccountNumber) + 1;
        _accounts.Add(key, value);
        AccountAdded?.Invoke(sender: this, new KeyEventArgs(key));
    }

    public void UpdateAccount(Guid key, Guid parentKey)
    {
        Account value = _accounts[key];

        if (parentKey != value.ParentKey)
        {
            RemoveChild(value);
            AddChild(value, parentKey);
        }

        NextAccountNumber = Math.Max(value.Number, NextAccountNumber);

        AccountUpdated?.Invoke(sender: this, new KeyEventArgs(key));
    }

    public void RemoveAccount(Guid key)
    {
        RemoveChild(_accounts[key]);
        _accounts.Remove(key);

        AccountRemoved?.Invoke(sender: this, new KeyEventArgs(key));
    }

    public Transaction? GetTransactionBefore(Guid key)
    {
        Transaction value = _transactions[key];

        if (TransactionComparer.Default.Compare(_sortedTransactions.Max, value) >= 0)
        {
            return null;
        }

        return _sortedTransactions
            .GetViewBetween(_sortedTransactions.Min, value)
            .FirstOrDefault();
    }

    public Transaction? GetTransactionAfter(Guid key)
    {
        Transaction value = _transactions[key];

        if (TransactionComparer.Default.Compare(_sortedTransactions.Max, value) <= 0)
        {
            return null;
        }

        return _sortedTransactions
            .GetViewBetween(value, _sortedTransactions.Max)
            .Take(2)
            .LastOrDefault();
    }

    public Guid AddTransaction(Transaction value)
    {
        Guid key = Guid.NewGuid();

        foreach (Line line in value.Lines)
        {
            _accounts[line.AccountKey].lines.Add(line);
        }

        _transactions.Add(key, value);
        _sortedTransactions.Add(value);

        NextTransactionNumber = Math.Max(value.Number, NextTransactionNumber) + 1;

        return key;
    }

    public void RemoveTransaction(Guid key)
    {
        Transaction value = _transactions[key];

        foreach (Line line in value.Lines)
        {
            _accounts[line.AccountKey].lines.Remove(line);
        }

        _transactions.Remove(key);
        _sortedTransactions.Remove(value);
    }

    public void CopyTo(Company other)
    {
        other.Name = Name;
        other.NextAccountNumber = NextAccountNumber;
        other.NextTransactionNumber = NextTransactionNumber;

        other._accounts.Clear();
        other._transactions.Clear();
        other._sortedTransactions.Clear();

        foreach (KeyValuePair<Guid, Account> account in _accounts)
        {
            other._accounts[account.Key] = account.Value;
        }

        foreach (KeyValuePair<Guid, Transaction> transaction in _transactions)
        {
            other._transactions[transaction.Key] = transaction.Value;
            other._sortedTransactions.Add(transaction.Value);
        }
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteElementString("name", Name);

        foreach (Account account in Accounts.Values)
        {
            XmlSerializers.Account.Serialize(writer, account);
        }
    }
}
