using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.DataBase;

public class DbQuery : IDbQuery
{
    private readonly IDbRequest _dbMesureRequest;

    public DbQuery(IDbRequest dbMesureRequest)
    {
        _dbMesureRequest = dbMesureRequest;
    }


    public IEnumerable<string?> GetStringsByDbQuery(IQueryBuilder queryAccess)
    {
        return _dbMesureRequest.GetValueByDbQuery(queryAccess).Select(row => row.Values.First().ToString());
    }


    public IEnumerable<MeasureRecord> GetMesuresByDbQuery(IQueryBuilder queryAccess)
    {
        return MapMeasures(_dbMesureRequest.GetValueByDbQuery(queryAccess));


    }


    //si la fonction demmande un MesureRecord alors on aurait quelque chose comme ceci:
    private MeasureRecord MapMeasure(IDictionary<string, object> row)
    {
            var name = row["thermometerName"] as string?? "Unknown";
            var type = row["type"] as string?? "Unknown";
            var date = row["datetime"] as DateTime? ?? DateTime.Now;
            var value = row["value"] as double? ?? 0.0;
            var expectedValue = row["expectedValue"] as double?;
            var format = row["format"] as string?? "DefaultFormat";
            var difference = expectedValue.HasValue ? value - expectedValue.Value : 0.0;
            var measure = new Measure(value, difference, format);
            return new MeasureRecord(name, type, date, measure);
    }

    private IEnumerable<MeasureRecord> MapMeasures (IEnumerable<IDictionary<string, object>> rows)
    {
        return rows.Select(MapMeasure);
    }
}
