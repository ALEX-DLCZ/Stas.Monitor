using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public class ThermometerRepository : IThermometerRepository
{
  private readonly Thermometer[] _thermometers;
  public ThermometerRepository(IConfigurationReader reader )
  {
    Configuration config = new Configuration(reader);
    _thermometers = config.Thermometers;
  }

  // public string[] AllThermometers { get; }
  
  public string[] AllThermometers => _thermometers.Select(t => t.Name).ToArray();
  
}