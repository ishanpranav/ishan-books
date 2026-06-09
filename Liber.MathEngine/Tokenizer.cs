// Tokenizer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Liber.MathEngine;

internal sealed class Tokenizer : IEnumerable<Token>
{
    private int _position;
    private Stack<(char Current, int Position)> _brackets = new Stack<(char, int)>();

    public string Text { get; }
    public CultureInfo Culture { get; }

    public Tokenizer(string text, CultureInfo culture)
    {
        Text = text;
        Culture = culture;
    }

    public IEnumerator<Token> GetEnumerator()
    {
        _position = 0;

        while (true)
        {
            SkipWhiteSpace();

            if (_position >= Text.Length)
            {
                yield return new Token(TokenType.End, value: string.Empty, _position);
                yield break;
            }

            char current = Text[_position];

            if (char.IsDigit(current))
            {
                yield return TokenizeNumber();
                continue;
            }

            yield return TokenizeOperator();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void SkipWhiteSpace()
    {
        while (_position < Text.Length && char.IsWhiteSpace(Text[_position]))
        {
            _position++;
        }
    }

    private Token TokenizeNumber()
    {
        int start = _position;
        string decimalSeparator = Culture.NumberFormat.NumberDecimalSeparator;
        string groupSeparator = Culture.NumberFormat.NumberGroupSeparator;
        bool hasDecimalSeparator = false;

        while (_position < Text.Length)
        {
            if (!hasDecimalSeparator && IsMatch(decimalSeparator))
            {
                _position += decimalSeparator.Length;
                hasDecimalSeparator = true;

                continue;
            }

            if (!hasDecimalSeparator && IsMatch(groupSeparator))
            {
                _position += groupSeparator.Length;

                continue;
            }

            if (!char.IsDigit(Text[_position]))
            {
                break;
            }

            _position++;
        }

        return new Token(TokenType.Number, Text.Substring(start, _position - start), start);
    }

    private bool IsMatch(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        if (_position + value.Length > Text.Length)
        {
            return false;
        }

        return Text.AsSpan(_position, value.Length).SequenceEqual(value.AsSpan());
    }

    private Token TokenizeOperator()
    {
        char current = Text[_position];

        switch (current)
        {
            case '+': return new Token(TokenType.Plus, "+", _position++);
            case '-': return new Token(TokenType.Minus, "-", _position++);
            case '*': return new Token(TokenType.Multiply, "*", _position++);
            case '/': return new Token(TokenType.Divide, "/", _position++);

            case '(':
            case '[':
            case '{':
                _brackets.Push((current, _position));

                return new Token(TokenType.Left, current.ToString(), _position++);

            case ')':
            case ']':
            case '}':
                if (_brackets.Count == 0)
                {
                    throw new TokenizationException(current, _position);
                }

                (char expected, int expectedPosition) = _brackets.Pop();

                if (expected == '(' && current != ')' ||
                    expected == '[' && current != ']' ||
                    expected == '{' && current != '}')
                {
                    throw new MismatchTokenizationException(current, _position, expected, expectedPosition);
                }

                return new Token(TokenType.Right, current.ToString(), _position++);
        }

        throw new TokenizationException(current, _position);
    }
}
