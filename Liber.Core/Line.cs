using CsvHelper.Configuration.Attributes;
using MessagePack;
using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Liber;

[MessagePackObject]
public class Line
{
    [Ignore]
    [Key(0)]
    public Guid AccountKey { get; set; }

    [Index(11)]
    [Key(1)]
    [Name("Value Num.")]
    [NumberStyles(NumberStyles.Currency)]
    public decimal Balance { get; set; }

    [IgnoreMember]
    [JsonIgnore]
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

    [IgnoreMember]
    [JsonIgnore]
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

    [Index(8)]
    [Key(2)]
    [Name("Memo")]
    [NullValues("")]
    [Optional]
    public string? Description { get; set; }
}
