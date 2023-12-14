using Stas.Monitor.Domains;
using Stas.Monitor.Infrastructures.DataBase;

namespace Stas.Monitor.Infrastructures;

public class ThermometerRepository : IThermometerRepository
{
    private readonly IList<Thermometer> _thermometers;
    private readonly string _connectionString;

    public ThermometerRepository(IDictionary<string, string> thermometersName, string connectionString)
    {
        _connectionString = connectionString;
        _thermometers = thermometersName.Select(t => new Thermometer(t.Value)).ToList();
    }

    public string[] AllThermometers => _thermometers.Select(t => t.Name).ToArray();

    public IRequest NewRequest()
    {
        return new DBRequest(_connectionString);
    }


}
