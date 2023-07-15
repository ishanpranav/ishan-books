using MessagePack;
using System;
using System.Text.Json.Serialization;

namespace Liber;

public class Line
{
    [Key(0)]
    public Guid AccountKey { get; set; }

    [Key(1)]
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

    [Key(2)]
    public string? Description { get; set; }
}
