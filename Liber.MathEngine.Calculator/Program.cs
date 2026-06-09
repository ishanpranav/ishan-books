// Program.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.MathEngine.Calculator;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            Console.WriteLine(Engine.Evaluate(args[0]));

            return;
        }

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("=");

            Console.ForegroundColor = ConsoleColor.Gray;

            string? line = Console.ReadLine();

            if (line == null)
            {
                return;
            }

            try
            {
                decimal result = Engine.Evaluate(line);

                Console.WriteLine("{0} = {1}", line, result);
            }
            catch (MismatchTokenizationException mismatchTokenizationException)
            {
                int left = mismatchTokenizationException.ExpectedPosition;
                int right = mismatchTokenizationException.Position;

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(line.AsSpan(start: 0, left));

                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write(mismatchTokenizationException.Expected);

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(line.AsSpan(left + 1, right - left - 1));

                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(mismatchTokenizationException.Current);

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine(line.AsSpan(right + 1));
                Console.WriteLine("\tSyntax error: {0}", mismatchTokenizationException.Message);
            }
            catch (TokenizationException tokenizationException)
            {
                int position = tokenizationException.Position;

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(line.AsSpan(start: 0, position));

                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(tokenizationException.Current);

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine(line.AsSpan(position + 1));
                Console.WriteLine("\tSyntax error: {0}", tokenizationException.Message);
            }
        }
    }
}
