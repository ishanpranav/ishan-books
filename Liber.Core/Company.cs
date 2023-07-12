using MessagePack;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Liber;

[MessagePackObject]
public sealed class Company
{
    private readonly Dictionary<Guid, Account> _accounts;

    public Company()
    {
        _accounts = new Dictionary<Guid, Account>();
    }

    [JsonConstructor]
    [SerializationConstructor]
    public Company(IReadOnlyDictionary<Guid, Account> accounts, decimal nextAccountNumber)
    {
        _accounts = new Dictionary<Guid, Account>(accounts);

        foreach (Account account in accounts.Values)
        {
            if (account.ParentKey != Guid.Empty)
            {
                accounts[account.ParentKey].children.Add(account);
            }
        }

        NextAccountNumber = nextAccountNumber;
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
    public decimal NextAccountNumber { get; private set; } = 1;

    [Key(2)]
    public string? Name { get; set; }

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

    public void CopyTo(Company other)
    {
        other.Name = Name;
        other.NextAccountNumber = NextAccountNumber;

        other._accounts.Clear();

        foreach (KeyValuePair<Guid, Account> account in _accounts)
        {
            other._accounts[account.Key] = account.Value;
        }
    }
}
