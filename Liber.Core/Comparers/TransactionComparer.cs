using Liber;
using System.Collections.Generic;

namespace System.Collections.Specialized;

internal sealed class TransactionComparer : Comparer<Transaction>
{
    private static TransactionComparer? s_instance;

    public static new TransactionComparer Default
    {
        get
        {
            s_instance ??= new TransactionComparer();

            return s_instance;
        }
    }

    public override int Compare(Transaction? x, Transaction? y)
    {
        if (x == y)
        {
            return 0;
        }

        if (x == null)
        {
            return 1;
        }

        if (y == null)
        {
            return -1;
        }

        int result = x.Posted.CompareTo(y.Posted);

        if (result != 0)
        {
            return result;
        }

        result = x.Number.CompareTo(y.Number);

        if (result != 0)
        {
            return result;
        }

        return 1;
    }
}
