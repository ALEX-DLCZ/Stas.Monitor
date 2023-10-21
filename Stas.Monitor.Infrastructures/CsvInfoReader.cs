namespace Stas.Monitor.Infrastructures;

public class CsvInfoReader: IInfoStrategy
{
  private readonly Queue<List<string>> _infoQueue;

  public CsvInfoReader()
  {
    _infoQueue = new Queue<List<string>>();
  }

  public void ReadLine(string line)
  {
    if ( IsEmpty(line) )
    {
      return;
    }
    _infoQueue.Enqueue(SplitLine(line));
    
  }

  public Queue<List<string>> GetInfo()
  {
    return _infoQueue;
  }
  
  
  private bool IsEmpty(string line)
  {
    return line.Equals(null)||line.Equals("");
  }
  
  private List<string> SplitLine(string line)
  {
    return line.Split(";").ToList();
  }
  
  
  
}