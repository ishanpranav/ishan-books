// AlphaVantageException.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.AlphaVantage;

[Serializable]
public class AlphaVantageException : Exception
{
    public AlphaVantageException() { }
    public AlphaVantageException(string? message) : base(message) { }
    public AlphaVantageException(string? message, Exception? innerException) : base(message, innerException) { }
}
