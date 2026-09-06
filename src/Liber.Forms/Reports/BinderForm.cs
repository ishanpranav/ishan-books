// BinderForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Windows.Forms;

namespace Liber.Forms.Reports;

internal partial class BinderForm : Form
{
    private readonly ReportEngine _engine;

    public BinderForm(ReportEngine engine)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _engine = engine;
    }
}
