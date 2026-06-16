// Anonymizer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Humanizer;

namespace Liber;

public class Anonymizer
{
    private readonly int _dayOffset;
    private readonly double _balanceFactor;
    private readonly Dictionary<string, string> _names = new Dictionary<string, string>();
    private readonly Dictionary<Guid, string> _accountNames = new Dictionary<Guid, string>();
    private readonly Dictionary<AccountType, int> _accountTypeCounts = new Dictionary<AccountType, int>();
    private readonly Company _company;

    private int _nameCount;
    private decimal _nextAccountNumber;
    private decimal _nextTransactionNumber;

    public Anonymizer(Company company) : this(company, Random.Shared) { }

    public Anonymizer(Company company, Random random)
    {
        _company = company;
        _dayOffset = random.Next(minValue: -363, maxValue: 364);
        _balanceFactor = random.NextDouble() * 10;
    }

    public void Anonymize()
    {
        _company.Name = null;
        _company.Color = Color.Empty;
        _company.FiscalYearStarted = Offset(_company.FiscalYearStarted);
        _company.FiscalYearPosted = Offset(_company.FiscalYearPosted);

        if (_company.CustomStarted != null)
        {
            _company.CustomStarted = Offset(_company.CustomStarted.Value);
        }

        if (_company.CustomPosted != null)
        {
            _company.CustomPosted = Offset(_company.CustomPosted.Value);
        }

        foreach (Account account in _company.Accounts.ToList())
        {
            Anonymize(account);
        }

        foreach (Transaction transaction in _company.Transactions.ToList())
        {
            Anonymize(transaction);
        }

        _company.NextAccountNumber = _nextAccountNumber;
        _company.NextTransactionNumber = _nextTransactionNumber;
    }

    private DateTime Offset(DateTime value)
    {
        return value.AddDays(_dayOffset);
    }

    public void Anonymize(Account value)
    {
        if (!_accountNames.TryGetValue(value.Id, out string? name))
        {
            int count = _accountTypeCounts.GetValueOrDefault(value.Type) + 1;

            _accountTypeCounts[value.Type] = count;
            name = $"{value.Type.Humanize()} {count}";
            _accountNames[value.Id] = name;
        }

        _nextAccountNumber++;
        value.Memo = null;
        value.Description = null;
        value.Color = Color.Empty;

        _company.UpdateAccount(value.Id, value.ParentId, _nextAccountNumber, name, value.Type);
    }

    public void Anonymize(Transaction value)
    {
        string? name;

        if (value.Name == null)
        {
            name = null;
        }
        else if (!_names.TryGetValue(value.Name, out name))
        {
            _nameCount++;
            name = $"Name {_nameCount}";
            _names[value.Name] = name;
        }

        value.Memo = null;

        Line[]? lines;

        if (value.Lines.Count > 0)
        {
            lines = new Line[value.Lines.Count];

            int i = 0;
            decimal trialBalance = 0;
            
            foreach (Line line in value.Lines)
            {
                decimal balance = (decimal)double.Round((double)line.Balance * _balanceFactor, 2);

                lines[i] = new Line(
                    line.AccountId,
                    balance,
                    line.Description,
                    line.Reconciled == null ? null : Offset(line.Reconciled.Value));
                trialBalance += balance;
                i++;
            }

            Line last = lines[lines.Length - 1];

            lines[lines.Length - 1] = new Line(
                last.AccountId,
                last.Balance - trialBalance,
                last.Description,
                last.Reconciled == null ? null : Offset(last.Reconciled.Value));
        }
        else
        {
            lines = Array.Empty<Line>();
        }

        _company.UpdateTransaction(value.Id, _nextTransactionNumber, name, Offset(value.Posted), lines);
        _nextTransactionNumber++;
    }
}
