using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public class MainInfoReader : IInfoReader
{
  private Queue<List<string>> _readedInfo;

  public MainInfoReader(string pathArg)
  {
    var fileType = pathArg.Split(".").Last();

    IInfoStrategy strategyType;
    if ( fileType == "csv" )
    {
      strategyType = new CsvInfoReader();
    }
    else
    {
      throw new FileNotFoundException("type of file is not supported");
    }

    _readedInfo = SetReadedInfo(pathArg, strategyType);
  }
  
  public Queue<List<string>> GetInfo()
  {
    return _readedInfo;
  }
  
  private Queue<List<string>> SetReadedInfo(string pathArg, IInfoStrategy strategyType)
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

    return strategyType.GetInfo();
  }
}