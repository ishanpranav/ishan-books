// Company.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Liber.Properties;

namespace Liber;

/// <summary>
/// Represents an individual, sole proprietorship, partnership, corporation, or other entity.
/// </summary>
public sealed class Company
{
    private readonly Dictionary<Guid, Account> _accounts;
    private readonly Dictionary<Guid, Transaction> _transactions;
    private readonly SortedSet<Account> _sortedAccounts;
    private readonly SortedSet<Transaction> _sortedTransactions;
    private readonly SortedSet<string> _names = new SortedSet<string>();

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

    public decimal NextAccountNumber { get; private set; } = 1;
    public decimal NextTransactionNumber { get; private set; } = 1;

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

    public event EventHandler? Invalidated;
    public event EventHandler? NameChanged;
    public event EventHandler? TypeChanged;
    public event EventHandler? ReportingChanged;
    public event EventHandler<GuidEventArgs>? AccountAdded;
    public event EventHandler<GuidEventArgs>? AccountUpdated;
    public event EventHandler<GuidEventArgs>? AccountRemoved;
    public event EventHandler<GuidEventArgs>? TransactionAdded;
    public event EventHandler<GuidEventArgs>? TransactionUpdated;
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

        value.Id = Guid.NewGuid();

        InitializeAccount(value);
        AddChild(value, parentId);
        _accounts.Add(value.Id, value);
        _sortedAccounts.Add(value);

        NextAccountNumber = Math.Max(value.Number, NextAccountNumber) + 1;

        AccountAdded?.Invoke(sender: this, new GuidEventArgs(value.Id));
    }

    public void UpdateAccount(Guid id, Guid parentId)
    {
        if (!_accounts.TryGetValue(id, out Account? value))
        {
            throw new InvalidOperationException();
        }

        if (parentId != value.ParentId)
        {
            RemoveChild(value);
            AddChild(value, parentId);
        }

        NextAccountNumber = Math.Max(value.Number, NextAccountNumber);

        _sortedAccounts.Remove(value);
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
            new Transaction() { Posted = started },
            new Transaction() { Posted = posted.AddDays(1) });
    }

    private void InitializeTransaction(Transaction value)
    {
        if (value.Balance != 0)
        {
            throw new InvalidOperationException($"Transaction {value.Id} does not balance");
        }

        if (string.IsNullOrWhiteSpace(value.Name))
        {
            value.Name = null;
        }
        else
        {
            value.Name = value.Name.Trim();

            _names.Add(value.Name);
        }

        if (string.IsNullOrWhiteSpace(value.Memo))
        {
            value.Memo = null;
        }
    }

    private void AddLines(Transaction value, IReadOnlyCollection<Line> lines)
    {
        foreach (Line line in lines)
        {
            Account account = _accounts[line.AccountId];

            if (string.IsNullOrWhiteSpace(line.Description))
            {
                line.Description = null;
            }

            line.Transaction = value;
            account.lines.Add(line);
        }
    }

    private void RemoveLines(Transaction value)
    {
        foreach (Line line in value.lines)
        {
            _accounts[line.AccountId].lines.Remove(line);
            line.Transaction = null;
        }

        value.lines.Clear();
    }

    public void AddTransaction(Transaction value, IReadOnlyCollection<Line> lines)
    {
        if (value.Id != Guid.Empty || value.Balance != 0)
        {
            throw new InvalidOperationException();
        }

        value.Id = Guid.NewGuid();

        InitializeTransaction(value);
        AddLines(value, lines);

        foreach (Line line in lines)
        {
            value.lines.Add(line);
        }

        _transactions.Add(value.Id, value);
        _sortedTransactions.Add(value);

        NextTransactionNumber = Math.Max(value.Number, NextTransactionNumber) + 1;

        TransactionAdded?.Invoke(sender: this, new GuidEventArgs(value.Id));
    }

    public void UpdateTransaction(Guid id, IReadOnlyCollection<Line> lines)
    {
        if (!_transactions.TryGetValue(id, out Transaction? value))
        {
            throw new InvalidOperationException();
        }

        InitializeTransaction(value);
        RemoveLines(value);
        AddLines(value, lines);

        foreach (Line line in lines)
        {
            value.lines.Add(line);
        }

        NextTransactionNumber = Math.Max(value.Number, NextTransactionNumber);

        _sortedTransactions.Remove(value);
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

    public void RemoveTransaction(Transaction value)
    {
        _transactions.Remove(value.Id);
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
                if (line.Transaction?.Name != null && line.Balance < 0)
                {
                    yield return line;
                }
            }
        }
    }

    public IEnumerable<Line> GetDeposits()
    {
        foreach (Account account in _sortedAccounts)
        {
            if (account.Type != AccountType.Bank)
            {
                continue;
            }

            foreach (Line line in account.Lines.Order())
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
        foreach (Account account in _sortedAccounts)
        {
            if (account.Type != AccountType.CreditCard)
            {
                continue;
            }

            foreach (Line line in account.lines.Order())
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
        foreach (Account account in _sortedAccounts)
        {
            if (account.Type != AccountType.CreditCard)
            {
                continue;
            }

            foreach (Line line in account.lines.Order())
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
        if (account.Id == EquityAccountId || account.Id == OtherEquityAccountId)
        {
            return account.GetBalance(Posted, filter);
        }

        if (account.Type.IsTemporary())
        {
            return account.GetBalance(Posted, Posted, filter);
        }

        return account.GetBalance(Posted, filter);
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
        other.FiscalYearStarted = FiscalYearStarted;
        other.FiscalYearPosted = FiscalYearPosted;
        other.ReportingPeriod = ReportingPeriod;
        other.CustomStarted = CustomStarted;
        other.CustomPosted = CustomPosted;

        other._accounts.Clear();
        other._sortedAccounts.Clear();
        other._transactions.Clear();
        other._sortedTransactions.Clear();
        other._names.Clear();

        foreach (KeyValuePair<Guid, Account> account in _accounts)
        {
            other._accounts[account.Key] = account.Value;

            other._sortedAccounts.Add(account.Value);
        }

        foreach (KeyValuePair<Guid, Transaction> transaction in _transactions)
        {
            other._transactions[transaction.Key] = transaction.Value;

            other._sortedTransactions.Add(transaction.Value);
        }

        foreach (string name in _names)
        {
            other._names.Add(name);
        }

        other.Invalidated?.Invoke(sender: this, EventArgs.Empty);
    }
}
