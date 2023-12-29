using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.DataBase;

public class DbMesureRequest : IDbRequest
{
    private readonly IMesureRepo _mesureRepository;

    public DbMesureRequest(IMesureRepo mesureRepository)
    {
        _mesureRepository = mesureRepository;
    }

    public IEnumerable<Dictionary<string, object>> GetValueByDbQuery(IQueryBuilder queryAccess)
    {
        string sqlQuery = queryAccess.Query ?? throw new ArgumentNullException(nameof(queryAccess.Query));

        using (var connection = _mesureRepository.GetConnection())
        {
            connection.Open();

            using (var command = _mesureRepository.GetCommand(connection, sqlQuery))
            {
                queryAccess.ParameterMaps?.ToList().ForEach(x =>
                {
                    var parameter = _mesureRepository.CreateParameter(command, x.Key, x.Value);
                    command.Parameters.Add(parameter);
                });

                using (var reader = _mesureRepository.ExecuteReader(command))
                {
                    var result = new List<Dictionary<string, object>>();
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader[i]);
                        }

                        result.Add(row);
                    }

                    return result;
                }
            }
        }
    }
}
