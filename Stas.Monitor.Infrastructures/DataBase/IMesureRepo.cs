using System.Data;

namespace Stas.Monitor.Infrastructures.DataBase;

public interface IMesureRepo
{
    IDbConnection GetConnection();

    IDbCommand GetCommand(IDbConnection connection, string query);

    IDbDataParameter CreateParameter(IDbCommand command, string parameterName, object value);
    
    IDataReader ExecuteReader(IDbCommand command);
}
