#include "pch.h"
#include "XslExtensions.h"

using namespace Liber;
using namespace System;
using namespace System::Resources;

String^ XslExtensions::fdate(DateTime value)
{
    return String::Format(gets(L"__fdate{0}"), value);
}

String^ XslExtensions::fdatel()
{
    return String::Format(gets(L"__fdatel{0}"), _report->Posted);
}

String^ XslExtensions::fm(Decimal value)
{
    return String::Format(L"{0:#,##0.00 ;(#,##0.00);-   }", value);
}

String^ XslExtensions::ftspanl()
{
    return String::Format(gets(L"__ftspanl{0}{1}"), _report->Started, _report->Posted);
}

String^ XslExtensions::ftspans()
{
    return String::Format(gets(L"__ftspans{0}{1}"), _report->Started, _report->Posted);
}

String^ XslExtensions::pngets(String^ key, Decimal value)
{
    if (value < 0)
    {
        return gets(L"_n_" + key);
    }

    return gets(L"_p_" + key);
}

String^ XslExtensions::gets(String^ key)
{
    return GetString(key, _culture);
}

String^ XslExtensions::GetString(String^ key, CultureInfo^ culture)
{
    if (!s_resourceManager)
    {
        s_resourceManager = gcnew ResourceManager(XslExtensions::typeid);
    }

    String^ result = s_resourceManager->GetString(key, culture);

    if (!result)
    {
        return key;
    }

    return result;
}
