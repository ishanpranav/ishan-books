using System;
using System.Collections.Generic;

namespace Liber.TaxNodes;

public class SumTaxNode : TaxNode
{
    public IReadOnlyCollection<TaxNode> Addends { get; set; } = new List<TaxNode>();

    protected override decimal EvaluateCore(DateTime started, DateTime posted)
    {
        decimal sum = 0;

        foreach (TaxNode addend in Addends)
        {
            sum += addend.Evaluate(started, posted);
        }

        return sum;
    }
}
