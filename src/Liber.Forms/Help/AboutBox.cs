// AboutBox.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Windows.Forms;
using Liber.Forms.Forms;

namespace Liber.Forms.Help;

internal partial class AboutBox : Form
{
    private readonly FormFactory _factory;

    public AboutBox(FormFactory facotry)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.OK;
        Text = string.Format(Text, SystemFeatures.ApplicationName);
        applicationNameLabel.Text = SystemFeatures.ApplicationName;
        applicationNameLabel.ForeColor = Colors.Primary;
        versionLabel.Text = string.Format(versionLabel.Text, SystemFeatures.ApplicationVersion);
        copyrightLabel.Text = SystemFeatures.ApplicationCopyright;
        companyLabel.Text = SystemFeatures.ApplicationCompany;
        _factory = facotry;
    }

    private void OnLicenseLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        _factory.AutoRegister(() => new UriForm(FormattedStrings.LicenseUri));
    }

    private void OnThirdPartyNoticesLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        _factory.AutoRegister(() => new UriForm(FormattedStrings.ThirdPartyNoticesUri));
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;

        Close();
    }
}
