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
