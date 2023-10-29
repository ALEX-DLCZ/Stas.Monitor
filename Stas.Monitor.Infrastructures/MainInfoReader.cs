using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public class MainInfoReader : IInfoReader
{
  private readonly Queue<List<string>> _readedInfo;
  private readonly string _pathArg;
  private readonly IInfoStrategy _strategyType;

  public MainInfoReader(string pathArg)
  {
    _pathArg = pathArg;
    _readedInfo = new Queue<List<string>>();
    _strategyType = new CsvInfoReader();
    var fileType = _pathArg.Split(".").Last();

    if ( fileType == "csv" )
    {
    }
    else
    {
      throw new FileNotFoundException("Monitor : type of file is not supported");
    }

    SetReadedInfo();
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

  private void SetReadedInfo()
  {
    try
    {
      var selectedTime = DateTime.Parse(LastNewInfo()[1]);
      selectedTime = selectedTime.AddMinutes(-1);


      foreach ( var line in File.ReadLines(_pathArg) )
      {
        var tempList = _strategyType.GetSoloLine(line);
        if ( DateTime.Parse(tempList[1]) < selectedTime )
        {
          break;
        }

        _readedInfo.Enqueue(tempList);
      }
    }
    catch ( FileNotFoundException e )
    {
      throw new FileNotFoundException("File not found" + e.Message);
    }
  }
}