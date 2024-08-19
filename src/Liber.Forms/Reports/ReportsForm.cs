// ReportsForm.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.Lines;
using Liber.Forms.Reports.Gdi;
using Liber.Forms.Reports.Xsl;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal sealed partial class ReportsForm : Form
{
    private IReportView? _view;

    public ReportsForm(ReportEngine engine)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        foreach (KeyValuePair<string, IReportView> view in engine.Views)
        {
            ListViewItem item = _listView.Items.Add(view.Key, view.Value.Title, imageIndex: 0);

            item.ImageIndex = _imageList.Images.Count - 1;
            item.Tag = view.Value;
        }
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        try
        {
            await _webView.EnsureCoreWebView2Async();

            _webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "liber.example",
                Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,
                CoreWebView2HostResourceAccessKind.DenyCors);
        }
        catch (COMException) { }

        InitializeReport();
    }

    private void InitializeReport()
    {
        if (_view != null && !_backgroundWorker.IsBusy)
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

                InitializeReport();

                break;
            }
        }
    }

    public void InitializeXslReport(string key)
    {
        _view = (IReportView)_listView.Items[key].Tag;

        XslReport report = (XslReport)_view.Properties;

        report.Accounts = new AccountsView(report.Accounts.Company);

        InitializeReport();
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        _view = (IReportView)_listView.SelectedItems[0].Tag;
        _propertyGrid.SelectedObject = _view.Properties;

        if (_view.Properties is GdiCheckReport checkReport && checkReport.Check.Value == null)
        {
            using CheckDialog checkForm = new CheckDialog(checkReport.Check);

            if (checkForm.ShowDialog() == DialogResult.OK)
            {
                checkReport.Check = checkForm.Value;
            }
        }

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

        try
        {
            _view.Navigate(_webView.CoreWebView2);
        }
        catch { }
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
