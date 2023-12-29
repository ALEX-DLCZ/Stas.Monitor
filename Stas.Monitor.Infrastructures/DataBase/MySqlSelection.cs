using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.DataBase;

public class MySqlSelection : ISelection
{
    private string _selectQuery = string.Empty;

    private string _endClause = string.Empty;


    public ISelection SelectAll()
    {
        _selectQuery = "SELECT Mesures.*, Alerts.expectedValue FROM Mesures LEFT JOIN Alerts ON Mesures.id = Alerts.idMesure ";

        _endClause = "ORDER BY datetime ASC";
        return this;
    }

    public ISelection SelectDistinct(string tableName)
    {
        _selectQuery = $"SELECT DISTINCT {tableName} FROM Mesures";
        return this;
    }

    public ISelection SelectSpecific(int timeSelected)
    {
        _selectQuery = $"(SELECT MAX(datetime) FROM Mesures )";
        _endClause = $" - INTERVAL {timeSelected} SECOND";
        return this;
    }

    public string GetSelection() => _selectQuery;
    public string GetEndClause() => _endClause;
}
