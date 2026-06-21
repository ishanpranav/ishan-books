// FormattedStrings.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Humanizer;

namespace Liber.Forms;

internal static class FormattedStrings
{
    public static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
    {
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    static FormattedStrings()
    {
        JsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
        JsonOptions.Converters.Add(new JsonColorConverter());
        JsonOptions.Converters.Add(new TypeConverterJsonConverterAdapter());
    }

    public static string GrossProfit
    {
        get
        {
            return GetString("_p_gross-profit");
        }
    }

    public static string OperatingIncome
    {
        get
        {
            return GetString("_p_operating-income");
        }
    }

    public static string PretaxIncome
    {
        get
        {
            return GetString("_p_pretax-income");
        }
    }

    public static string NetIncome
    {
        get
        {
            return GetString("_p_net-income");
        }
    }

    public static string ComprehensiveIncome
    {
        get
        {
            return GetString("_p_comprehensive-income");
        }
    }

    public static string WorkingCapital
    {
        get
        {
            return GetString("_p_working-capital");
        }
    }

    public static string NonCash
    {
        get
        {
            return GetString("_p_non-cash");
        }
    }

    public static string PlusGain
    {
        get
        {
            return GetString("plus-gain");
        }
    }

    public static string LessGain
    {
        get
        {
            return GetString("less-gain");
        }
    }

    public static string Operating
    {
        get
        {
            return GetString("operating");
        }
    }

    public static string NetOperating
    {
        get
        {
            return GetString("_p_operating");
        }
    }

    public static string Financing
    {
        get
        {
            return GetString("financing");
        }
    }

    public static string NetFinancing
    {
        get
        {
            return GetString("_p_financing");
        }
    }

    public static string Investing
    {
        get
        {
            return GetString("investing");
        }
    }

    public static string NetInvesting
    {
        get
        {
            return GetString("_p_investing");
        }
    }

    public static string NetCashFlow
    {
        get
        {
            return GetString("_p_net-cash-flow");
        }
    }

    public static Uri AboutUri
    {
        get
        {
            return new Uri(GetString());
        }
    }

    public static Uri LicenseUri
    {
        get
        {
            return new Uri(GetString());
        }
    }

    public static Uri ThirdPartyNoticesUri
    {
        get
        {
            return new Uri(GetString());
        }
    }

    public static ResourceManager ResourceManager { get; } = new ResourceManager(typeof(FormattedStrings));

    public static string GetString([CallerMemberName] string? key = null)
    {
        if (key == null)
        {
            return string.Empty;
        }

        return ResourceManager.GetString(key) ?? key;
    }

    public static bool TryGetString(string key, [MaybeNullWhen(false)] out string value)
    {
        value = ResourceManager.GetString(key);

        return value != null;
    }

    public static IEnumerable<string> GetStringsBySuffix(string suffix)
    {
        ResourceSet? resources = ResourceManager.GetResourceSet(
            CultureInfo.InvariantCulture,
            createIfNotExists: true,
            tryParents: true);

        if (resources == null)
        {
            yield break;
        }

        foreach (DictionaryEntry entry in resources)
        {
            string? key = entry.Key.ToString();

            if (key == null || !key.EndsWith(suffix) || entry.Value == null)
            {
                continue;
            }

            string? value = ResourceManager.GetString(key, CultureInfo.CurrentUICulture);

            if (value == null)
            {
                continue;
            }

            yield return value;
        }
    }

    public static string GetCheckWords(decimal amount)
    {
        decimal integral = decimal.Floor(amount);
        decimal fractional = decimal.Round((amount - integral) * 100, 0);

        if (integral > int.MaxValue)
        {
            return string.Format(GetString("CheckWords{0}{1}"), integral, fractional);
        }

        return string.Format(GetString("CheckWords{0}{1}"), ((int)integral).ToWords(), fractional);
    }

    private static string GetClearedCountFormat()
    {
        return GetString("ClearedCount{0}");
    }

    public static string GetClearedDebitCount(int count)
    {
        return string.Format(GetClearedCountFormat(), Properties.Resources.Debit.ToQuantity(count, format: "n0"));
    }

    public static string GetClearedCreditCount(int count)
    {
        return string.Format(GetClearedCountFormat(), Properties.Resources.Credit.ToQuantity(count, format: "n0"));
    }

    public static string GetMultipleWords(decimal multiple)
    {
        switch (multiple)
        {
            case 0:
            case 1:
            case 0.01m:
                return string.Empty;
        }

        if (multiple > int.MaxValue || !multiple.IsPow10() || multiple < 1)
        {
            return string.Format(GetString("MultipleWords2{0}"), multiple);
        }

        int integral = (int)multiple;
        string words = integral
            .ToWords()
            .Replace(1.ToWords(), string.Empty)
            .Trim()
            .Pluralize();

        return string.Format(GetString("MultipleWords1{0}"), words);
    }

    public static string GetTaxTypeText(bool taxType)
    {
        return GetString(string.Format("TaxType{0}", taxType));
    }

    public static DialogResult ShowNotSupportedMessage(string extension)
    {
        string text;

        if (extension.Length == 0)
        {
            text = GetString("NotSupportedText");
        }
        else
        {
            text = string.Format(GetString("NotSupportedText{0}"), extension.Substring(1).ToUpper());
        }

        return MessageBox.Show(text, Properties.Resources.ExceptionCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static DialogResult ShowDeleteTransactionMessage()
    {
        return MessageBox.Show(
            Properties.Resources.DeleteTransactionText,
            Properties.Resources.DeleteTransactionCaption,
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);
    }

    public static DialogResult ShowDeleteAccountMessage()
    {
        return MessageBox.Show(
            Properties.Resources.DeleteAccountText,
            Properties.Resources.ExceptionCaption,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    public static DialogResult ShowUnreconcileMessage(Account account)
    {
        return MessageBox.Show(
            string.Format(GetString("UnreconcileText{0}{1}{2}"),
                account.Name,
                account.Reconciled,
                Properties.Resources.Lines.ToQuantity(account.LastReconciledLines?.Count ?? 0, format: "n0")),
            Properties.Resources.UnreconcileCaption,
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);
    }

    public static DialogResult ShowCancelMessage(Company company)
    {
        return MessageBox.Show(
            string.Format(GetString("CancelText{0}"), company.DisplayName),
            Properties.Resources.CancelCaption,
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Warning);
    }

    public static string GetTitle(string name)
    {
        if (!TryGetString("_r_" + name, out string? result))
        {
            return name;
        }

        return result;
    }

    public static string GetTitle(string name, CompanyType type)
    {
        if (type == CompanyType.None ||
            !TryGetString($"_r_{type}_{name}", out string? result))
        {
            return GetTitle(name);
        }

        return result;
    }

    public static string GetNullReconciledDescription(Account account)
    {
        return string.Format(GetString("NullReconciledDescription{0}"), account.Name);
    }

    public static string GetOverdueReconciledDescription(Account account, TimeSpan overdue)
    {
        return string.Format(GetString("OverdueReconciledDescription{0}{1}"),
                account.Name,
                overdue.Humanize(precision: 2, minUnit: TimeUnit.Day, maxUnit: TimeUnit.Year));
    }

    public static string GetOverduePostedDescription(Account account, TimeSpan overdue)
    {
        return string.Format(GetString("OverduePostedDescription{0}{1}"),
            account.Name,
            overdue.Humanize(precision: 2, minUnit: TimeUnit.Day, maxUnit: TimeUnit.Year));
    }
}
