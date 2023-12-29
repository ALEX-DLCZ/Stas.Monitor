using System.Data;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public interface IDbQuery
{
    public IEnumerable<MeasureRecord> GetMesuresByDbQuery(IQueryBuilder queryAccess);

    public IEnumerable<string?> GetStringsByDbQuery(IQueryBuilder queryAccess);
}
