using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.DataBase;

public interface IDbRequest
{
    IEnumerable<Dictionary<string,object>> GetValueByDbQuery(IQueryBuilder queryAccess);
}
