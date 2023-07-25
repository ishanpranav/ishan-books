// Liber.Xsl.h
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.
#pragma once

using namespace System;
using namespace System::Resources;

namespace Liber
{
    public ref class XslExtensions
    {
    public:
        XslExtensions(Report^ report)
        {
            _report = report;
        }

        String^ fdate(DateTime value);
        String^ fdatel();
        String^ fm(Decimal value);
        String^ ftspanl();
        String^ ftspans();
        String^ pngets(String^ key, Decimal value);
        String^ gets(String^ key);

        static String^ GetString(String^ key);

    private:
        Report^ _report;
        static ResourceManager^ s_resourceManager;
    };
}
