namespace Stas.Monitor.Infrastructures;

public interface IConfigurationStrategy
{
    IDictionary<string, IDictionary<string, string>> ExecuteConfigurationStrategy(string path);
}
