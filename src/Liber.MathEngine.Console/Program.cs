// Program.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Globalization;
using Liber.MathEngine.Exceptions;
using Liber.MathEngine.Expressions;

namespace Liber.MathEngine.Console;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            System.Console.WriteLine(Parse(args[0]).Evaluate());

            return;
        }

        while (true)
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;

            System.Console.Write("=");

            System.Console.ForegroundColor = ConsoleColor.Gray;

            string? line = System.Console.ReadLine();

            if (line == null)
            {
                return;
            }

            try
            {
                IExpression expression = Parse(line);
                decimal result = expression.Evaluate();

                System.Console.WriteLine("\t{0} = {1}", expression, result);
            }
            catch (MismatchException mismatchException)
            {
                int left = mismatchException.ExpectedOffset;
                int right = mismatchException.Offset;
                int leftLength = mismatchException.ExpectedLength;
                int rightLength = mismatchException.Length;

                System.Console.ForegroundColor = ConsoleColor.Gray;

                System.Console.Write(line.AsSpan(start: 0, left));

                System.Console.ForegroundColor = ConsoleColor.Green;

                System.Console.Write(line.AsSpan(left, leftLength));

                System.Console.ForegroundColor = ConsoleColor.Gray;

                System.Console.Write(line.AsSpan(left + leftLength, right - left - leftLength));

                System.Console.ForegroundColor = ConsoleColor.Red;

                System.Console.Write(line.AsSpan(right, rightLength));

                System.Console.ForegroundColor = ConsoleColor.Gray;

                System.Console.WriteLine(line.AsSpan(right + rightLength));
                System.Console.WriteLine("\tSyntax error");
            }
            catch (MathEngineException mathEngineException)
            {
                int offset = mathEngineException.Offset;
                int length = mathEngineException.Length;

                System.Console.ForegroundColor = ConsoleColor.Gray;

                System.Console.Write(line.AsSpan(start: 0, offset));

                System.Console.ForegroundColor = ConsoleColor.Red;

                System.Console.Write(line.AsSpan(offset, length));

                System.Console.ForegroundColor = ConsoleColor.Gray;

                System.Console.WriteLine(line.AsSpan(offset + length));

                if (mathEngineException is ParsingException parsingException)
                {
                    System.Console.WriteLine("\tSyntax error: expected {0} but read {1}",
                        parsingException.ExpectedType,
                        parsingException.Type);
                }
                else
                {
                    System.Console.WriteLine("\tSyntax error");
                }
            }
            catch (DivideByZeroException divideByZeroException)
            {
                System.Console.WriteLine("\tError: {0}", divideByZeroException.Message);
            }
        }
    }

    private static IExpression Parse(string text)
    {
        if (text.StartsWith('='))
        {
            text = text.Substring(1);
        }

        CultureInfo culture = CultureInfo.CurrentCulture;
        List<Token> tokens = new Tokenizer(text, culture).ToList();
        Parser parser = new Parser(tokens, culture);
        IExpression expression = parser.Parse();

        System.Console.WriteLine("\nTokens: {0}\n", string.Join(' ', tokens));

        return expression;
    }
}
