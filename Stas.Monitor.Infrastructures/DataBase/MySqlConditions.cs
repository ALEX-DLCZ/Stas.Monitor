using System.Text;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.DataBase;

public class MySqlConditions : IConditionsList
{
    private readonly IList<string> _conditions = new List<string>();
    private IList<ParameterMap> _parameterMaps = new List<ParameterMap>();

    public IConditionsList In(string elementTable, IEnumerable<string> values)
    {
        var i = 0;
        var valuesList = values.ToList();
        var condition = new StringBuilder($"{elementTable} IN (");
        foreach (var value in valuesList)
        {
            condition.Append($"@{elementTable}{i}, ");
            _parameterMaps.Add(new ParameterMap($"@{elementTable}{i}", value));
            i++;
        }
        return this;
    }

    public IConditionsList Equal(string elementTable, object value)
    {
        _conditions.Add($"{elementTable} = @{elementTable}");
        _parameterMaps.Add(new ParameterMap($"@{elementTable}", value));
        return this;
    }

    public IConditionsList UpperThan(string elementTable, object value)
    {
        _conditions.Add($"{elementTable} > @{elementTable}");
        _parameterMaps.Add(new ParameterMap($"@{elementTable}", value));
        return this;
    }

    public IConditionsList LowerThan(string elementTable, object value)
    {
        _conditions.Add($"{elementTable} < @{elementTable}");
        _parameterMaps.Add(new ParameterMap($"@{elementTable}", value));
        return this;
    }

    public IConditionsList UpperOtherBuild(string elementTable, IQueryBuilder build)
    {
        _conditions.Add($"{elementTable} >= ({build.Query})");
        //fusion des paramètres de la requête imbriquée avec ceux de la requête principale et gestion des doublons
        _parameterMaps = _parameterMaps.Union(build.ParameterMaps).ToList();

        return this;
    }


    public string GetConditions() => string.Join(" AND ", _conditions);

    public IEnumerable<ParameterMap> GetParameterMaps() => _parameterMaps;
}
