// FormFactory.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liber.Forms.Components;

internal sealed class FormFactory : Component
{
    private readonly Dictionary<Guid, Form> _forms = new Dictionary<Guid, Form>();

    public FormFactory() { }

    public FormFactory(IContainer container)
    {
        container.Add(this);
    }

    [Browsable(false)]
    public Form? Parent { get; set; }

    public void Register(Guid key, Form value)
    {
        _forms.Add(key, value);

        value.MdiParent = Parent;
        value.FormClosed += (_, _) => _forms.Remove(key);

        value.Show();
    }

    public void Kill(Guid key)
    {
        if (_forms.TryGetValue(key, out Form? form))
        {
            form.Close();
        }
    }

    public bool TryKill(Guid key)
    {
        if (!_forms.TryGetValue(key, out Form? form))
        {
            return false;
        }

        form.Activate();

        return true;
    }

    public void AutoRegister<TForm>(Func<TForm> factory) where TForm : Form
    {
        Guid key = typeof(TForm).GUID;

        if (TryKill(key))
        {
            return;
        }

        Register(key, factory());
    }
}
