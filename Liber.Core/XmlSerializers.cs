using System.Xml.Serialization;

namespace Liber;

internal static class XmlSerializers
{
    private static XmlSerializer? s_stylesheet;
    private static XmlSerializer? s_report;
    private static XmlSerializer? s_account;
    private static XmlSerializer? s_transaction;
    private static XmlSerializer? s_line;

    public static XmlSerializer Stylesheet
    {
        get
        {
            s_stylesheet ??= new XmlSerializer(typeof(XslStylesheet));

            return s_stylesheet;
        }
    }

    public static XmlSerializer Report
    {
        get
        {
            s_report ??= new XmlSerializer(typeof(Report));

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
}
