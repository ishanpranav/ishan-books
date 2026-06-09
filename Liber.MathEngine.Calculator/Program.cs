// Program.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Globalization;
using Liber.MathEngine.Exceptions;
using Liber.MathEngine.Expressions;

namespace Liber.MathEngine.Calculator;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            Console.WriteLine(Parse(args[0]).Evaluate());

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
                IExpression expression = Parse(line);
                decimal result = expression.Evaluate();

                Console.WriteLine("\t{0} = {1}", expression, result);
            }
            catch (MismatchException mismatchException)
            {
                int left = mismatchException.ExpectedOffset;
                int right = mismatchException.Offset;
                int leftLength = mismatchException.ExpectedLength;
                int rightLength = mismatchException.Length;

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(line.AsSpan(start: 0, left));

                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write(line.AsSpan(left, leftLength));

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(line.AsSpan(left + leftLength, right - left - leftLength));

                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(line.AsSpan(right, rightLength));

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine(line.AsSpan(right + rightLength));
                Console.WriteLine("\tSyntax error: {0}");
            }
            catch (MathEngineException mathEngineException)
            {
                int offset = mathEngineException.Offset;
                int length = mathEngineException.Length;

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(line.AsSpan(start: 0, offset));

                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(line.AsSpan(offset, length));

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine(line.AsSpan(offset + length));

                if (mathEngineException is ParsingException parsingException)
                {
                    Console.WriteLine("\tSyntax error: expected {0} but read {1}",
                        parsingException.ExpectedType,
                        parsingException.Type);
                }
                else
                {
                    Console.WriteLine("\tSyntax error");
                }
            }
            catch (DivideByZeroException divideByZeroException)
            {
                Console.WriteLine("\tError: {0}", divideByZeroException.Message);
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

        Console.WriteLine("\nTokens: {0}\n", string.Join(' ', tokens), expression);

        return expression;
    }
}
