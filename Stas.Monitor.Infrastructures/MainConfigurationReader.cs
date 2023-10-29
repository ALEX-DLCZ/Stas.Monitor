using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public class MainConfigurationReader : IConfigurationReader
{
  private IDictionary<string, IDictionary<string, string>> _readedConfiguration;

  public MainConfigurationReader(string[] pathArg)
  {
    string fileType = "";
    try
    {
      fileType = pathArg[1].Split(".").Last();
    }
    catch ( IndexOutOfRangeException e )
    {
      throw new IndexOutOfRangeException("monitor: missing configuration file argument");
    }

    IConfigurationStrategy strategyType;
    if ( fileType == "ini" )
    {
      strategyType = new IniConfigurationReader();
    }
    else
    {
      throw new FileNotFoundException("monitor: type of file is not supported");
    }

    _readedConfiguration = SetReadedConfiguration(pathArg[1], strategyType);
  }


  public IDictionary<string, IDictionary<string, string>> GetReadedConfiguration()
  {
    return _readedConfiguration;
  }

  private IDictionary<string, IDictionary<string, string>> SetReadedConfiguration(string pathArg,
    IConfigurationStrategy strategyType)
  {
    try
    {
      foreach ( var line in File.ReadLines(pathArg) )
      {
        strategyType.ReadLine(line);
      }
    }
    catch ( FileNotFoundException e )
    {
      throw new FileNotFoundException("monitor: configuration file not found");
    }


    return strategyType.GetSectionMaps();
  }
}