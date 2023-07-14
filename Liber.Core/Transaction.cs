using MessagePack;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Liber;

public class Transaction
{
    public Transaction()
    {
        Lines = new HashSet<Line>();
    }

    [JsonConstructor]
    [SerializationConstructor]
    public Transaction(ICollection<Line> lines)
    {
        Lines = lines;
    }

    public decimal Number { get; set; }
    public DateTime Posted { get; set; }
    public string? Description { get; set; }

    public decimal Balance
    {
        get
        {
            decimal result = 0;

            foreach (Line line in Lines)
            {
                result += line.Balance;
            }

            return result;
        }
    }

    public decimal Debit
    {
        get
        {
            if (Balance < 0)
            {
                return 0;
            }

            return Balance;
        }
    }

    public decimal Credit
    {
        get
        {
            if (Balance > 0)
            {
                return 0;
            }

            return -Balance;
        }
    }

    public ICollection<Line> Lines { get; } 
}
