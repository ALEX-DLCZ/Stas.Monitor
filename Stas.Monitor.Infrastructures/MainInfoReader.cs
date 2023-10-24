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
    _readedInfo = new Queue<List<string>>();
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
        var temp = _strategyType.GetSoloLine(line);
        //vérifie si le temps n'est pas dépassé sinon on quitte la boucle
        if ( DateTime.Parse(temp[1]) < selectedTime )
        {
          break;
        }

        _readedInfo.Enqueue(_strategyType.GetSoloLine(line));
      }
    }
    catch ( FileNotFoundException e )
    {
      throw new FileNotFoundException("File not found");
    }
  }
}