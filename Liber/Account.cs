using CsvHelper.Configuration.Attributes;
using Liber.Properties;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Liber;

/// <summary>
/// 
/// </summary>
public class Account
{
    private string _name = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    [Name("Account Name")]
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(Resources.AccountNameArgumentException, nameof(value));
            }

            _name = value;
        }
    }

    [JsonIgnore]
    public string QualifiedName
    {
        get
        {
            return Number + " - " + Name;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [Name("Account Code")]
    public decimal Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public decimal Balance
    {
        get
        {
            decimal result = 0;

            foreach (Transaction line in Transactions)
            {
                result += line.Debit;
            }

            return Type.ToBalance(result);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Name("Type")]
    public AccountType Type { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [Ignore]
    public Guid CompanionId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public ICollection<Transaction> Transactions { get; } = new HashSet<Transaction>();

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [Name("Placeholder")]
    [BooleanTrueValues("T")]
    [BooleanFalseValues("F")]
    public bool Locked { get; set; }
}
