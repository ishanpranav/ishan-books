namespace Liber.Sqlite;

public interface IProgress
{
    void WriteAccount();
    void WriteTransaction();
}
