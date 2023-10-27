namespace Stas.Monitor.Domains;

public class Configuration
{
  private readonly IDictionary<string, IDictionary<string, string>> _configDico;

  public Configuration(IConfigurationReader reader)
  {
    _configDico = reader.GetReadedConfiguration();
  }

  public Thermometer[] Thermometers
  {
    get
    {
      var thermometers = new List<Thermometer>();
      try
      {
        foreach ( var section in _configDico["general"] )
        {
          var thermometer = new Thermometer(section.Value);
          thermometers.Add(thermometer);
        }

      }
      catch ( KeyNotFoundException e )
      {
        throw new KeyNotFoundException("monitor: missing required section thermometers (general)");
      }
      return thermometers.ToArray();
    }
  }
}