// ReportsForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Liber.Skia;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal sealed partial class ReportsForm : Form
{
    private readonly Company _company;

    private IReportView? _view;

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
        InitializeReports(
            path: "styles",
            searchPattern: "*.xslt",
            XslReport.GetString,
            x => new XslReportView(_company, x));

        string getString(string value)
        {
            return value;
        }

        IReportView createReportView(string path)
        {
            return new SkiaReportView(new DrawableCheck(path));
        }

        InitializeReports(
            path: "checks",
            searchPattern: "*.chk",
            getString,
            createReportView);
        InitializeReports(
            path: "checks",
            searchPattern: "*.ini",
            getString,
            createReportView);
    }

    private void InitializeReports(string path, string searchPattern, Func<string, string> stringAccessor, Func<string, IReportView> reportViewFactory)
    {
        string[] files = Directory.GetFiles(path, searchPattern);

        if (files.Length == 0)
        {
            return;
        }

        using Icon? icon = Icon.ExtractAssociatedIcon(files[0]);

        if (icon != null)
        {
            Bitmap bitmap = icon.ToBitmap();

            _imageList.Images.Add(bitmap);

            icon.Dispose();
        }

        foreach (string file in files)
        {
            IReportView reportView;

            try
            {
                reportView = reportViewFactory(file);
            }
            catch
            {
                continue;
            }

            string key = Path.GetFileNameWithoutExtension(file);
            ListViewItem item = _listView.Items.Add(stringAccessor(key));

            item.ImageIndex = _imageList.Images.Count - 1;
            item.Tag = reportView;
        }
    }

    private void InitializeReport()
    {
        if (_view == null)
        {
            return;
        }

        _view.InitializeReport(_webView.CoreWebView2);
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        _view = (IReportView)_listView.SelectedItems[0].Tag;
        _propertyGrid.SelectedObject = _view.Properties;
        saveAsToolStripButton.Enabled = true;
        saveAsToolStripMenuItem.Enabled = true;
        printPreviewToolStripButton.Enabled = true;
        printPreviewToolStripMenuItem.Enabled = true;
        printToolStripButton.Enabled = true;
        printToolStripMenuItem.Enabled = true;

        InitializeReport();
    }

    private void OnPrintPreviewToolStripButtonClick(object sender, EventArgs e)
    {
        _webView.CoreWebView2.ShowPrintUI();
    }

    private async void OnSaveAsToolStripButtonClick(object sender, EventArgs e)
    {
        if (_view == null || _saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        await _view.PrintAsync(_saveFileDialog.FileName);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (ListViewItem item in _listView.Items)
            {
                if (item.Tag is IDisposable disposable)
                {
                    disposable.Dispose();
                    item.Tag = null;
                }
            }

            if (components != null)
            {
                components.Dispose();
                components = null;
            }
        }

        base.Dispose(disposing);
    }

    private void OnPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        InitializeReport();
    }
}
