namespace Stas.Monitor.Domains;

public interface IQueryBuilder
{
    // void BuildQuery(ISelection selection, IConditionsList? conditionsList);
    //
    // void BuildQuery(ISelection selection);

    IQueryBuilder BuilderFromConstructor(IConstructor constructor);

    public string? Query { get; }

    public IEnumerable<ParameterMap>? ParameterMaps { get; }
}
