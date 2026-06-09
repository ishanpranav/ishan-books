// MathEngine.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Liber.MathEngine;

public static class Engine
{
    public static decimal Evaluate(string text)
    {
        return Evaluate(text, CultureInfo.CurrentCulture);
    }

    public static decimal Evaluate(string text, CultureInfo culture)
    {
        if (text.StartsWith('='))
        {
            text = text.Substring(1);
        }

        List<Token> tokens = new Tokenizer(text, culture).ToList();

        Console.WriteLine("\nTokenizer: {0}\n",
            string.Join(", ", tokens));

        return 0;
    }
}
