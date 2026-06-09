// Tokenizer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Liber.MathEngine.Exceptions;

namespace Liber.MathEngine;

public class Tokenizer : IEnumerable<Token>
{
    private int _offset;
    private readonly Stack<(char Current, int Offset)> _brackets = new Stack<(char, int)>();

    public string Text { get; }
    public CultureInfo Culture { get; }

    public Tokenizer(string text, CultureInfo culture)
    {
        Text = text;
        Culture = culture;
    }

    public IEnumerator<Token> GetEnumerator()
    {
        _offset = 0;

        while (true)
        {
            SkipWhiteSpace();

            if (_offset >= Text.Length)
            {
                if (_brackets.TryPeek(out (char Current, int Offset) result))
                {
                    throw new MathEngineException(result.Offset, length: 1);
                }

                yield return new Token(TokenType.End, value: string.Empty, _offset);
                yield break;
            }

            char current = Text[_offset];

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
        while (_offset < Text.Length && char.IsWhiteSpace(Text[_offset]))
        {
            _offset++;
        }
    }

    private Token TokenizeNumber()
    {
        int start = _offset;
        string decimalSeparator = Culture.NumberFormat.NumberDecimalSeparator;
        string groupSeparator = Culture.NumberFormat.NumberGroupSeparator;
        bool hasDecimalSeparator = false;

        while (_offset < Text.Length)
        {
            if (!hasDecimalSeparator && IsMatch(decimalSeparator))
            {
                _offset += decimalSeparator.Length;
                hasDecimalSeparator = true;

                continue;
            }

            if (!hasDecimalSeparator && IsMatch(groupSeparator))
            {
                _offset += groupSeparator.Length;

                continue;
            }

            if (!char.IsDigit(Text[_offset]))
            {
                break;
            }

            _offset++;
        }

        return new Token(TokenType.Number, Text.Substring(start, _offset - start), start);
    }

    private bool IsMatch(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        if (_offset + value.Length > Text.Length)
        {
            return false;
        }

        return Text.AsSpan(_offset, value.Length).SequenceEqual(value.AsSpan());
    }

    private Token TokenizeOperator()
    {
        char current = Text[_offset];

        switch (current)
        {
            case '+': return new Token(TokenType.Plus, "+", _offset++);
            case '-': return new Token(TokenType.Minus, "-", _offset++);
            case '*': return new Token(TokenType.Star, "*", _offset++);
            case '/': return new Token(TokenType.Slash, "/", _offset++);

            case '(':
            case '[':
            case '{':
                _brackets.Push((current, _offset));

                return new Token(TokenType.Left, current.ToString(), _offset++);

            case ')':
            case ']':
            case '}':
                if (_brackets.Count == 0)
                {
                    throw new MathEngineException(_offset, length: 1);
                }

                (char expected, int expectedOffset) = _brackets.Pop();

                if (expected == '(' && current != ')' ||
                    expected == '[' && current != ']' ||
                    expected == '{' && current != '}')
                {
                    throw new MismatchException(
                        _offset, length: 1,
                        expectedOffset, expectedLength: 1);
                }

                return new Token(TokenType.Right, current.ToString(), _offset++);
        }

        throw new MathEngineException(_offset, length: 1);
    }
}
