// ReportsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.Lines;
using Liber.Forms.RecentPaths;
using Liber.Forms.Reports.Gdi;

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
        await _webView.InitializeWebViewAsync(_recentPathManager);

        if (View != null)
        {
            _webView.GenerateReport(View);
        }

        _timer.Start();
    }

    public void InitializeReport(IReportView view)
    {
        View = view;

        _webView.GenerateReport(view);
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

        _webView.GenerateReport(view);
    }

    private void OnPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
    {
        _timer.Stop();

        if (View != null)
        {
            _webView.GenerateReport(View);
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
        _webView.GenerateReport(View);
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
