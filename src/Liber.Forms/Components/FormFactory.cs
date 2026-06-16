// FormFactory.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Liber.Forms.Components;

internal sealed class FormFactory : Component
{
    private readonly Dictionary<Guid, Form> _forms = new Dictionary<Guid, Form>();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Form? Parent { get; private set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Form? Embedded { get; private set; }

    public IReadOnlyDictionary<Guid, Form> Forms
    {
        get
        {
            return _forms;
        }
    }

    public event EventHandler? Invalidated;

    public FormFactory() { }

    public FormFactory(IContainer container)
    {
        container.Add(this);
    }

    public void Register(Guid key, Form value)
    {
        _forms.Add(key, value);

        value.FormClosed += (_, _) =>
        {
            _forms.Remove(key);
            Invalidated?.Invoke(sender: this, EventArgs.Empty);
        };

        value.Show();
        Invalidated?.Invoke(sender: this, EventArgs.Empty);
    }

    public void RegisterEmbedded(Guid key, Form parent, Form value)
    {
        if (Parent != null)
        {
            Parent.Resize -= OnParentResize;
        }

        if (Embedded != null)
        {
            Embedded.Close();
        }

        Embedded = value;
        Parent = parent;

        _forms.Add(key, value);

        value.FormClosed += (_, _) =>
        {
            _forms.Remove(key);
            Invalidated?.Invoke(sender: this, EventArgs.Empty);

            Parent = null;
            Embedded = null;
        };
        value.FormBorderStyle = FormBorderStyle.None;
        value.ControlBox = false;
        value.MdiParent = parent;
        value.Text = string.Empty;
        value.WindowState = FormWindowState.Minimized;
        parent.Resize += OnParentResize;

        value.Show();
        parent.BeginInvoke(() =>
        {
            value.WindowState = FormWindowState.Maximized;
        });
        Invalidated?.Invoke(sender: this, EventArgs.Empty);
    }

    private void OnParentResize(object? sender, EventArgs e)
    {
        if (Embedded != null)
        {
            Embedded.WindowState = FormWindowState.Maximized;
        }
    }

    public void Kill(Guid key)
    {
        if (_forms.TryGetValue(key, out Form? form))
        {
            form.Close();
        }
    }

    public void KillAll()
    {
        foreach (Form form in _forms.Values.ToList())
        {
            form.Close();
        }
    }

    public bool TryActivate(Guid key)
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

        if (TryActivate(key))
        {
            return;
        }

        Register(key, factory());
    }
}
