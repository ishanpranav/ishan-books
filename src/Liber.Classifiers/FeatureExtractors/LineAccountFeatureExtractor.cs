// LineAccountFeatureExtractor.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Liber.Classifiers.FeatureExtractors;

internal class LineAccountFeatureExtractor : IFeatureExtractor<Line, Guid>
{
    private static readonly char[] s_delimiters = new char[] { ' ', ':' };

    public IEnumerable<Feature> GetFeatures(Line item)
    {
        Transaction transaction = item.Transaction;
        int weight = transaction.Lines.Count;

        foreach (Line cousin in transaction.Lines)
        {
            int cousinWeight = cousin == item ? weight : 1;

            foreach (Feature feature in GetFeaturesInternal(cousin))
            {
                yield return new Feature("cousin" + feature.Type, feature.Value, cousinWeight);
            }
        }

        yield return new Feature("name", transaction.Name ?? string.Empty, weight);
        yield return new Feature("memo", transaction.Memo ?? string.Empty, weight);

        foreach (string token in Tokenize(transaction.Name))
        {
            yield return new Feature("nametoken", token, weight);
        }

        foreach (string token in Tokenize(transaction.Memo))
        {
            yield return new Feature("memotoken", token, weight);
        }

        DateTime posted = transaction.Posted;

        yield return new Feature("dayofweek", posted.DayOfWeek.ToString(), weight);
        yield return new Feature("year", posted.Year.ToString(), weight);
        yield return new Feature("month", posted.Month.ToString(), weight);

        int day = posted.Day;

        if (day <= 10)
        {
            yield return new Feature("day", "1/3", weight);
        }
        else if (day <= 20)
        {
            yield return new Feature("day", "2/3", weight);
        }
        else
        {
            yield return new Feature("day", "3/3", weight);
        }

        if (day <= 15)
        {
            yield return new Feature("day", "1/2", weight);
        }
        else
        {
            yield return new Feature("day", "2/2", weight);
        }
    }

    private static IEnumerable<Feature> GetFeaturesInternal(Line item)
    {
        foreach (string token in Tokenize(item.Description))
        {
            yield return new Feature("description", token, weight: 1);
        }

        decimal balance = item.Balance;

        if (balance >= 0)
        {
            yield return new Feature("balance", "debit", weight: 1);
        }
        else
        {
            yield return new Feature("balance", "credit", weight: 1);
        }

        if (balance != 0)
        {
            yield return new Feature("balance", ((int)double.Log10((double)decimal.Abs(balance)) + 1).ToString(), weight: 1);
        }
    }

    private static IEnumerable<string> Tokenize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Enumerable.Empty<string>();
        }

        return value
            .ToLowerInvariant()
            .Split(s_delimiters, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(x => x.Length > 1);
    }

    public Guid GetLabel(Line item)
    {
        return item.AccountId;
    }
}
