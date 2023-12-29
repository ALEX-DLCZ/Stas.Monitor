namespace Stas.Monitor.Domains;

public interface IConditionsList
{
    IConditionsList In(string elementTable, IEnumerable<string> values);

    IConditionsList Equal(string elementTable, object value);

    IConditionsList UpperThan(string elementTable, object value);

    IConditionsList LowerThan(string elementTable, object value);

    IConditionsList UpperOtherBuild(string elementTable, IQueryBuilder selection);

    string GetConditions();

    IEnumerable<ParameterMap> GetParameterMaps();
}
