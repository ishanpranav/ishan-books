// ReportWebView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Liber.Forms.RecentPaths;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Liber.Forms.Reports;

internal class ReportWebView : WebView2
{
    private BackgroundWorker? _backgroundWorker = new BackgroundWorker()
    {
        WorkerSupportsCancellation = true
    };
    private IReportView? _view;

    public ReportWebView()
    {
        _backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
        _backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
    }

    public event EventHandler? Generated;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CoreWebView2PrintSettings? PrintSettings { get; private set; }

    public async Task InitializeWebViewAsync(RecentPathManager recentPathManager)
    {
        try
        {
            await EnsureCoreWebView2Async();

            CoreWebView2.SetVirtualHostNameToFolderMapping(
                "liber.example",
                Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,
                CoreWebView2HostResourceAccessKind.DenyCors);

            if (!recentPathManager.Empty)
            {
                CoreWebView2.Profile.DefaultDownloadFolderPath = Path.GetDirectoryName(recentPathManager.Paths.First());
            }

            CoreWebView2.Profile.PreferredColorScheme = CoreWebView2PreferredColorScheme.Light;
            CoreWebView2.NavigationCompleted += OnCoreWebView2NavigationCompleted;

            PrintSettings = CoreWebView2.Environment.CreatePrintSettings();

            PrintSettings.ShouldPrintBackgrounds = true;
        }
        catch (COMException) { }
    }

    public void GenerateReport(IReportView view)
    {
        _view = view;

        if (_backgroundWorker != null && !_backgroundWorker.IsBusy)
        {
            _backgroundWorker.RunWorkerAsync();
        }
    }

    private void OnBackgroundWorkerDoWork(object? sender, DoWorkEventArgs e)
    {
        if (_view == null)
        {
            e.Cancel = true;

            return;
        }

        _view.Generate();
    }

    private void OnBackgroundWorkerRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled || _view == null)
        {
            return;
        }

        if (CoreWebView2 != null)
        {
            _view.Navigate(CoreWebView2);
        }
    }

    private void OnCoreWebView2NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        if (e.IsSuccess)
        {
            OnGenerated(EventArgs.Empty);
        }
    }

    protected virtual void OnGenerated(EventArgs e)
    {
        Generated?.Invoke(sender: this, e);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_backgroundWorker != null)
            {
                _backgroundWorker.Dispose();

                _backgroundWorker = null;
            }
        }

        base.Dispose(disposing);
    }
}
