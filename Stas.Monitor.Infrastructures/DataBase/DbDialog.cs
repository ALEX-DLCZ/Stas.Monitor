using System.Collections;

namespace Stas.Monitor.Infrastructures.DataBase;
using Domains;

using MySql.Data.MySqlClient;
public class DbDialog: IDialoger
{
    private readonly string _connectionString;

    public DbDialog(string connectionString)
    {
        _connectionString = connectionString;
    }

    //sélectionne le nom de tout les thermomètres dans la base de données dans la table Mesure
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
    public string[] AllThermometers
    {
        get
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT thermometerName FROM Mesures";
            using MySqlDataReader reader = command.ExecuteReader();
            List<string> thermometers = new List<string>();
            while (reader.Read())
            {
                thermometers.Add(reader.GetString(0));
            }

            return thermometers.ToArray();
        }
    }

    public IEnumerable<string> SelectDistinctDialog(string request)
    {

        using MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = request;
        using MySqlDataReader reader = command.ExecuteReader();

        var result = new List<string>();

        while (reader.Read())
        {
            result.Add(reader.GetString(0));
        }
        return result;
    }

    public IEnumerable<MeasureRecord> allValeurGPT()
    {
        int secondAfterFirstValue = 600;
        using MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT Mesures.*, Alerts.expectedValue " +
                              "FROM Mesures " +
                              "LEFT JOIN Alerts ON Mesures.id = Alerts.idMesure " +
                              "WHERE datetime >= (SELECT MAX(datetime) FROM Mesures) - INTERVAL ?seconds SECOND " +
                              "AND thermometerName LIKE '%Living Room 1%'" +
                              "AND  type IN ('temperature','humidity') " +
                              "ORDER BY datetime DESC";


        command.Parameters.AddWithValue("seconds", secondAfterFirstValue);
        using MySqlDataReader reader = command.ExecuteReader();
        List<MeasureRecord> measureRecords = new List<MeasureRecord>();
        while (reader.Read())
        {
            measureRecords.Add(MapMeasure(reader));
        }
        return measureRecords;
    }
    public IEnumerable<MeasureRecord> AllValeur(string commande)
    {
        using MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = commande;

        using MySqlDataReader reader = command.ExecuteReader();
        List<MeasureRecord> measureRecords = new List<MeasureRecord>();
        while (reader.Read())
        {
            measureRecords.Add(MapMeasure(reader));
        }
        return measureRecords;
    }

    private MeasureRecord MapMeasure(MySqlDataReader reader)
    {
        string name = reader["thermometerName"] as string ?? "Unknown";
        string type = reader["type"] as string ?? "Unknown";
        DateTime date = reader.GetDateTime(reader.GetOrdinal("datetime"));

        double value = reader.GetDouble(reader.GetOrdinal("value"));
        double? expectedValue = reader["expectedValue"] as double?;
        string format = reader["format"] as string ?? "DefaultFormat";

        double difference = expectedValue.HasValue ? expectedValue.Value - value : 0.0;
        Measure measure = new Measure(value, difference, format);


        return new MeasureRecord(name, type, date, measure);
    }


}
