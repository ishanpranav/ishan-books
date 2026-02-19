// IAccountView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.Accounts;

internal interface IAccountView : IEquatable<Guid>
{
    Guid Id { get; }
    string DisplayName { get; }
}
