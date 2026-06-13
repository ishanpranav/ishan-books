// TransactionHelpers.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Media;
using Liber.Forms.Properties;

namespace Liber.Forms.Transactions;

internal static class TransactionHelpers
{
    public static void Post(DateTime posted)
    {
        SystemSounds.Asterisk.Play();

        Settings.Default.LastPosted = posted;

        Settings.Default.Save();
    }
}
