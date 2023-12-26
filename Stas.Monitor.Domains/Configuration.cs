namespace Stas.Monitor.Domains;

public class Configuration
{
    private readonly IDictionary<string, IDictionary<string, string>> _configDico;

    public Configuration(IConfigurationReader reader)
    {
        _configDico = reader.GetReadedConfiguration();

        if (!_configDico.ContainsKey("general") || _configDico["general"].Count == 0)
        {
            throw new KeyNotFoundException("monitor: missing required section general (general)");
        }
        if (!_configDico.ContainsKey("BD"))
        {
            throw new KeyNotFoundException("monitor: missing required section paths");
        }
    }

    public IDictionary<string, string> GetGeneral() => _configDico["general"];

    public IDictionary<string, string> GetBb() => _configDico["BD"];
}
