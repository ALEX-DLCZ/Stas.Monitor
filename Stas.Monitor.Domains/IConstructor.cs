namespace Stas.Monitor.Domains;

public interface IConstructor
{
    // IConditionsList CreateSelection();
    //
    // IConditionsList CreateConditionsList();



    IConditionsList SelectAll();

    IConditionsList SelectDistinct(string tableName);

    IConditionsList SelectSpecific(int timeSelected);

    IConstructor Conditions(IConditionsList conditionsList);

    string GetSelection();

    string GetEndClause();

    IEnumerable<ParameterMap> GetParameterMaps();

}
