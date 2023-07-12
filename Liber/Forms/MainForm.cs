using Liber.Commands;
using Liber.Commands.Companies;
using Liber.Properties;
using Liber.Readers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liber.Forms;

internal sealed partial class MainForm : Form
{
    private readonly MainContext _context;

    public MainForm(Settings settings)
    {
        InitializeComponent();

        _context = new MainContext(form: this, settings);

        int filterIndex = 3;
        StringBuilder builder = new StringBuilder(Resources.FilterPrefix)
            .Append(" (")
            .AppendJoin("; ", settings.Readers.Keys.Select(x => '*' + x))
            .Append(")|")
            .AppendJoin(';', settings.Readers.Keys.Select(x => '*' + x));

        foreach (KeyValuePair<string, Reader> reader in settings.Readers)
        {
            reader.Value.FilterIndex = filterIndex;

            _ = builder
                .Append('|')
                .Append(reader.Value.DisplayName)
                .Append("|*")
                .Append(reader.Key);

            filterIndex++;
        }

        _openFileDialog.Filter = builder.ToString();
    }

    public void UpdateCompany(Company company)
    {
        foreach (Form child in MdiChildren)
        {
            child.Close();
        }

        UpdateCompanyName(company);
    }

    public void UpdateCompanyName(Company company)
    {
        Text = $"{Application.ProductName} - {company.DisplayName}";
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        InitializeCustomMenu();

        if (_context.Settings.MostRecentPath != null)
        {
            await ImportCommand.ShellExecuteAsync(_context, _context.Settings.MostRecentPath);
        }
    }

    private async void OnFormClosing(object sender, FormClosingEventArgs e)
    {
        if (await TryCancelAsync())
        {
            e.Cancel = true;

            return;
        }

        await ObjectLoader.SaveSettingsAsync(_context.Settings);
    }

    private void OnDragOver(object sender, DragEventArgs e)
    {
        if (e.Data is not null && e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effect = DragDropEffects.Copy;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private async void OnDragDrop(object sender, DragEventArgs e)
    {
        if (e.Data == null || e.Data.GetData(DataFormats.FileDrop) is not string[] paths || paths.Length == 0)
        {
            return;
        }

        if (await _context.Form.TryCancelAsync())
        {
            return;
        }

        await ImportCommand.ShellExecuteAsync(_context, paths[0]);
    }

    private void InitializeCustomMenu()
    {
        foreach (KeyValuePair<string, IList<CustomMenuItem>> group in _context.Settings.Groups)
        {
            ToolStripMenuItem parent = new ToolStripMenuItem(FormattedStrings.GetString(group.Key + "Name"));

            foreach (CustomMenuItem item in group.Value)
            {
                IReadOnlyCollection<Command>? subCommands = null;

                if (item.Command is ICommandParent commandParent)
                {
                    subCommands = commandParent.GetChildren(_context);

                    if (subCommands.Count == 0)
                    {
                        continue;
                    }
                }

                ToolStripMenuItem menuItem = new ToolStripMenuItem(item.Command.DisplayName)
                {
                    ShortcutKeys = item.ShortcutKeys,
                    ShowShortcutKeys = true
                };

                InitializeCustomMenuItem(item, menuItem);

                if (subCommands != null)
                {
                    foreach (Command child in subCommands)
                    {
                        ToolStripMenuItem dropDownItem = new ToolStripMenuItem(child.DisplayName);

                        InitializeCommand(child, dropDownItem);

                        _ = menuItem.DropDownItems.Add(dropDownItem);
                    }
                }

                _ = parent.DropDownItems.Add(menuItem);

                if (item.HasSeparator)
                {
                    _ = parent.DropDownItems.Add(new ToolStripSeparator());
                }
            }

            _ = _menuStrip.Items.Add(parent);
        }

        foreach (CustomMenuItem shortcut in _context.Settings.Shortcuts)
        {
            ToolStripItem child = new ToolStripButton(shortcut.Command.DisplayName)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };

            InitializeCustomMenuItem(shortcut, child);

            _ = _toolStrip.Items.Add(child);

            if (shortcut.HasSeparator)
            {
                _ = _toolStrip.Items.Add(new ToolStripSeparator());
            }
        }
    }

    private void InitializeCustomMenuItem(CustomMenuItem value, ToolStripItem control)
    {
        control.Image = value.Command.Image;

        InitializeCommand(value.Command, control);
    }

    private void InitializeCommand(Command value, ToolStripItem control)
    {
        control.Click += async (_, _) => await value.ExecuteAsync(_context);
    }

    public async Task<bool> TryCancelAsync()
    {
        switch (MessageBox.Show(
            FormattedStrings.CancelText(_context.Company),
            Resources.CancelCaption,
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Warning))
        {
            case DialogResult.No:
                return false;

            case DialogResult.Cancel:
                return true;
        }

        await SaveCompanyCommand.ShellExecuteAsync(_context);

        return false;
    }

    public bool TryGetSavePath([MaybeNullWhen(false)] out string result)
    {
        if (_saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            result = null;

            return false;
        }

        result = _saveFileDialog.FileName;

        return true;
    }

    public bool TryGetOpenPath(
        string defaultExtension,
        int filterIndex,
        [MaybeNullWhen(false)] out string result)
    {
        _openFileDialog.DefaultExt = defaultExtension;
        _openFileDialog.FilterIndex = filterIndex;

        if (_openFileDialog.ShowDialog() != DialogResult.OK)
        {
            result = null;

            return false;
        }

        result = _openFileDialog.FileName;

        return true;
    }
}
