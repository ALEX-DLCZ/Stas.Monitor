using System.Linq.Expressions;

namespace Stas.Monitor.Domains;

public interface IRequest
{


    IRequest Where<T>(string columnName, Func<T, string> condition, T value);


    // IRequest Where(ICondition criterion);
    //
    //
    // IRequest SortBy(Expression<Comparison> comparison);
    //
    // IEnumerable Select();
    //
    // IEnumerable SelectDistinct();
    //
    // IEnumerable<TArg> Select<TArg>(Expression<Func<TArg>> mapper);
    //
    // IEnumerable<TArg> SelectDistinct<TArg>(Expression<Func<TArg>> mapper);


    IEnumerable<string> SelectDistinct(string tableName);

    IEnumerable<TObjet> Select<TObjet>(Expression<Func<MeasureRecord, TObjet>> mapper);

}
