using System;

namespace Liber;

public class Line
{
    public Guid AccountKey { get; set; }
    public decimal Balance { get; set; }

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

    public string? Description { get; set; }
}
