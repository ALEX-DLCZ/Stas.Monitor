using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public class MainInfoReader : IInfoReader
{
  private Queue<List<string>> _readedInfo;
  private string _pathArg;
  private IInfoStrategy _strategyType;

  public MainInfoReader(string pathArg)
  {
    _pathArg = pathArg;
    var fileType = _pathArg.Split(".").Last();

    IInfoStrategy strategyType;
    if ( fileType == "csv" )
    {
      _strategyType = new CsvInfoReader();
    }
    else
    {
      throw new FileNotFoundException("type of file is not supported");
    }

    _readedInfo = SetReadedInfo();
  }
  
  public Queue<List<string>> GetInfo()
  {
    return _readedInfo;
  }

  public List<string> LastNewInfo()
  {
    var line = File.ReadLines(_pathArg).First();
    return _strategyType.GetSoloLine(line);
  }

  private Queue<List<string>> SetReadedInfo( )
  {
    try
    {
      foreach ( var line in File.ReadLines(_pathArg) )
      {
        _strategyType.ReadLine(line);
      }
    }
    catch ( FileNotFoundException e )
    {
      throw new FileNotFoundException("File not found");
    }

    return _strategyType.GetInfo();
  }
}