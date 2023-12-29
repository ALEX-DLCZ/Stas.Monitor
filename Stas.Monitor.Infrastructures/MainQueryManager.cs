using Stas.Monitor.Domains;
using Stas.Monitor.Infrastructures.DataBase;

namespace Stas.Monitor.Infrastructures;

public class MainQueryManager :IQueryManager
{
    private readonly IDbQuery _dialoger;
    private readonly IConstructor _constructor;
    private readonly IQueryBuilder _builder;

    public MainQueryManager(IDbQuery dialoger)
    {
        _constructor = new MySqlConstructor();
        _builder = new MySqlQueryBuilder();
        _dialoger = dialoger;
    }


    public IEnumerable<string> ExecuteSimple(IQueryBuilder request)
    {
        return _dialoger.GetStringsByDbQuery(request);
    }

    public IEnumerable<MeasureRecord> ExecuteMesure(IQueryBuilder request)
    {
        return _dialoger.GetMesuresByDbQuery(request);
    }

    public IConstructor ConstructQuery()
    {
        return _constructor;
    }

    public IQueryBuilder Building(IConstructor constructor)
    {
        return _builder.BuilderFromConstructor(constructor);
    }

}
