using Liber.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

namespace Liber.Forms;

internal sealed partial class SettingsForm : Form
{
    private static IEnumerable<CultureInfo>? s_availableCultures;

    private readonly Settings _settings;

    public SettingsForm(Settings settings)
    {
        InitializeComponent();

        if (s_availableCultures == null)
        {
            s_availableCultures = GetAvailableCultures();
        }

        DialogResult = DialogResult.Cancel;
        cultureComboBox.DataSource = s_availableCultures;
        _settings = settings;
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
        _settings.Culture = (CultureInfo?)cultureComboBox.SelectedItem;
        DialogResult = DialogResult.OK;

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
