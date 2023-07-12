using System;
using System.Windows.Forms;

namespace Liber.Forms;

internal sealed partial class ErrorsForm : Form
{
    private readonly MainContext _context;

    public ErrorsForm(MainContext context)
    {
        InitializeComponent();

        _context = context;
    }

    public void InitializeErrors()
    {
        foreach (Error error in _context.Errors)
        {
            ListViewItem item = _listView.Items.Add(error.Created.ToString());

            item.Tag = error;

            item.SubItems.AddRange(new string[]
            {
                error.Description,
                error.RawString ?? string.Empty
            });
        }

        _listView.AutoResizeColumns();

        _toolStripStatusLabel.Text = FormattedStrings.ErrorCount(_context.Errors.Count);
        ignoreAllButton.Enabled = _context.Errors.Count > 0;
    }

    private void OnListViewItemChecked(object sender, ItemCheckedEventArgs e)
    {
        ignoreButton.Enabled = _listView.CheckedItems.Count > 0;
    }

    private void OnRefreshButtonClick(object sender, EventArgs e)
    {
        InitializeErrors();
    }

    private void OnIgnoreButtonClick(object sender, EventArgs e)
    {
        ListViewItem[] items = new ListViewItem[_listView.CheckedItems.Count];

        _listView.CheckedItems.CopyTo(items, index: 0);

        foreach (ListViewItem item in items)
        {
            _ = _context.Errors.Remove((Error)item.Tag);
            _listView.Items.Remove(item);
        }
    }

    private void OnIgnoreAllButtonClick(object sender, EventArgs e)
    {
        _context.Errors.Clear();
        _listView.Items.Clear();
    }

    private void OnCloseClick(object sender, EventArgs e)
    {
        Close();
    }
}
