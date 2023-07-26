// ReportsForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Xsl;
using Liber.Forms.Properties;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms;

internal sealed partial class ReportsForm : Form
{
    private static readonly Dictionary<string, XslCompiledTransform> s_styles = new Dictionary<string, XslCompiledTransform>();

    private readonly Company _company;

    private string? _xhtml;
    private string? _file;

    public ReportsForm(Company company)
    {
        InitializeComponent();
        ClickOnce.Initialize(this);

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

        _webView.CoreWebView2.SetVirtualHostNameToFolderMapping("liber.example", Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, CoreWebView2HostResourceAccessKind.DenyCors);

        string[] files = Directory.GetFiles("styles", "*.xslt");

        if (files.Length == 0)
        {
            return;
        }

        _imageList.Images.Add(Icon.ExtractAssociatedIcon(files[0])!);

        foreach (string file in files)
        {
            string key = Path.GetFileNameWithoutExtension(file);
            ListViewItem item = _listView.Items.Add(XslExtensions.GetString(key, CultureInfo.CurrentUICulture));

            item.ImageIndex = 0;
            item.Tag = file;
        }

        startedDateTimePicker.Value = new DateTime(DateTime.Today.Year, 1, 1);
        postedDateTimePicker.Value = DateTime.Today;
    }

    private void InitializeReport()
    {
        if (_file == null)
        {
            return;
        }

        if (!s_styles.TryGetValue(_file, out XslCompiledTransform? style))
        {
            style = XmlReportSerializer.DeserializeTransform(_file);
            s_styles[_file] = style;
        }

        Report report = new Report(_company, postedDateTimePicker.Value)
        {
            Started = startedDateTimePicker.Value
        };

        _xhtml = XmlReportSerializer.Serialize(style, report, new XslExtensions(report, CultureInfo.CurrentUICulture));

        _webView.NavigateToString(_xhtml);
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        _file = (string)_listView.SelectedItems[0].Tag;
        saveAsToolStripButton.Enabled = true;
        saveAsToolStripMenuItem.Enabled = true;
        printPreviewToolStripButton.Enabled = true;
        printPreviewToolStripMenuItem.Enabled = true;
        printToolStripButton.Enabled = true;
        printToolStripMenuItem.Enabled = true;
        startedDateTimePicker.Enabled = true;
        postedDateTimePicker.Enabled = true;

        InitializeReport();
    }

    private void OnPrintPreviewToolStripButtonClick(object sender, EventArgs e)
    {
        _webView.CoreWebView2.ShowPrintUI();
    }

    private async void OnSaveAsToolStripButtonClick(object sender, EventArgs e)
    {
        if (_saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        string extension = Path.GetExtension(_saveFileDialog.FileName);

        switch (extension.ToUpperInvariant())
        {
            case ".PDF":
                await _webView.CoreWebView2.PrintToPdfAsync(_saveFileDialog.FileName);
                break;

            case ".HTM":
            case ".HTML":
                await File.WriteAllTextAsync(_saveFileDialog.FileName, _xhtml);
                break;

            default:
                MessageBox.Show(Resources.ExceptionCaption, FormattedStrings.GetNotSupportedText(extension), MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
        }
    }

    private void OnStartedDateTimePickerValueChanged(object sender, EventArgs e)
    {
        postedDateTimePicker.MinDate = startedDateTimePicker.Value;

        InitializeReport();
    }

    private void OnPostedDateTimePickerValueChanged(object sender, EventArgs e)
    {
        startedDateTimePicker.MaxDate = postedDateTimePicker.Value;

        InitializeReport();
    }
}
