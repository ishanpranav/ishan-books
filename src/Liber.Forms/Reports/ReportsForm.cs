// ReportsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

    public ReportsForm(ReportEngine engine)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);
        Dictionary<string, int> imageIndices = new Dictionary<string, int>();

        foreach (KeyValuePair<string, IReportView> prototype in engine.Prototypes
            .OrderBy(x => x.Value.SortOrder)
            .ThenBy(x => x.Value.GenericTitle))
        {
            ListViewItem item = _listView.Items.Add(prototype.Key, prototype.Value.GenericTitle, imageIndex: 0);
            string imageKey = prototype.Value.ImageKey;

            if (!imageIndices.TryGetValue(imageKey, out int index))
            {
                index = _imageList.Images.Count;
                imageIndices[imageKey] = index;

                _imageList.Images.Add((Bitmap)VisualStudioImageLibrary.ResourceManager.GetObject(imageKey)!);
            }

            item.ImageIndex = index;

            IReportView view = prototype.Value.Clone();

            item.Tag = view;

            if (view.Properties is IntervalView report)
            {
                report.Accounts = new AccountsView(engine.Company);
            }
        }

        _engine = engine;

        if (engine.Find(ReportEngine.AccountMapReport, out IReportView? favoriteView, out IntervalView? favoriteReport))
        {
            View = favoriteView;

            RefreshFavorite(favoriteReport);
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

        InitializeReport();
        _timer.Start();
    }

    private void InitializeReport()
    {
        if (View != null && !_backgroundWorker.IsBusy)
        {
            _backgroundWorker.RunWorkerAsync();
        }
    }

    public void InitializeReport(IReportView view)
    {
        View = view;

        InitializeReport();
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        if (_listView.SelectedItems.Count == 0)
        {
            return;
        }

        _timer.Stop();

        View = (IReportView)_listView.SelectedItems[0].Tag!;

        if (View.Properties is GdiCheckReport checkReport && checkReport.Check.Value == null)
        {
            using CheckDialog checkForm = new CheckDialog(checkReport.Check);

            if (checkForm.ShowDialog() == DialogResult.OK)
            {
                checkReport.Check = checkForm.Value;
            }
        }

        InitializeReport();
    }

    private void OnPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
    {
        _timer.Stop();
        InitializeReport();
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
