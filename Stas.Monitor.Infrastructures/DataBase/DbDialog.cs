namespace Stas.Monitor.Infrastructures.DataBase;
using Domains;

using MySql.Data.MySqlClient;
public class DbDialog
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

    //séléctionne tout les mesure avec la valeur expected_value si la mesure a une alerte
    public IEnumerable<IDictionary<string, string>> allValeur()
    {
        using MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Mesures INNER JOIN Alerts ON Mesures.id = Alerts.idMesure";
        using MySqlDataReader reader = command.ExecuteReader();
        List<IDictionary<string, string>> mesures = new List<IDictionary<string, string>>();
        while (reader.Read())
        {
            mesures.Add(new Dictionary<string, string>()
            {
                {"id", reader.GetString(0)},
                {"thermometerName", reader.GetString(1)},
                {"datetime", reader.GetString(2)},
                {"type", reader.GetString(3)},
                {"format", reader.GetString(4)},
                {"value", reader.GetString(5)},
                {"expectedValue", reader.GetString(7)}
            });
        }
        return mesures;
    }

    public IEnumerable<MeasureRecord> allValeurGPT()
    {
        using MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT Mesures.*, Alerts.expectedValue " +
                              "FROM Mesures " +
                              "LEFT JOIN Alerts ON Mesures.id = Alerts.idMesure " +
                              "WHERE Mesures.id = 479";
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

        double? difference = expectedValue.HasValue ? value - expectedValue.Value : null;
        Measure measure = new Measure(value, difference ?? 0.0, format);

        return new MeasureRecord(name, type, date, measure);
    }
}
