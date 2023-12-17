using System.Data;
using Stas.Monitor.Infrastructures.PersonalExceptions;

namespace Stas.Monitor.Infrastructures.DataBase;

using Domains;
using MySql.Data.MySqlClient;

public class DbDialog : IDialoger
{
    private readonly string _connectionString;
    private DateTime _lastUpdate;

    public DbDialog(string connectionString)
    {
        _connectionString = connectionString;
    }

    /*
     * -- Table 'Mesures'
     * CREATE TABLE Mesures (
     * id INT AUTO_INCREMENT PRIMARY KEY,
     * thermometerName VARCHAR(255) NOT NULL,
     * datetime DATETIME NOT NULL,
     * type VARCHAR(50) NOT NULL,
     * format VARCHAR(50) NOT NULL,
     * value DOUBLE NOT NULL
     * );
     * -- Table 'Alerts'
     * CREATE TABLE Alerts (
     * id INT AUTO_INCREMENT PRIMARY KEY,
     * expectedValue DOUBLE NOT NULL,
     * idMesure INT NOT NULL,
     * FOREIGN KEY (idMesure) REFERENCES Mesures(id)
     * );
     */
    public IEnumerable<string> SelectDistinctDialog(string request)
    {
        try
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = request;
            using var reader = command.ExecuteReader();

            var result = new List<string>();

            while (reader.Read())
            {
                result.Add(reader.GetString(0));
            }

            return result;
        }
        catch (MySqlException e)
        {
            throw new DbConnectionException(" stas monitor : unable to connect to the database");
        }
    }

    public IEnumerable<MeasureRecord> AllValeur(string commande)
    {
        try
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = commande;

            command.Parameters.AddWithValue("lastupdate", _lastUpdate);
            using var reader = command.ExecuteReader();
            var measureRecords = new List<MeasureRecord>();
            while (reader.Read())
            {
                measureRecords.Add(MapMeasure(reader));
            }

            _lastUpdate = DateTime.Now;
            return measureRecords;
        }
        catch (MySqlException e)
        {
            throw new DbDataRequestException("  stas monitor : unable to read data");
        }
    }

    private MeasureRecord MapMeasure(IDataRecord reader)
    {
        var name = reader["thermometerName"] as string ?? "Unknown";
        var type = reader["type"] as string ?? "Unknown";
        var date = reader.GetDateTime(reader.GetOrdinal("datetime"));

        var value = reader.GetDouble(reader.GetOrdinal("value"));
        var expectedValue = reader["expectedValue"] as double?;
        var format = reader["format"] as string ?? "DefaultFormat";

        var difference = expectedValue.HasValue ? expectedValue.Value - value : 0.0;
        var measure = new Measure(value, difference, format);

        return new MeasureRecord(name, type, date, measure);
    }
}
