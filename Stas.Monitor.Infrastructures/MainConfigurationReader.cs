using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public class MainConfigurationReader : IConfigurationReader
{
  private IDictionary<string, IDictionary<string, string>> _readedConfiguration;

  public MainConfigurationReader(string pathArg)
  {
    var fileType = pathArg.Split(".").Last();

    IConfigurationStrategy strategyType;
    if ( fileType == "ini" )
    {
      strategyType = new IniConfigurationReader();
    }
    else
    {
      throw new FileNotFoundException("type of file is not supported");
    }

    _readedConfiguration = SetReadedConfiguration(pathArg, strategyType);
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
      throw new FileNotFoundException("File not found");
    }


    return strategyType.GetSectionMaps();
  }
}