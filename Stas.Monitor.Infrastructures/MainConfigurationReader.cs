using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;



public class MainConfigurationReader : IConfigurationReader
{
  private readonly IDictionary<string, IDictionary<string, string>> _readedConfiguration;

  public MainConfigurationReader(string pathArg)
  {
    try
    {
      var fileType = pathArg.Split(".").Last();

      IConfigurationStrategy strategyType;
      if ( fileType == "ini" )
      {
        strategyType = new IniConfigurationReader();
      }
      else
      {
        throw new Exception("type of file is not supported");
      }
      
      _readedConfiguration = SetReadedConfiguration(pathArg, strategyType);
    }
    catch ( Exception e )
    {
      Console.WriteLine(e);
      throw;
    }
  }


  public IDictionary<string, IDictionary<string, string>> GetReadedConfiguration()
  {
    return _readedConfiguration;
  }
  private IDictionary<string, IDictionary<string, string>> SetReadedConfiguration(string pathArg, IConfigurationStrategy strategyType)
  {
    //var fileInfo = strategyType.GetFileInfo(pathArg);
    
    try
    {
      foreach (var line in File.ReadLines(pathArg))
      {
        strategyType.ReadLine(line);
      }
    }
    catch (IOException e)
    {
      Console.WriteLine(e.ToString());
    }
    
    
    
    return _readedConfiguration;
  }
  
}