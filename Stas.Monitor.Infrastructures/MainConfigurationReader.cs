using Stas.Monitor.Domains;
using Stas.Monitor.Infrastructures.PersonalExceptions;

namespace Stas.Monitor.Infrastructures;

public class MainConfigurationReader : IConfigurationReader
{
    private readonly IDictionary<string, IDictionary<string, string>> _configurationMap;

    public MainConfigurationReader(string pathArgs)
    {
        var fileExtension = Path.GetExtension(pathArgs);

        var configurationStrategy = fileExtension switch
        {
            ".ini" => new IniConfigurationReader(),
            _ => throw new UnknownArgumentException("Unknown file extension")
        };

        _configurationMap = configurationStrategy.ExecuteConfigurationStrategy(pathArgs);
    }

    public IDictionary<string, IDictionary<string, string>> GetReadedConfiguration() => _configurationMap;
}
