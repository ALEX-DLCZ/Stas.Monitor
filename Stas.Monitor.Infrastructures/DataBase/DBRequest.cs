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

    public void AddThermometer(string thermometerName)
    {
        // using var connection = new SqlConnection(_connectionString);
        // connection.Open();
        // var command = connection.CreateCommand();
        // command.CommandText = "INSERT INTO Thermometers (Name) VALUES (@Name)";
        // command.Parameters.AddWithValue("@Name", thermometerName);
        // command.ExecuteNonQuery();
    }

    public IRequest Where(object unknown) => throw new NotImplementedException();

    public IEnumerable<string> SelectDistinct(string tableName)
    {
        var sqlRequest = "SELECT DISTINCT " + tableName + " FROM Mesures";
        return _dialoger.SelectDistinctDialog(sqlRequest);


    }
}

