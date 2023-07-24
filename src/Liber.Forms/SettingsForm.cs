// SettingsForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using Liber.Forms.Properties;

namespace Liber.Forms;

internal sealed partial class SettingsForm : Form
{
    private static IEnumerable<CultureInfo>? s_availableCultures;

    public SettingsForm()
    {
        InitializeComponent();

        if (s_availableCultures == null)
        {
            s_availableCultures = GetAvailableCultures();
        }

        DialogResult = DialogResult.Cancel;
        cultureComboBox.DataSource = s_availableCultures;
        cultureComboBox.SelectedItem = CultureInfo.CurrentUICulture;
    }

    private static IEnumerable<CultureInfo> GetAvailableCultures()
    {
        List<CultureInfo> result = new List<CultureInfo>();
        ResourceManager resourceManager = new ResourceManager(typeof(Resources));

        foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            if (culture.Equals(CultureInfo.InvariantCulture))
            {
                continue;
            }

            try
            {
                ResourceSet? set = resourceManager.GetResourceSet(
                    culture,
                    createIfNotExists: true,
                    tryParents: false);

                if (set != null)
                {
                    result.Add(culture);
                }
            }
            catch (CultureNotFoundException) { }
        }

        resourceManager.ReleaseAllResources();

        return result;
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        CultureInfo? culture = (CultureInfo?)cultureComboBox.SelectedItem;

        if (culture == null)
        {
            return;
        }

        Settings.Default.Culture = culture.Name;
        CultureInfo.CurrentUICulture = culture;
        DialogResult = DialogResult.OK;

        Settings.Default.Save();
        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private void OnCultureComboBoxFormat(object sender, ListControlConvertEventArgs e)
    {
        CultureInfo? culture = (CultureInfo?)e.ListItem;

        if (culture != null)
        {
            e.Value = culture.DisplayName;
        }
    }
}
