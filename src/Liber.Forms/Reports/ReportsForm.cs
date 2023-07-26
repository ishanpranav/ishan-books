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
            Report.GetString,
            x => new XslReportView(x));

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

        startedDateTimePicker.Value = new DateTime(DateTime.Today.Year, 1, 1);
        postedDateTimePicker.Value = DateTime.Today;
    }

    private void InitializeReports(string path, string searchPattern, Func<string, string> stringAccessor, Func<string, IReportView> reportViewFactory)
    {
        string[] files = Directory.GetFiles(path, searchPattern);

        if (files.Length == 0)
        {
            return;
        }

        Icon? icon = Icon.ExtractAssociatedIcon(files[0]);

        if (icon != null)
        {
            _imageList.Images.Add(icon);
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
                throw; // continue;
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

        _view.InitializeReport(_webView.CoreWebView2, new Report(_company, postedDateTimePicker.Value)
        {
            Started = startedDateTimePicker.Value
        });
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        _view = (IReportView)_listView.SelectedItems[0].Tag;
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
        if (_view == null || _saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        await _view.PrintAsync(_saveFileDialog.FileName);
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

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (ListViewItem item in _listView.Items)
            {
                if (item.Tag is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            components?.Dispose();
        }

        base.Dispose(disposing);
    }
}
