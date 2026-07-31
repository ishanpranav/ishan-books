// Program.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Liber.Trades;

namespace Liber.BrokerageConsole;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: Liber.BrokerageConsole <path>");

            return;
        }

        string path = args[0];

        try
        {
            ProcessFile(path);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Exception: {exception.Message}");
        }
    }

    private static void ProcessFile(string path)
    {
        using FileStream input = File.OpenRead(path);

        Trade[]? trades = JsonSerializer.Deserialize<Trade[]>(input);

        if (trades is null)
        {
            Console.WriteLine("No trades.");

            return;
        }

        foreach (Trade trade in trades)
        {
            Console.WriteLine(trade);
        }
    }
}
