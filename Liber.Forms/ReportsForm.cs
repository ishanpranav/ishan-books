using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace Liber.Forms;

internal sealed partial class ReportsForm : Form
{
    private static readonly Dictionary<string, XslCompiledTransform> s_styles = new Dictionary<string, XslCompiledTransform>();

    private readonly Company _company;

    public ReportsForm(Company company)
    {
        InitializeComponent();

        _company = company;
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        await _webView.EnsureCoreWebView2Async();

        _webView.CoreWebView2.DocumentTitleChanged += (_, _) => Text = _webView.CoreWebView2.DocumentTitle;

        string[] files = Directory.GetFiles("styles", "*.xslt");

        if (files.Length == 0)
        {
            return;
        }

        _imageList.Images.Add(Icon.ExtractAssociatedIcon(files[0])!);

        foreach (string file in files)
        {
            ListViewItem item = _listView.Items.Add(Path.GetFileNameWithoutExtension(file));

            item.ImageIndex = 0;
            item.Tag = file;
        }
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        string file = (string)_listView.SelectedItems[0].Tag;

        if (!s_styles.TryGetValue(file, out XslCompiledTransform? style))
        {
            style = new XslCompiledTransform();

            style.Load(file);

            s_styles[file] = style;
        }

        string xhtml = XmlReportSerializer.Serialize(style, _company);

        _webView.NavigateToString(xhtml);
    }
}
