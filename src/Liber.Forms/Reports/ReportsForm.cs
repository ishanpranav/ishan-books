// ReportsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.Lines;
using Liber.Forms.Reports.Gdi;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal partial class ReportsForm : Form
{
    private readonly ReportEngine _engine;
    private int _favoriteIndex = 0;

    private IReportView? _view;

    public ReportsForm(ReportEngine engine, ReportsFormStyle style)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        switch (style)
        {
            case ReportsFormStyle.Extended:
                _listView.Initialize(engine);
                break;

            case ReportsFormStyle.None:
                splitContainer1.Panel1Collapsed = true;
                break;

            case ReportsFormStyle.ConfigurationOnly:
                splitContainer2.Panel1Collapsed = true;
                break;
        }

        _engine = engine;

        if (engine.Find(ReportEngine.AccountMapReport, out IReportView? favoriteView, out IntervalView? favoriteReport))
        {
            View = favoriteView;

            RefreshFavorite(favoriteReport);
        }
    }

    private IReportView? View
    {
        get
        {
            return _view;
        }
        set
        {
            _view = value;

            if (value != null)
            {
                _propertyGrid.SelectedObject = value.Properties;
            }
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

            if (!_recentPathManager.Empty)
            {
                _webView.CoreWebView2.Profile.DefaultDownloadFolderPath = Path.GetDirectoryName(_recentPathManager.Paths.First());
            }

            _webView.CoreWebView2.Profile.PreferredColorScheme = CoreWebView2PreferredColorScheme.Light;
        }
        catch (COMException) { }

        GenerateReport();
        _timer.Start();
    }

    private void GenerateReport()
    {
        if (View != null && !_backgroundWorker.IsBusy)
        {
            _backgroundWorker.RunWorkerAsync();
        }
    }

    public void InitializeReport(IReportView view)
    {
        View = view;

        GenerateReport();
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out IReportView? view))
        {
            return;
        }

        _timer.Stop();

        View = view;

        if (view.Properties is GdiCheckReport checkReport && checkReport.Check.Value == null)
        {
            using CheckDialog checkForm = new CheckDialog(checkReport.Check);

            if (checkForm.ShowDialog() == DialogResult.OK)
            {
                checkReport.Check = checkForm.Value;
            }
        }

        GenerateReport();
    }

    private void OnPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
    {
        _timer.Stop();
        GenerateReport();
    }

    private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
    {
        if (_view == null)
        {
            e.Cancel = true;

            return;
        }

        _view.Generate();
    }

    private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled || View == null)
        {
            return;
        }

        if (_webView.CoreWebView2 != null)
        {
            View.Navigate(_webView.CoreWebView2);
        }
    }

    private void RefreshFavorite(IntervalView report)
    {
        report.Accounts = new AccountsView(
            _engine.Company,
            _engine.Company.Accounts
                .Where(x => _favoriteIndex == 0 ? x.Type.IsTemporary() : !x.Type.IsTemporary())
                .ToHashSet());
        _favoriteIndex = _favoriteIndex == 0 ? 1 : 0;
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        if (View == null || View.Properties is not IntervalView report)
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
            }

            return;
        }

        RefreshFavorite(report);
        GenerateReport();
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
