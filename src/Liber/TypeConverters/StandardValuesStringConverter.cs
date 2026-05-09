// StandardValuesStringConverter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber;

namespace System.ComponentModel;

public class StandardValuesStringConverter : StringConverter
{
    /// <inheritdoc/>
    public override bool GetStandardValuesSupported(ITypeDescriptorContext? context)
    {
        return true;
    }

    /// <inheritdoc/>
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context)
    {
        return false;
    }

    /// <inheritdoc/>
    public override StandardValuesCollection? GetStandardValues(ITypeDescriptorContext? context)
    {
        if (context?.Instance is not IStandardValuesProvider instance)
        {
            return new StandardValuesCollection(null);
        }

        return new StandardValuesCollection(instance.GetStandardValues());
    }
}
