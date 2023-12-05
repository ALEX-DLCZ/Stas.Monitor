using System.Collections.Generic;
using System.IO;
using Stas.Monitor.App.PersonalExceptions;
using Stas.Monitor.Domains;
using Stas.Monitor.Infrastructures;
using Stas.Monitor.Infrastructures.PersonalExceptions;

namespace Stas.Monitor.App;

public class ArgsExecutor
{
    private readonly string _connectionString;
    private readonly IDictionary<string, string> _thermomterName;

    public ArgsExecutor(string[] args)
    {
        MainConfigurationReader configurationStrategy;
        try
        {
            if (args[0].Equals("--config-file"))
            {
                configurationStrategy = new MainConfigurationReader(args[1]);
            }
            else
            {
                throw new System.Exception();
            }
        }
        catch (UnknownArgumentException e)
        {
            throw new FatalException(e.Message);
        }
        catch (FileNotFoundException e)
        {
            throw new FatalException(e.Message);
        }
        catch (System.IndexOutOfRangeException)
        {
            throw new FatalException("missing configuration file argument");
        }
        catch (System.Exception)
        {
            throw new FatalException("Unknown argument");
        }

        Configuration configuration = new Configuration(configurationStrategy);
        _thermomterName = configuration.GetGeneral();
        IDictionary<string, string> db = configuration.GetBb();
        _connectionString = $"jdbc:mysql://{db["IpServer"]}:{db["PortServer"]}/{db["User"]}?user={db["User"]}&password={db["Pws"]}";
    }

    public string GetConnectionString() => _connectionString;

    public IDictionary<string, string> GetThermoName () => _thermomterName;
}
