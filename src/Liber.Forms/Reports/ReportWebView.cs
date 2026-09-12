// ReportWebView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Liber.Forms.RecentPaths;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Liber.Forms.Reports;

internal class ReportWebView : WebView2
{
    private bool _printing;
    private BackgroundWorker? _backgroundWorker = new BackgroundWorker()
    {
        WorkerSupportsCancellation = true
    };
    private IReportView? _view;
    private Action? _callback;

    public ReportWebView()
    {
        _backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
        _backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
    }

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

    public void GenerateReport(IReportView view, Action? callback = null)
    {
        _view = view;

        if (_backgroundWorker != null && !_backgroundWorker.IsBusy)
        {
            _callback = callback;

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
            _view.Navigate(this);
        }
    }

    private void OnCoreWebView2NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        if (e.IsSuccess && _callback != null)
        {
            _callback();

            _callback = null;
        }
    }

    public void PrintQueue(
        string path,
        Queue<IReportView> queue,
        Action? progressCallback,
        Action? completeCallback)
    {
        if (_printing)
        {
            return;
        }

        if (queue.Count == 0)
        {
            return;
        }

        _printing = true;

        PdfDocument document = new PdfDocument();

        DequeuePrint(pageIndex: 0, queue, path, document, progressCallback, () =>
        {
            document.Save(path);
            document.Close();
            document.Dispose();

            _printing = false;

            if (completeCallback != null)
            {
                completeCallback();
            }
        });
    }

    private void DequeuePrint(
        int pageIndex,
        Queue<IReportView> queue,
        string path,
        PdfDocument document,
        Action? progressCallback,
        Action? completeCallback)
    {
        if (!queue.TryDequeue(out IReportView? view))
        {
            if (completeCallback != null)
            {
                completeCallback();
            }

            return;
        }

        GenerateReport(view, async () =>
        {
            using Stream stream = await CoreWebView2.PrintToPdfStreamAsync(PrintSettings);
            using MemoryStream memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);

            using PdfDocument inputDocuemnt = PdfReader.Open(memoryStream, PdfDocumentOpenMode.Import);

            int j = pageIndex;

            foreach (PdfPage page in inputDocuemnt.Pages)
            {
                document.AddPage(page);

                pageIndex++;
            }

            document.Outlines.Add(view.Properties.Title, document.Pages[j], opened: true);

            if (progressCallback != null)
            {
                progressCallback();
            }

            DequeuePrint(
                pageIndex,
                queue,
                path,
                document,
                progressCallback,
                completeCallback);
        });
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
