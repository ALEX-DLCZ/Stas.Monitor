using System.Text;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.DataBase;

public class MySqlQueryBuilder : IQueryBuilder
{
    // public void BuildQuery(ISelection selection, IConditionsList? conditionsList)
    // {
    //     var queryBuilder = new StringBuilder();
    //     queryBuilder.Append(selection.GetSelection());
    //     if (conditionsList != null)
    //     {
    //         queryBuilder.Append($" WHERE {conditionsList.GetConditions()}");
    //     }
    //
    //     queryBuilder.Append(selection.GetEndClause());
    //     Query = queryBuilder.ToString();
    //     ParameterMaps = conditionsList?.GetParameterMaps();
    // }
    //
    // public void BuildQuery(ISelection selection)
    // {
    //     var queryBuilder = new StringBuilder();
    //     queryBuilder.Append(selection.GetSelection());
    //     queryBuilder.Append(selection.GetEndClause());
    //     Query = queryBuilder.ToString();
    //     ParameterMaps = null;
    // }

    public IQueryBuilder BuilderFromConstructor(IConstructor constructor)
    {
        var queryBuilder = new StringBuilder();
        queryBuilder.Append(constructor.GetSelection());
        queryBuilder.Append(constructor.GetEndClause());
        Query = queryBuilder.ToString();
        ParameterMaps = constructor.GetParameterMaps();
        return this;
    }


    public string? Query { get; private set; }

    public IEnumerable<ParameterMap>? ParameterMaps { get; private set; }

}
