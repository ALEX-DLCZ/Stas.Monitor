namespace Stas.Monitor.Domains;

public interface ISelection
{
    ISelection SelectAll();

    ISelection SelectDistinct(string tableName);

    ISelection SelectSpecific(int timeSelected);

    string GetSelection();

    string GetEndClause();

}
