// Parser.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Globalization;
using Liber.MathEngine.Exceptions;
using Liber.MathEngine.Expressions;

namespace Liber.MathEngine;

public class Parser
{
    private readonly IEnumerator<Token> _enumerator;

    public CultureInfo Culture { get; }

    public Parser(IEnumerable<Token> tokens, CultureInfo culture)
    {
        _enumerator = tokens.GetEnumerator();
        Culture = culture;

        _enumerator.MoveNext();
    }

    public IExpression Parse()
    {
        IExpression result = ParseExpression(Precedence.None);

        Expect(TokenType.End);

        return result;
    }

    private IExpression ParseExpression(Precedence parentPrecedence)
    {
        IExpression left = ParsePrefix();

        while (true)
        {
            Precedence precedence = _enumerator.Current.Type.ToPrecedence();

            if (precedence <= parentPrecedence)
            {
                break;
            }

            Token operatorToken = _enumerator.Current;

            _enumerator.MoveNext();

            IExpression right = ParseExpression(precedence);
            IExpression? operatorExpression = CreateExpression(left, operatorToken.Type, right);

            if (operatorExpression == null)
            {
                throw new MathEngineException(_enumerator.Current.Offset, _enumerator.Current.Value.Length);
            }

            left = operatorExpression;
        }

        return left;
    }

    private IExpression ParsePrefix()
    {
        if (Accept(TokenType.Minus))
        {
            return new NegationExpression(ParseExpression(Precedence.Negation));
        }

        if (Accept(TokenType.Left))
        {
            IExpression expression = ParseExpression(Precedence.None);

            Expect(TokenType.Right);

            return expression;
        }

        if (_enumerator.Current.Type != TokenType.Number)
        {
            throw new ParsingException(
                _enumerator.Current.Type,
                TokenType.Number,
                _enumerator.Current.Offset,
                _enumerator.Current.Value.Length);
        }

        if (!decimal.TryParse(
            _enumerator.Current.Value,
            NumberStyles.Number,
            Culture,
            out decimal value))
        {
            throw new MathEngineException(_enumerator.Current.Offset, _enumerator.Current.Value.Length);
        }

        _enumerator.MoveNext();

        return new DecimalExpression(value);
    }

    private IExpression? CreateExpression(IExpression left, TokenType type, IExpression right)
    {
        switch (type)
        {
            case TokenType.Plus: return new AdditionExpression(left, right);
            case TokenType.Minus: return new SubtractionExpression(left, right);
            case TokenType.Star: return new MultiplicationExpression(left, right);
            case TokenType.Slash: return new DivisionExpression(left, right);
        }

        return null;
    }

    private bool Accept(TokenType type)
    {
        if (_enumerator.Current.Type != type)
        {
            return false;
        }

        _enumerator.MoveNext();

        return true;
    }

    private void Expect(TokenType type)
    {
        if (!Accept(type))
        {
            throw new ParsingException(
                _enumerator.Current.Type,
                type,
                _enumerator.Current.Offset,
                _enumerator.Current.Value.Length);
        }
    }
}
