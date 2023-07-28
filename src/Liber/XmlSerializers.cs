// XmlSerializers.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Xml.Serialization;

namespace Liber;

public static class XmlSerializers
{
    private static XmlSerializer? s_report;
    private static XmlSerializer? s_account;
    private static XmlSerializer? s_transaction;
    private static XmlSerializer? s_line;
    private static XmlSerializer? s_company;

    public static XmlSerializer Report
    {
        get
        {
            s_report ??= new XmlSerializer(typeof(XslReport));

            return s_report;
        }
    }

    public static XmlSerializer Account
    {
        get
        {
            s_account ??= new XmlSerializer(typeof(Account));

            return s_account;
        }
    }

    public static XmlSerializer Transaction
    {
        get
        {
            s_transaction ??= new XmlSerializer(typeof(Transaction));

            return s_transaction;
        }
    }

    public static XmlSerializer Line
    {
        get
        {
            s_line ??= new XmlSerializer(typeof(Line));

            return s_line;
        }
    }

    public static XmlSerializer Company
    {
        get
        {
            s_company ??= new XmlSerializer(typeof(Company));

            return s_company;
        }
    }
}
