// ReverseDateTimeComparer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace System.Collections.Specialized;

public class ReverseDateTimeComparer : Comparer<DateTime>
{
    private static ReverseDateTimeComparer? s_instance;

    public static new ReverseDateTimeComparer Default
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new ReverseDateTimeComparer();
            }

            return s_instance;
        }
    }

    public override int Compare(DateTime x, DateTime y)
    {
        return y.CompareTo(x);
    }
}
