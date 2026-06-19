// Company.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Liber.Properties;

namespace Liber;

/// <summary>
/// Represents an individual, sole proprietorship, partnership, corporation, or other entity.
/// </summary>
public class Company : ICloneable
{
    private readonly Dictionary<Guid, Account> _accounts;
    private readonly Dictionary<Guid, Transaction> _transactions;
    private readonly SortedSet<Account> _sortedAccounts;
    private readonly SortedSet<Transaction> _sortedTransactions;
    private readonly SortedDictionary<string, int> _names = new SortedDictionary<string, int>();

    private string? _name;
    private CompanyType _type;
    private DateTime _fiscalYearStarted = new DateTime(DateTime.Today.Year, month: 1, day: 1);
    private DateTime _fiscalYearPosted = new DateTime(DateTime.Today.Year, month: 12, day: 31);
    private ReportingPeriod _reportingPeriod;
    private DateTime? _customStarted;
    private DateTime? _customPosted;

    public IReadOnlyCollection<Account> Accounts
    {
        get
        {
            return _sortedAccounts;
        }
    }

    public IReadOnlyCollection<Transaction> Transactions
    {
        get
        {
            return _sortedTransactions;
        }
    }

    public decimal NextAccountNumber { get; internal set; } = 1;
    public decimal NextTransactionNumber { get; internal set; } = 1;

    public string? Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (_name != value)
            {
                _name = value;

                NameChanged?.Invoke(sender: this, System.EventArgs.Empty);
            }
        }
    }

    [JsonIgnore]
    public string DisplayName
    {
        get
        {
            return Name ?? Resources.DefaultCompanyName;
        }
    }

    public CompanyType Type
    {
        get
        {
            return _type;
        }
        set
        {
            if (_type != value)
            {
                _type = value;

                TypeChanged?.Invoke(sender: this, System.EventArgs.Empty);
            }
        }
    }

    public Color Color { get; set; } = ColorTranslator.FromHtml("#ccedff");
    public Guid EquityAccountId { get; set; }
    public Guid OtherEquityAccountId { get; set; }

    public DateTime FiscalYearStarted
    {
        get
        {
            return _fiscalYearStarted;
        }
        set
        {
            value = new DateTime(DateTime.Today.Year, value.Month, value.Day);

            if (_fiscalYearStarted != value)
            {
                _fiscalYearStarted = value;

                ReportingChanged?.Invoke(sender: this, System.EventArgs.Empty);
            }
        }
    }

    public DateTime FiscalYearPosted
    {
        get
        {
            return _fiscalYearPosted;
        }
        set
        {
            value = new DateTime(DateTime.Today.Year, value.Month, value.Day);

            if (_fiscalYearPosted != value)
            {
                _fiscalYearPosted = value;

                ReportingChanged?.Invoke(sender: this, System.EventArgs.Empty);
            }
        }
    }

    public ReportingPeriod ReportingPeriod
    {
        get
        {
            return _reportingPeriod;
        }
        set
        {
            if (_reportingPeriod != value)
            {
                _reportingPeriod = value;

                ReportingChanged?.Invoke(sender: this, EventArgs.Empty);
            }
        }
    }

    public DateTime? CustomStarted
    {
        get
        {
            return _customStarted;
        }
        set
        {
            if (_customStarted != value)
            {
                _customStarted = value;

                ReportingChanged?.Invoke(sender: this, EventArgs.Empty);
            }
        }
    }

    public DateTime? CustomPosted
    {
        get
        {
            return _customPosted;
        }
        set
        {
            if (_customPosted != value)
            {
                _customPosted = value;

                ReportingChanged?.Invoke(sender: this, EventArgs.Empty);
            }
        }
    }

    [JsonIgnore]
    public DateTime Started
    {
        get
        {
            if (ReportingPeriod == ReportingPeriod.Custom && CustomStarted != null)
            {
                return CustomStarted.Value;
            }

            DateTime current = new DateTime(DateTime.Today.Year, FiscalYearStarted.Month, FiscalYearStarted.Day);
            DateTime result = current > DateTime.Today ? current.AddYears(-1) : current;

            if (ReportingPeriod == ReportingPeriod.PreviousFiscalYear)
            {
                return result.AddYears(-1);
            }

            return result;
        }
    }

    [JsonIgnore]
    public DateTime Posted
    {
        get
        {
            if (ReportingPeriod == ReportingPeriod.Custom && CustomPosted != null)
            {
                return CustomPosted.Value;
            }

            if (ReportingPeriod == ReportingPeriod.FiscalYearToDate)
            {
                return DateTime.Today;
            }

            DateTime current = new DateTime(DateTime.Today.Year, FiscalYearPosted.Month, FiscalYearPosted.Day);
            DateTime result = current < Started ? current.AddYears(1) : current;

            if (ReportingPeriod == ReportingPeriod.PreviousFiscalYear)
            {
                return result.AddYears(-1);
            }

            return result;
        }
    }

    [JsonIgnore]
    public string? Password { get; set; }

    [JsonIgnore]
    public Transaction? FirstTransaction
    {
        get
        {
            return _sortedTransactions.Min;
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

    public event EventHandler? NameChanged;
    public event EventHandler? TypeChanged;
    public event EventHandler? ReportingChanged;
    public event EventHandler<GuidEventArgs>? AccountAdded;
    public event EventHandler<GuidEventArgs>? AccountUpdated;
    public event EventHandler<GuidEventArgs>? AccountRemoved;
    public event EventHandler<GuidEventArgs>? TransactionAdded;
    public event EventHandler<GuidEventArgs>? TransactionUpdated;
    public event EventHandler<GuidEventArgs>? TransactionReconciled;
    public event EventHandler<GuidEventArgs>? TransactionRemoved;

    /// <summary>
    /// Initializes a new instance of the <see cref="Company"/> class.
    /// </summary>
    public Company()
    {
        _accounts = new Dictionary<Guid, Account>();
        _sortedAccounts = new SortedSet<Account>();
        _transactions = new Dictionary<Guid, Transaction>();
        _sortedTransactions = new SortedSet<Transaction>();

        ResetEquityAccount();
        ResetOtherEquityAccount();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Company"/> class with the specified accounts, journal entries, account number sequence, and journal entry number sequence.
    /// </summary>
    /// <param name="accounts">The account dictionary.</param>
    /// <param name="transactions">The transaction collection.</param>
    /// <param name="nextAccountNumber">The next account number to be assigned.</param>
    [JsonConstructor]
    public Company(
        IReadOnlyCollection<Account> accounts,
        IReadOnlyCollection<Transaction> transactions,
        decimal nextAccountNumber,
        decimal nextTransactionNumber)
    {
        _accounts = accounts.ToDictionary(x => x.Id);
        _sortedAccounts = new SortedSet<Account>(accounts);
        _transactions = transactions.ToDictionary(x => x.Id);
        _sortedTransactions = new SortedSet<Transaction>(transactions);
        NextAccountNumber = nextAccountNumber;
        NextTransactionNumber = nextTransactionNumber;

        foreach (Account account in accounts)
        {
            InitializeAccount(account);
            AddChild(account, account.ParentId);
        }

        foreach (Transaction transaction in transactions)
        {
            InitializeTransaction(transaction);
            AddName(transaction, transaction.Name);
            AddLines(transaction, transaction.Lines);
        }
    }

    public void ResetEquityAccount()
    {
        Account value = new Account()
        {
            Name = Resources.DefaultEquityAccountName,
            Type = AccountType.Equity
        };

        AddAccount(value, Guid.Empty);

        EquityAccountId = value.Id;
    }

    public void ResetOtherEquityAccount()
    {
        Account value = new Account()
        {
            Name = Resources.DefaultOtherEquityAccountName,
            Type = AccountType.Equity,
            CashFlow = CashFlow.OtherEquity
        };

        AddAccount(value, Guid.Empty);

        OtherEquityAccountId = value.Id;
    }

    public string[] GetNames()
    {
        string[] result = new string[_names.Count];

        _names.Keys.CopyTo(result, index: 0);

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

    private void InitializeAccount(Account value)
    {
        if (string.IsNullOrWhiteSpace(value.Memo))
        {
            value.Memo = null;
        }

        if (string.IsNullOrWhiteSpace(value.Description))
        {
            value.Description = null;
        }

        if (value.Color == Color)
        {
            value.Color = default;
        }
    }

    public Account GetAccount(Guid id)
    {
        return _accounts[id];
    }

    public void AddAccount(Account value, Guid parentId)
    {
        if (value.Id != Guid.Empty)
        {
            throw new InvalidOperationException();
        }

        Guid id = Guid.NewGuid();

        InitializeAccount(value);

        value.Id = id;

        AddChild(value, parentId);
        _accounts.Add(value.Id, value);
        _sortedAccounts.Add(value);

        NextAccountNumber = Math.Max(value.Number, NextAccountNumber) + 1;

        AccountAdded?.Invoke(sender: this, new GuidEventArgs(value.Id));
    }

    public void UpdateAccount(Guid id, Guid parentId, decimal number, string name, AccountType type)
    {
        if (!_accounts.TryGetValue(id, out Account? value))
        {
            throw new InvalidOperationException();
        }

        InitializeAccount(value);

        if (parentId != value.ParentId)
        {
            RemoveChild(value);
            AddChild(value, parentId);
        }

        NextAccountNumber = Math.Max(value.Number, NextAccountNumber);

        _sortedAccounts.Remove(value);

        value.Number = number;
        value.Name = name;
        value.Type = type;

        _sortedAccounts.Add(value);
        AccountUpdated?.Invoke(sender: this, new GuidEventArgs(id));
    }

    public bool RemoveAccount(Guid id)
    {
        if (!_accounts.TryGetValue(id, out Account? value))
        {
            throw new InvalidOperationException();
        }

        if (EquityAccountId == id || OtherEquityAccountId == id)
        {
            return false;
        }

        if (value.children.Count > 0 || value.lines.Count > 0)
        {
            return false;
        }

        RemoveChild(value);
        _accounts.Remove(id);
        _sortedAccounts.Remove(value);
        AccountRemoved?.Invoke(sender: this, new GuidEventArgs(id));

        return true;
    }

    public Transaction GetTransaction(Guid id)
    {
        return _transactions[id];
    }

    public Transaction? GetTransactionBefore(Transaction value)
    {
        if (_sortedTransactions.Min == null || _sortedTransactions.Min >= value)
        {
            return null;
        }

        return _sortedTransactions
            .GetViewBetween(_sortedTransactions.Min, value)
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

        return _sortedTransactions
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
        return _sortedTransactions.GetViewBetween(
            new Transaction(started),
            new Transaction(posted) { Posted = posted.AddDays(1) });
    }

    private static void InitializeTransaction(Transaction value)
    {
        if (string.IsNullOrWhiteSpace(value.Memo))
        {
            value.Memo = null;
        }
    }

    private void AddName(Transaction value, string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            value.Name = null;

            return;
        }

        value.Name = name.Trim();
        _names[value.Name] = _names.GetValueOrDefault(value.Name) + 1;
    }

    private void RemoveName(Transaction value)
    {
        if (string.IsNullOrWhiteSpace(value.Name))
        {
            return;
        }

        int count = _names[value.Name] - 1;

        if (count < 1)
        {
            _names.Remove(value.Name);

            return;
        }

        _names[value.Name] = count;
    }

    private void AddLines(Transaction value, IReadOnlyCollection<Line> lines)
    {
        foreach (Line line in lines)
        {
            if (line.Balance == 0)
            {
                continue;
            }

            Account account = _accounts[line.AccountId];

            if (string.IsNullOrWhiteSpace(line.Description))
            {
                line.Description = null;
            }

            line.transaction = value;
            account.lines.Add(line);

            if (line.Reconciled != null)
            {
                DateTime reconciled = line.Reconciled.Value;

                if (account.Reconciled == null || reconciled > account.Reconciled)
                {
                    account.Reconciled = line.Reconciled;
                }
            }
        }
    }

    private void RemoveLines(Transaction value)
    {
        foreach (Line line in value.lines)
        {
            _accounts[line.AccountId].lines.Remove(line);
            line.transaction = null;
        }
    }

    private static void TrialBalance(IReadOnlyCollection<Line> lines)
    {
        decimal trialBalance = 0;

        foreach (Line line in lines)
        {
            trialBalance += line.Balance;
        }

        if (trialBalance > 0)
        {
            throw new ArgumentException(message: null, nameof(lines));
        }
    }

    public void AddTransaction(Transaction value, string? name, IReadOnlyCollection<Line> lines)
    {
        if (value.Id != Guid.Empty)
        {
            throw new InvalidOperationException();
        }

        TrialBalance(lines);

        Guid id = Guid.NewGuid();

        InitializeTransaction(value);

        value.Id = id;

        AddName(value, name);
        AddLines(value, lines);

        if (value.lines.Count == 0)
        {
            foreach (Line line in lines)
            {
                if (line.Balance == 0)
                {
                    continue;
                }

                value.lines.Add(line);
            }
        }

        _transactions.Add(id, value);
        _sortedTransactions.Add(value);

        NextTransactionNumber = Math.Max(value.Number, NextTransactionNumber) + 1;

        TransactionAdded?.Invoke(sender: this, new GuidEventArgs(id));
    }

    public void UpdateTransaction(Guid id, decimal number, string? name, DateTime posted, IReadOnlyCollection<Line> lines)
    {
        if (!_transactions.TryGetValue(id, out Transaction? value))
        {
            throw new InvalidOperationException();
        }

        TrialBalance(lines);
        InitializeTransaction(value);

        if (name != value.Name)
        {
            RemoveName(value);
            AddName(value, name);
        }

        RemoveLines(value);
        AddLines(value, lines);

        if (lines != value.lines)
        {
            value.lines.Clear();

            foreach (Line line in lines)
            {
                if (line.Balance == 0)
                {
                    continue;
                }

                value.lines.Add(line);
            }
        }

        NextTransactionNumber = Math.Max(value.Number, NextTransactionNumber);

        _sortedTransactions.Remove(value);

        value.Number = number;
        value.Posted = posted;

        _sortedTransactions.Add(value);
        TransactionUpdated?.Invoke(sender: this, new GuidEventArgs(id));
    }

    public bool RemoveTransaction(Guid id)
    {
        if (!_transactions.TryGetValue(id, out Transaction? value))
        {
            throw new InvalidOperationException();
        }

        RemoveLines(value);
        _transactions.Remove(id);
        _sortedTransactions.Remove(value);
        TransactionRemoved?.Invoke(sender: this, new GuidEventArgs(id));

        return true;
    }

    public IEnumerable<Transaction> GetTransfers()
    {
        foreach (Transaction transaction in _sortedTransactions)
        {
            if (transaction.Lines.All(x => _accounts[x.AccountId].Type.IsAsset()))
            {
                yield return transaction;
            }
        }
    }

    public IEnumerable<Line> GetChecks()
    {
        foreach (Account account in _sortedAccounts)
        {
            if (account.Type != AccountType.Bank)
            {
                continue;
            }

            foreach (Line line in account.lines.Order())
            {
                if (line.Transaction.Name != null && line.Balance < 0)
                {
                    yield return line;
                }
            }
        }
    }

    public decimal GetEquity(DateTime posted, Regex filter)
    {
        decimal result = _accounts[EquityAccountId].GetBalance(posted, filter);

        foreach (Account account in _accounts.Values)
        {
            if (account.Type.IsTemporary())
            {
                result += account.GetBalance(posted, filter);
            }
        }

        return result;
    }

    public decimal GetBalance(Account account, Regex filter)
    {
        if (account.Type.IsTemporary())
        {
            return account.GetBalance(Started, Posted, filter);
        }

        return account.GetBalance(Posted, filter);
    }

    public decimal Reconcile(Account account, DateTime reconciled, decimal endingBalance, IEnumerable<Line> lines)
    {
        if (account.Type.IsTemporary())
        {
            throw new ArgumentException(message: null, nameof(account));
        }

        decimal reconciledBalance = account.GetReconciledBalance();
        HashSet<Guid> transactionIds = new HashSet<Guid>();

        foreach (Line line in lines)
        {
            if (line.AccountId != account.Id)
            {
                throw new ArgumentException(message: null, nameof(lines));
            }

            reconciledBalance += line.Balance;

            transactionIds.Add(line.Transaction.Id);
        }

        if (endingBalance != reconciledBalance)
        {
            return endingBalance - reconciledBalance;
        }

        foreach (Line line in lines)
        {
            line.Reconciled = reconciled;
        }

        foreach (Guid transactionId in transactionIds)
        {
            TransactionReconciled?.Invoke(sender: this, new GuidEventArgs(transactionId));
        }

        return 0;
    }

    private BalanceInfo ComputeBalances(Account account, ReportTypes type, DateTime started, DateTime posted, Regex filter)
    {
        if (account.Id == EquityAccountId)
        {
            BalanceInfo result = new BalanceInfo()
            {
                Previous = GetEquity(started.AddDays(-1), filter)
            };

            if (type == ReportTypes.None || type.HasFlag(ReportTypes.CurrentPosted))
            {
                result.Balance = GetEquity(posted, filter);
            }

            if (type.HasFlag(ReportTypes.CurrentStarted))
            {
                result.Balance = GetEquity(started.AddDays(-1), filter);
            }

            return result;
        }

        if (account.Id == OtherEquityAccountId)
        {
            return new BalanceInfo()
            {
                Balance = account.GetBalance(posted, filter),
                Previous = account.GetBalance(started, filter)
            };
        }

        if (account.Type.IsTemporary())
        {
            return new BalanceInfo()
            {
                Balance = account.GetBalance(started, posted, filter),
                Previous = account.GetBalance(started - (posted - started), started, filter)
            };
        }

        return new BalanceInfo()
        {
            Balance = account.GetBalance(posted, filter),
            Previous = account.GetBalance(started.AddDays(-1), filter),
            AverageDailyBalance = account.GetAverageDailyBalance(started, posted, filter)
        };
    }

    private void ComputeSubtreeBalances(
        Account account,
        IReadOnlySet<Account> visibleAccounts,
        ReportTypes type,
        DateTime started,
        DateTime posted,
        Regex filter,
        Dictionary<ParentKey, BalanceInfo> results)
    {
        BalanceInfo balances = ComputeBalances(account, type, started, posted, filter);
        IEnumerable<Account> visibleChildren = account.children.Where(visibleAccounts.Contains);

        if (!visibleChildren.Any())
        {
            ParentKey key = new ParentKey(account);

            if (results.TryGetValue(key, out BalanceInfo? existing))
            {
                existing.Balance += balances.Balance;
                existing.Previous += balances.Previous;
                existing.AverageDailyBalance += balances.AverageDailyBalance;
            }
            else
            {
                results[key] = balances;
            }
        }

        foreach (Account child in visibleChildren)
        {
            ComputeSubtreeBalances(child, visibleAccounts, type, started, posted, filter, results);
        }
    }

    public IEnumerable<(Account Account, BalanceInfo Balances)> GetBalances(
        IReadOnlySet<Account> visibleAccounts,
        ReportTypes type,
        DateTime started,
        DateTime posted,
        Regex filter)
    {
        foreach (Account account in visibleAccounts)
        {
            yield return (account, ComputeBalances(account, type, started, posted, filter));
        }
    }

    private IEnumerable<Account> GetVisibleRoots(IReadOnlySet<Account> visibleAccounts)
    {
        return visibleAccounts.Where(x => x.ParentId == Guid.Empty || !visibleAccounts.Contains(_accounts[x.ParentId]));
    }

    public IEnumerable<(Account Parent, ParentKey Key, BalanceInfo Balances)> GetBalancesByKey(
        IReadOnlySet<Account> visibleAccounts,
        ReportTypes type,
        DateTime started,
        DateTime posted,
        Regex filter)
    {
        foreach (Account root in GetVisibleRoots(visibleAccounts))
        {
            Dictionary<ParentKey, BalanceInfo> results = new Dictionary<ParentKey, BalanceInfo>();

            ComputeSubtreeBalances(root, visibleAccounts, type, started, posted, filter, results);

            foreach (KeyValuePair<ParentKey, BalanceInfo> entry in results)
            {
                yield return (root, entry.Key, entry.Value);
            }
        }
    }

    public IEnumerable<(AccountType Type, BalanceInfo Balances)> GetBalancesByType(
        IReadOnlySet<Account> visibleAccounts,
        ReportTypes type,
        DateTime started,
        DateTime posted,
        Regex filter)
    {
        foreach (IGrouping<AccountType, (Account, ParentKey, BalanceInfo)> group in GetBalancesByKey(
                visibleAccounts,
                type,
                started,
                posted,
                filter)
            .GroupBy(x => x.Key.Type))
        {
            BalanceInfo result = new BalanceInfo();

            foreach ((Account _, ParentKey _, BalanceInfo balance) in group)
            {
                result.Balance += balance.Balance;
                result.Previous = balance.Previous;
                result.AverageDailyBalance += balance.AverageDailyBalance;
            }

            yield return (group.Key, result);
        }
    }

    public IEnumerable<(Account Parent, BalanceInfo Balances)> GetBalancesByParent(
        IReadOnlySet<Account> visibleAccounts,
        ReportTypes type,
        DateTime started,
        DateTime posted,
        Regex filter)
    {
        foreach (IGrouping<Account, (Account, ParentKey, BalanceInfo)> group in GetBalancesByKey(
                visibleAccounts,
                type,
                started,
                posted,
                filter)
            .GroupBy(x => x.Parent))
        {
            BalanceInfo result = new BalanceInfo();

            foreach ((Account _, ParentKey _, BalanceInfo balance) in group)
            {
                result.Balance += balance.Balance;
                result.Previous = balance.Previous;
                result.AverageDailyBalance += balance.AverageDailyBalance;
            }

            yield return (group.Key, result);
        }
    }

    public IEnumerable<(AccountType Type, Account Parent, BalanceInfo Balances)> GetBalancesByTypeAndParent(
        IReadOnlySet<Account> visibleAccounts,
        ReportTypes type,
        DateTime started,
        DateTime posted,
        Regex filter)
    {
        foreach (IGrouping<(AccountType Type, Account Parent), (Account, ParentKey, BalanceInfo)> group in GetBalancesByKey(
                visibleAccounts,
                type,
                started,
                posted,
                filter)
            .GroupBy(x => (x.Key.Type, x.Parent)))
        {
            BalanceInfo result = new BalanceInfo();

            foreach ((Account _, ParentKey _, BalanceInfo balance) in group)
            {
                result.Balance += balance.Balance;
                result.Previous = balance.Previous;
                result.AverageDailyBalance += balance.AverageDailyBalance;
            }

            yield return (group.Key.Type, group.Key.Parent, result);
        }
    }

    public IEnumerable<(AccountType Type, Account Account, int Depth, BalanceInfo Balances)> GetBalancesByTree(
        IReadOnlySet<Account> visibleAccounts,
        ReportTypes type,
        DateTime started,
        DateTime posted,
        Regex filter)
    {
        foreach (IGrouping<AccountType, Account> typeGroup in GetVisibleRoots(visibleAccounts).GroupBy(x => x.Type))
        {
            foreach (Account root in typeGroup)
            {
                foreach ((AccountType, Account, int, BalanceInfo) entry in EnumerateSubtree(
                    root,
                    typeGroup.Key,
                    depth: 0,
                    visibleAccounts,
                    type,
                    started,
                    posted,
                    filter))
                {
                    yield return entry;
                }
            }
        }
    }

    private IEnumerable<(AccountType Type, Account Account, int Depth, BalanceInfo Balances)> EnumerateSubtree(
        Account root,
        AccountType currentType,
        int depth,
        IReadOnlySet<Account> visibleAccounts,
        ReportTypes type,
        DateTime started,
        DateTime posted,
        Regex filter)
    {
        List<Account> sameTypeChildren = new List<Account>();
        List<Account> crossTypeChildren = new List<Account>();

        foreach (Account child in root.children)
        {
            if (!visibleAccounts.Contains(child))
            {
                continue;
            }

            if (child.Type == root.Type)
            {
                sameTypeChildren.Add(child);
            }
            else
            {
                crossTypeChildren.Add(child);
            }
        }

        if (root.Type == currentType || sameTypeChildren.Count > 0)
        {
            yield return (currentType, root, depth, ComputeBalances(root, type, started, posted, filter));

            foreach (Account child in sameTypeChildren)
            {
                foreach ((AccountType, Account, int, BalanceInfo) entry in
                    EnumerateSubtree(child, currentType, depth + 1, visibleAccounts, type, started, posted, filter))
                {
                    yield return entry;
                }
            }
        }

        foreach (IGrouping<AccountType, Account> crossGroup in crossTypeChildren.GroupBy(x => x.Type))
        {
            yield return (crossGroup.Key, root, Depth: 0, ComputeBalances(root, type, started, posted, filter));

            foreach (Account child in crossGroup)
            {
                foreach ((AccountType, Account, int, BalanceInfo) entry in
                        EnumerateSubtree(child, currentType, depth + 1, visibleAccounts, type, started, posted, filter))
                {
                    yield return entry;
                }
            }
        }
    }

    public Color GetColorOrDefault(Account account)
    {
        Color result = account.Color;

        if (result.IsEmpty)
        {
            result = Color;
        }

        if (result == Color && account.ParentId != Guid.Empty)
        {
            result = _accounts[account.ParentId].Color;

            if (result.IsEmpty)
            {
                result = Color;
            }
        }

        return result;
    }

    public override string ToString()
    {
        return DisplayName;
    }

    public Company Clone()
    {
        return new Company(_accounts.Values, _transactions.Values, NextAccountNumber, NextTransactionNumber);
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}
