using System;
using System.Collections.Generic;

namespace Liber;

/// <summary>
/// 
/// </summary>
public class Transaction
{
    public int Id { get; set; }
    public DateTime Posted { get; set; }
    public string? Description { get; set; }
    public decimal Debit { get; set; }

    public ICollection<Line> Lines { get; } = new HashSet<Line>();
}
