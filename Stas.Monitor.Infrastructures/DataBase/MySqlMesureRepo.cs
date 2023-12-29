using System.Data;
using MySql.Data.MySqlClient;

namespace Stas.Monitor.Infrastructures.DataBase;

public class MySqlMesureRepo : IMesureRepo
{
    private readonly string _connectionString;

    public MySqlMesureRepo(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection GetConnection() => new MySqlConnection(_connectionString);

    public IDbCommand GetCommand(IDbConnection connection, string query) => new MySqlCommand(query, (MySqlConnection)connection);

    public IDbDataParameter CreateParameter(IDbCommand command, string parameterName, object value)
    {
        var parameter = ((MySqlCommand)command).CreateParameter();
        parameter.ParameterName = parameterName;
        parameter.Value = value;
        return parameter;
    }

    public IDataReader ExecuteReader(IDbCommand command) => command.ExecuteReader();
}
