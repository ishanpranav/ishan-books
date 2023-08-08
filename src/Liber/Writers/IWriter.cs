// IWriter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Threading.Tasks;

namespace Liber.Writers;

public interface IWriter
{
    Task SerializeAsync(Stream output, Company company);
}
