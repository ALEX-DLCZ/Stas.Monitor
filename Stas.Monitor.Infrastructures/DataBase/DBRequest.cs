using System.Linq.Expressions;
using System.Reflection;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.DataBase;

public class DbRequest : IRequest
{
    //TODO c'est ici qu'on crée les requêtes SQL

    private IDialoger _dialoger;
    private IList<string> _conditions = new List<string>();

    public DbRequest(IDialoger dialoger)
    {
        _dialoger = dialoger;
    }



    public IRequest Where<T>(string columnName, Func<T, string> condition, T value)
    {
        string whereClause = condition.Invoke(value);
        _conditions.Add($"{columnName} {whereClause}");
        return this;
    }
    public IRequest WhereUpdate()
    {
        _conditions.Add("datetime > ?lastupdate");
        return this;
    }



    public IEnumerable<string> SelectDistinct(string tableName)
    {
        var sqlRequest = "SELECT DISTINCT " + tableName + " FROM Mesures";
        return _dialoger.SelectDistinctDialog(sqlRequest);

    }




    public IEnumerable<TObjet> Select<TObjet>(Expression<Func<MeasureRecord, TObjet>> mapper)
        => Select()
            .Select(entity => mapper.Compile()(entity));

    private IEnumerable<MeasureRecord> Select()
    {
        foreach (var condition in _conditions)
        {
            Console.WriteLine("condition:  " +condition);
        }
        //
        // //TODO
        // //mesure de test forcé
        // Measure measure = new Measure(0.5976, 0.01, "0%");
        // Measure measure2 = new Measure(16.798352, 168.5, "00.00°");
        //
        // MeasureRecord measureRecord = new MeasureRecord("thermometer1", "humidity", DateTime.Now, measure);
        // MeasureRecord measureRecord2 = new MeasureRecord("thermometer1", "temperature", DateTime.Now, measure2);
        //
        // List<MeasureRecord> mesures = new List<MeasureRecord>();
        // mesures.Add(measureRecord);
        // mesures.Add(measureRecord2);


        // string whereClause = "WHERE " + string.Join(" AND ", _conditions);
        string whereClause = " ";
        if (_conditions.Count > 0)
        {
             whereClause = "WHERE " + string.Join(" AND ", _conditions) + " ";
        }

        var result = "SELECT Mesures.*, Alerts.expectedValue " +
                     "FROM Mesures " +
                     "LEFT JOIN Alerts ON Mesures.id = Alerts.idMesure " +
                     whereClause +
                     "ORDER BY datetime DESC";



        return _dialoger.AllValeur(result);
    }

}

