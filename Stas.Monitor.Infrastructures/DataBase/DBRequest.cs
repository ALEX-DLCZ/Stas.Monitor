using System.Linq.Expressions;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.DataBase;

public class DbRequest : IRequest
{
    //TODO c'est ici qu'on crée les requêtes SQL

    private IDialoger _dialoger;

    public DbRequest(IDialoger dialoger)
    {
        _dialoger = dialoger;
    }

    public IRequest Where(object unknown) => throw new NotImplementedException();

    public IEnumerable<string> SelectDistinct(string tableName)
    {
        var sqlRequest = "SELECT DISTINCT " + tableName + " FROM Mesures";
        return _dialoger.SelectDistinctDialog(sqlRequest);

    }

    public IEnumerable<TObjet> Select<TObjet>(Expression<Func<MeasureRecord, TObjet>> mapper)
        => Select()
            .Select(entity => mapper.Compile()(entity));

    public IEnumerable<MeasureRecord> Select()
    {
        //mesure de test forcé
        Measure measure = new Measure(0.5976, 0.0, "0%");
        Measure measure2 = new Measure(16.798352, 0.0, "00.00°");

        MeasureRecord measureRecord = new MeasureRecord("thermometer1", "humidity", DateTime.Now, measure);
        MeasureRecord measureRecord2 = new MeasureRecord("thermometer1", "temperature", DateTime.Now, measure2);

        List<MeasureRecord> mesures = new List<MeasureRecord>();
        mesures.Add(measureRecord);
        mesures.Add(measureRecord2);
        return mesures;
    }

}

