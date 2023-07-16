using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace Liber.Forms;

internal sealed partial class ReportsForm : Form
{
    private static readonly Dictionary<string, XslCompiledTransform> s_styles = new Dictionary<string, XslCompiledTransform>();

    private readonly Company _company;

    private string? _xhtml;

    public ReportsForm(Company company)
    {
        InitializeComponent();

        _company = company;
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        await _webView.EnsureCoreWebView2Async();

        _webView.CoreWebView2.DocumentTitleChanged += (_, _) => Text = _webView.CoreWebView2.DocumentTitle;
        _webView.CoreWebView2.ContextMenuRequested += (_, e) =>
        {
            e.MenuItems.Clear();
            _contextMenu.Show(_webView, e.Location);
        };
        
        _webView.CoreWebView2.SetVirtualHostNameToFolderMapping("sharp-books.example", Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, CoreWebView2HostResourceAccessKind.DenyCors);

        string[] files = Directory.GetFiles("styles", "*.xslt");

        if (files.Length == 0)
        {
            return;
        }

        _imageList.Images.Add(Icon.ExtractAssociatedIcon(files[0])!);

        foreach (string file in files)
        {
            IReadOnlyDictionary<string, string> variables = XmlReportSerializer.DeserializeStylesheet(file).ToDictionary();

            if (!variables.TryGetValue("title", out string? title))
            {
                continue;
            }

            ListViewItem item = _listView.Items.Add(title);

            item.ImageIndex = 0;
            item.Tag = file;
        }
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        string file = (string)_listView.SelectedItems[0].Tag;

        if (!s_styles.TryGetValue(file, out XslCompiledTransform? style))
        {
            style = XmlReportSerializer.DeserializeTransform(file);
            s_styles[file] = style;
        }

        _xhtml = XmlReportSerializer.Serialize(style, _company);

        _webView.NavigateToString(_xhtml);

        saveAsToolStripButton.Enabled = true;
        saveAsToolStripMenuItem.Enabled = true;
        printPreviewToolStripButton.Enabled = true;
        printPreviewToolStripMenuItem.Enabled = true;
        printToolStripButton.Enabled = true;
        printToolStripMenuItem.Enabled = true;
    }

    private void OnPrintPreviewToolStripButtonClick(object sender, EventArgs e)
    {
        _webView.CoreWebView2.ShowPrintUI();
    }

    private void OnHelpToolStripButtonClick(object sender, EventArgs e)
    {

    }

    private async void OnSaveAsToolStripButtonClick(object sender, EventArgs e)
    {
        if (_saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        switch (Path.GetExtension(_saveFileDialog.FileName).ToUpperInvariant())
        {
            case ".PDF":
                await _webView.CoreWebView2.PrintToPdfAsync(_saveFileDialog.FileName);
                break;

            case ".HTM":
            case ".HTML":
                await File.WriteAllTextAsync(_saveFileDialog.FileName, _xhtml);
                break;
        }
    }
}
