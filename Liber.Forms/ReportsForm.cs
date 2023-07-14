using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace Liber.Forms;

internal sealed partial class ReportsForm : Form
{
    private static readonly Dictionary<string, XslCompiledTransform> s_transforms = new Dictionary<string, XslCompiledTransform>();

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

        string[] files = Directory.GetFiles("transforms", "*.xslt");

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

        if (!s_transforms.TryGetValue(file, out XslCompiledTransform? transform))
        {
            transform = XmlReportSerializer.DeserializeTransform(file);
            s_transforms[file] = transform;
        }

        string xhtml = XmlReportSerializer.Serialize(transform, _company);

        _webView.NavigateToString(xhtml);
    }
}
