using System.Linq.Expressions;

namespace Stas.Monitor.Domains;

public interface IRequest
{
    IRequest WhereUpdate();

    IRequest Where<T>(string columnName, Func<T, string> condition, T value);

    IEnumerable<string> SelectDistinct(string tableName);

    IEnumerable<TObjet> Select<TObjet>(Expression<Func<MeasureRecord, TObjet>> mapper);
}
