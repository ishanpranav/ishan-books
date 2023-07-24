// Company.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using MessagePack;
using MessagePack.Formatters;

namespace Liber;

[MessagePackObject]
[XmlRoot("company")]
public sealed class Company : IXmlSerializable
{
    private readonly Dictionary<Guid, Account> _accounts;
    private readonly SortedSet<Transaction> _transactions;

    public Company()
    {
        _accounts = new Dictionary<Guid, Account>();
        _transactions = new SortedSet<Transaction>();
    }

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

        foreach (Account account in accounts.Values)
        {
            if (account.ParentKey != Guid.Empty)
            {
                accounts[account.ParentKey].children.Add(account);
            }
        }

        foreach (Transaction transaction in transactions)
        {
            foreach (Line line in transaction.Lines)
            {
                accounts[line.AccountKey].lines.Add(line);
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

    [Key(5)]
    public CompanyType Type { get; set; }

    [Key(6)]
    [MessagePackFormatter(typeof(MessagePackColorFormatter))]
    public Color Color { get; set; }

    [IgnoreMember]
    [JsonIgnore]
    public Transaction? LastTransaction
    {
        get
        {
            return _transactions.Max;
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

    public Guid AddAccount(Account value, Guid parentKey)
    {
        Guid result = Guid.NewGuid();

        AddChild(value, parentKey);
        NextAccountNumber = Math.Max(value.Number, NextAccountNumber) + 1;
        _accounts.Add(result, value);
        AccountAdded?.Invoke(sender: this, new KeyEventArgs(result));

        return result;
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

    public void AddTransaction(Transaction value)
    {
        foreach (Line line in value.Lines)
        {
            Account account = _accounts[line.AccountKey];

            line.Transaction = value;
            account.lines.Add(line);
        }

        _transactions.Add(value);

        NextTransactionNumber = Math.Max(value.Number, NextTransactionNumber) + 1;
    }

    public void RemoveTransaction(Transaction value)
    {
        foreach (Line line in value.Lines)
        {
            line.Transaction = null;
            _accounts[line.AccountKey].lines.Remove(line);
        }

        _transactions.Remove(value);
    }

    public void CopyTo(Company other)
    {
        other.Name = Name;
        other.NextAccountNumber = NextAccountNumber;
        other.NextTransactionNumber = NextTransactionNumber;
        other.Color = Color;

        other._accounts.Clear();
        other._transactions.Clear();

        foreach (KeyValuePair<Guid, Account> account in _accounts)
        {
            other._accounts[account.Key] = account.Value;
        }

        foreach (Transaction transaction in _transactions)
        {
            other._transactions.Add(transaction);
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
        writer.WriteElementString("type", Type.ToString());

        foreach (Account account in _accounts.Values)
        {
            XmlSerializers.Account.Serialize(writer, account);
        }

        IEnumerable<Transaction> transactions;

        if (writer is XmlReportWriter reportWriter)
        {
            transactions = _transactions.GetViewBetween(reportWriter.Report.MinTransaction, reportWriter.Report.MaxTransaction);
        }
        else
        {
            transactions = _transactions;
        }

        foreach (Transaction transaction in transactions)
        {
            XmlSerializers.Transaction.Serialize(writer, transaction);
        }
    }
}
