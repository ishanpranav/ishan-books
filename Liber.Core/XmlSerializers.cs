using System.Xml.Serialization;

namespace Liber;

internal static class XmlSerializers
{
    private static XmlSerializer? s_report;
    private static XmlSerializer? s_account;

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
