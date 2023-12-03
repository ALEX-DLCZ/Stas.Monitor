using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public class ThermometerRepository : IThermometerRepository
{
    private readonly IList<Thermometer> _thermometers;

    public ThermometerRepository(IDictionary<string, string> thermometersName)
    {
        _thermometers = new List<Thermometer>();

        _thermometers = thermometersName.Select(t => new Thermometer(t.Value)).ToList();
    }

    public IList<Thermometer> GetThermometers() => _thermometers;

    public string[] AllThermometers => _thermometers.Select(t => t.Name).ToArray();

    public IThermometer FindThermometer(string thermoName)
    {
        return _thermometers.FirstOrDefault(t => t.Name == thermoName) ?? throw new Exception("Thermometer not found");
    }
}
