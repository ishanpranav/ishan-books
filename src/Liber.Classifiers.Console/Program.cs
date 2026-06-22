// Program.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Liber.Classifiers.NaiveBayesClassifiers;

namespace Liber.Classifiers.Console;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            System.Console.WriteLine("Usage: Liber.Classifiers.Console <path|json>");

            return;
        }

        Company? company = null;

        if (Path.Exists(args[0]))
        {
            using FileStream input = File.OpenRead(args[0]);

            company = JsonSerializer.Deserialize<Company>(input, SerializationOptions.Json);
        }

        if (company == null)
        {
            company = JsonSerializer.Deserialize<Company>(args[0], SerializationOptions.Json);
        }

        if (company == null)
        {
            System.Console.WriteLine("Usage: Liber.Classifiers.Console <path|json>");

            return;
        }

        List<Line> allLines = company.Accounts
            .SelectMany(x => x.Lines)
            .ToList();
        LineAccountNaiveBayesClassifier classifier = new LineAccountNaiveBayesClassifier(allLines);

        while (true)
        {
            try
            {
                System.Console.Write("Name: ");

                string? name = System.Console.ReadLine();

                System.Console.Write("Memo: ");

                string? memo = System.Console.ReadLine();

                System.Console.Write("Posted: ");

                string? postedText = System.Console.ReadLine();
                DateTime posted = postedText == null ? DateTime.Today : DateTime.Parse(postedText);
                List<Line> lines = new List<Line>();

                while (true)
                {
                    System.Console.Write("Balance & Description: ");

                    string? text = System.Console.ReadLine();

                    if (string.IsNullOrEmpty(text))
                    {
                        break;
                    }

                    string[] segments = text.Split();
                    decimal balance = segments.Length > 0 ? decimal.Parse(segments[0]) : 0;
                    string? description = segments.Length > 1 ? segments[1] : null;

                    lines.Add(new Line(Guid.Empty, balance, description, reconciled: null));
                }

                if (lines.Count == 1)
                {
                    lines.Add(new Line(Guid.Empty, -lines[0].Balance, description: null, reconciled: null));
                }

                Transaction transaction = new Transaction(
                    id: Guid.Empty,
                    number: 0,
                    name,
                    posted,
                    lines)
                {
                    Memo = memo
                };

                foreach (Line line in lines)
                {
                    IEnumerable<KeyValuePair<Guid, double>> results = classifier
                        .Score(line)
                        .OrderByDescending(x => x.Value)
                        .Take(3);

                    System.Console.WriteLine("{0} ({1}): ", line.Balance, line.Description);

                    foreach (KeyValuePair<Guid, double> entry in results)
                    {
                        System.Console.WriteLine("\t{0,12:f6}  {1}", entry.Value, company.GetAccount(entry.Key));
                    }

                    System.Console.WriteLine();
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("{0}\n", exception.Message);
            }
        }
    }
}
