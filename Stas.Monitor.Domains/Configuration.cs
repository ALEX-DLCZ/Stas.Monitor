namespace Stas.Monitor.Domains;

public class Configuration
{
    private readonly IDictionary<string, IDictionary<string, string>> _configDico;

    public Configuration(IConfigurationReader reader)
    {
        _configDico = reader.GetReadedConfiguration();

        if (!_configDico.ContainsKey("general"))
        {
            throw new KeyNotFoundException("monitor: missing required section thermometers (general)");
        }

        if (!_configDico.ContainsKey("BD"))
        {
            throw new KeyNotFoundException("monitor: missing required section paths");
        }
    }

    public IDictionary<string, string> GetGeneral()
    {
        return _configDico["general"];
    }

    public IDictionary<string, string> GetBb()
    {
        return _configDico["BD"];
    }
}
