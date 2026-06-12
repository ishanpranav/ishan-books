// ImportRulesForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Forms;
using Liber.Forms.Properties;

namespace Liber.Forms;

internal sealed partial class ImportRulesForm : Form
{
    private readonly BindingList<ImportRule> _rules = new BindingList<ImportRule>();

    public ImportRulesForm()
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        InitializeSettings();

        DialogResult = DialogResult.Cancel;
    }

    private void InitializeSettings()
    {
        _rules.Clear();

        ImportRule[]? rules = JsonSerializer.Deserialize<ImportRule[]>(Settings.Default.ImportRules, FormattedStrings.JsonOptions);

        if (rules != null)
        {
            foreach (ImportRule rule in rules)
            {
                _rules.Add(rule);
            }
        }

        _textBox.Text = JsonSerializer.Serialize(_rules, FormattedStrings.JsonOptions);
        importRulesDataGridView.DataSource = _rules;

        importRulesDataGridView.AutoResizeColumns();
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Settings.Default.ImportRules = JsonSerializer.Serialize(_rules, FormattedStrings.JsonOptions);

        Settings.Default.Save();

        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private void OnResetButtonClick(object sender, EventArgs e)
    {
        Settings.Default.Reset();
        InitializeSettings();
    }

    private void OnTabControlSelectedIndexChanged(object sender, EventArgs e)
    {
        if (_tabControl.SelectedTab == jsonTabPage)
        {
            _textBox.Text = JsonSerializer.Serialize(_rules, FormattedStrings.JsonOptions);

            return;
        }

        ImportRule[]? rules;

        try
        {
            rules = JsonSerializer.Deserialize<ImportRule[]>(_textBox.Text, FormattedStrings.JsonOptions);
        }
        catch (JsonException)
        {
            return;
        }

        if (rules == null)
        {
            return;
        }

        _errorProvider.SetError(_tabControl, null);
        _rules.Clear();

        foreach (ImportRule rule in rules)
        {
            _rules.Add(rule);
        }
    }

    private void OnTabControlSelecting(object sender, TabControlCancelEventArgs e)
    {
        if (_tabControl.SelectedTab == jsonTabPage)
        {
            return;
        }

        if (!string.IsNullOrEmpty(_errorProvider.GetError(editorTabPage)))
        {
            Settings.Default.Reset();
            InitializeSettings();

            return;
        }

        try
        {
            ImportRule[]? rules = JsonSerializer.Deserialize<ImportRule[]>(_textBox.Text, FormattedStrings.JsonOptions);

            if (rules == null)
            {
                _errorProvider.SetError(editorTabPage, "Bad JSON");

                e.Cancel = true;
            }
            else
            {
                _errorProvider.SetError(editorTabPage, null);
            }
        }
        catch (JsonException)
        {
            _errorProvider.SetError(editorTabPage, "Bad JSON");

            e.Cancel = true;
        }
    }
}
