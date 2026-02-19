// IWriter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Threading.Tasks;

namespace Liber.Writers;

/// <summary>
/// Defines a method for writing company data.
/// </summary>
public interface IWriter
{
    /// <summary>
    /// Asynchronously writes company data to a stream.
    /// </summary>
    /// <param name="output">The output stream.</param>
    /// <param name="company">The company whose data to write.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    Task WriteAsync(Stream output, Company company);
}
