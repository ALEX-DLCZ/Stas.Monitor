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


/*
 * package stas.thermometer.infrastructures;

import stas.thermometer.domains.ConfigurationReader;
import stas.thermometer.infrastructures.personal.exceptions.FileNotFoundException;
import stas.thermometer.infrastructures.personal.exceptions.unknownArgumentException;

import java.util.Map;

/**
 *@implNote classe qui implémente l'interface ConfigurationReader
 * <p>
 * inspirée du design pattern Strategy en temps que "Context"
 * /
public class MainConfigurationReader implements ConfigurationReader {

    private final Map<String, Map<String, String>> configurationMap;

    public MainConfigurationReader(String pathArgs) throws unknownArgumentException, FileNotFoundException {

        String fileExtension;

        if ( pathArgs.contains(".") ) {
            fileExtension = pathArgs.substring(pathArgs.lastIndexOf(".") + 1);
        }
        else {
            throw new unknownArgumentException();
        }
        ConfigurationStrategy configurationStrategy;

        if ( "ini".equals(fileExtension) ) {
            configurationStrategy = new IniConfigurationReader();
        }
        else {
            throw new unknownArgumentException();
        }

        configurationMap = configurationStrategy.executeConfigurationReaderStrategy(pathArgs);
    }

    @Override
    public Map<String, Map<String, String>> getReadedConfiguration() {
        return configurationMap;
    }
}

 */





