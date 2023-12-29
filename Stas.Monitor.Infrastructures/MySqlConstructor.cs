using Stas.Monitor.Domains;
using Stas.Monitor.Infrastructures.DataBase;

namespace Stas.Monitor.Infrastructures;

public class MySqlConstructor : IConstructor
{
    private string _selectQuery = string.Empty;

    private string _endClause = string.Empty;

    private IList<ParameterMap> _parameterMaps = new List<ParameterMap>();


    public IConditionsList SelectAll()
    {
        _selectQuery = "SELECT Mesures.*, Alerts.expectedValue FROM Mesures LEFT JOIN Alerts ON Mesures.id = Alerts.idMesure ";

        _endClause = "ORDER BY datetime ASC";
        return new MySqlConditions();
    }

    public IConditionsList SelectDistinct(string tableName)
    {
        _selectQuery = $"SELECT DISTINCT {tableName} FROM Mesures";
        return new MySqlConditions();
    }

    public IConditionsList SelectSpecific(int timeSelected)
    {
        _selectQuery = $"(SELECT MAX(datetime) FROM Mesures ";
        _endClause = $" ) - INTERVAL {timeSelected} SECOND";
        return new MySqlConditions();
    }

    public IConstructor Conditions(IConditionsList conditionsList)
    {
        _selectQuery += $"WHERE {conditionsList.GetConditions()} ";
        _parameterMaps = new List<ParameterMap>(conditionsList.GetParameterMaps());
        return this;
    }

    public string GetSelection() => _selectQuery;

    public string GetEndClause() => _endClause;

    public IEnumerable<ParameterMap> GetParameterMaps() => _parameterMaps;
}
