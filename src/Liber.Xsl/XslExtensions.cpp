#include "pch.h"
#include "XslExtensions.h"

using namespace Liber;
using namespace System;
using namespace System::Resources;

String^ XslExtensions::fdate(DateTime value)
{
    return String::Format(GetString(L"__fdate{0}"), value);
}

String^ XslExtensions::fdatel()
{
    return String::Format(GetString(L"__fdatel{0}"), _report->Posted);
}

String^ XslExtensions::fm(Decimal value)
{
    return String::Format(L"{0:#,##0.00 ;(#,##0.00);-   }", value);
}

String^ XslExtensions::ftspanl()
{
    return String::Format(GetString(L"__ftspanl{0}{1}"), _report->Started, _report->Posted);
}

String^ XslExtensions::ftspans()
{
    return String::Format(GetString(L"__ftspans{0}{1}"), _report->Started, _report->Posted);
}

String^ XslExtensions::pngets(String^ key, Decimal value)
{
    if (value < 0)
    {
        return GetString(L"_n_" + key);
    }

    return GetString(L"_p_" + key);
}

String^ XslExtensions::gets(String^ key)
{
    return GetString(key);
}

String^ XslExtensions::GetString(String^ key)
{
    if (!s_resourceManager)
    {
        s_resourceManager = gcnew ResourceManager(XslExtensions::typeid);
    }

    String^ result = s_resourceManager->GetString(key);

    if (!result)
    {
        return key;
    }

    return result;
}
