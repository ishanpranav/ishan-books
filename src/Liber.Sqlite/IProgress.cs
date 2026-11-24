// IProgress.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Sqlite;

public interface IProgress
{
    void WriteAccount();
    void WriteTransaction();
    void WriteSecurity();
}
