namespace Stas.Monitor.Domains;

public interface IConfigurationReader
{
  IDictionary<string, IDictionary<string, string>> GetReadedConfiguration();
    
}