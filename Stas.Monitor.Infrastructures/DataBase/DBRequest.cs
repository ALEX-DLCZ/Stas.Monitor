using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.DataBase;

public class DBRequest : IRequest
{

    private string _connectionString;

    public DBRequest(string connectionString)
    {
        _connectionString = connectionString;
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
}
