using System.Linq.Expressions;

namespace Stas.Monitor.Domains;

public interface IQueryManager
{

    IConstructor ConstructQuery();

    IQueryBuilder Building(IConstructor constructor);

    IEnumerable<string> ExecuteSimple(IQueryBuilder request);

    IEnumerable<MeasureRecord> ExecuteMesure(IQueryBuilder request);


}
