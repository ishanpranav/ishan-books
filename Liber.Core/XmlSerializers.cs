using System.Xml.Serialization;

namespace Liber;

internal static class XmlSerializers
{
    private static XmlSerializer? s_stylesheet;
    private static XmlSerializer? s_report;
    private static XmlSerializer? s_account;

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
}
