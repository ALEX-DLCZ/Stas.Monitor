namespace Stas.Monitor.Domains;

public class Configuration
{
    
  private IConfigurationReader _configurationReader;
  
  
  public Configuration(IConfigurationReader configurationReader)
  {
    _configurationReader = configurationReader;
  }
  
  public void ReadConfiguration()
  {
    var configuration = _configurationReader.GetReadedConfiguration();
    foreach(var section in configuration)
    {
      Console.WriteLine($"Section: {section.Key}");
      foreach(var key in section.Value)
      {
        Console.WriteLine($"Key: {key.Key} Value: {key.Value}");
      }
    }
  }
}