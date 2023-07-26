// Liber.Xsl.h
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.
#pragma once

using namespace System;
using namespace System::Globalization;
using namespace System::Resources;

namespace Liber
{
    public ref class XslExtensions
    {
    public:
        XslExtensions(Report^ report, CultureInfo^ culture)
        {
            _report = report;
            _culture = culture;
        }

        String^ fdate(DateTime value);
        String^ fdatel();
        String^ fm(Decimal value);
        String^ ftspanl();
        String^ ftspans();
        String^ pngets(String^ key, Decimal value);
        String^ gets(String^ key);

        static String^ GetString(String^ key, CultureInfo^ culture);

    private:
        static ResourceManager^ s_resourceManager;

        Report^ _report;
        CultureInfo^ _culture;
    };
}
