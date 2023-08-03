// ReportsForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Liber.Forms.Lines;
using Liber.Forms.Reports.Gdi;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal sealed partial class ReportsForm : Form
{
    private readonly Company _company;

    private IReportView? _view;

    public ReportsForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _company = company;
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        XslReportView.InitializeReports(Path.Combine("data", Path.ChangeExtension("reports", "json")));

        InitializeReports(
            path: "styles",
            searchPattern: "*.xslt",
            x => new XslReportView(_company, x));

        IReportView createReportView(string path)
        {
            GdiCheckReport report = IniSerializer.DeserializeGdiCheckReport(_company, path);

            return new GdiReportView(report);
        }

        InitializeReports(
            path: "checks",
            searchPattern: "*.chk",
            createReportView);
        InitializeReports(
            path: "checks",
            searchPattern: "*.ini",
            createReportView);

        await _webView.EnsureCoreWebView2Async();

        _webView.CoreWebView2.SetVirtualHostNameToFolderMapping("liber.example", Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, CoreWebView2HostResourceAccessKind.DenyCors);
    }

    private void InitializeReports(string path, string searchPattern, Func<string, IReportView> reportViewFactory)
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
            ListViewItem item = _listView.Items.Add(reportView.Title);

            item.ImageIndex = _imageList.Images.Count - 1;
            item.Tag = reportView;
        }
    }

    private void InitializeReport()
    {
        if (!_backgroundWorker.IsBusy)
        {
            _backgroundWorker.RunWorkerAsync();
        }
    }

    public void InitializeCheck(CheckView value)
    {
        foreach (ListViewItem item in _listView.Items)
        {
            _view = (IReportView)item.Tag;

            if (_view.Properties is GdiCheckReport checkReport)
            {
                checkReport.Check = value;
            }
        }

        InitializeReport();
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        _view = (IReportView)_listView.SelectedItems[0].Tag;
        _propertyGrid.SelectedObject = _view.Properties;
        InitializeReport();
    }

    private void OnPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        InitializeReport();
    }

    private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
    {
        if (_view == null)
        {
            e.Cancel = true;

            return;
        }

        _view.InitializeReport();
    }

    private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled || _view == null)
        {
            return;
        }

        _view.Navigate(_webView.CoreWebView2);
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
}
