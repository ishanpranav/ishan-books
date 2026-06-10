// ExpressionConverter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Globalization;
using Liber.MathEngine;
using Liber.MathEngine.Exceptions;
using Liber.MathEngine.Expressions;

namespace System.ComponentModel;

public class ExpressionConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string text)
        {
            return base.ConvertFrom(context, culture, value);
        }

        if (culture == null)
        {
            culture = CultureInfo.CurrentCulture;
        }

        Tokenizer tokenizer = new Tokenizer(text, culture);
        Parser parser = new Parser(tokenizer, culture);

        try
        {
            return parser.Parse();
        }
        catch (MathEngineException)
        {
            return new DecimalExpression(0);
        }
        catch (DivideByZeroException)
        {
            return new DecimalExpression(0);
        }
    }
}
