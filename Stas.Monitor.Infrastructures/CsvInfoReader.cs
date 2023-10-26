namespace Stas.Monitor.Infrastructures;

public class CsvInfoReader : IInfoStrategy
{
  public List<string> GetSoloLine(string line)
  {
    if ( IsEmpty(line) )
    {
      throw new Exception("line is empty");
    }

    return SplitLine(line);
  }


  private bool IsEmpty(string line)
  {
    return line.Equals(null) || line.Equals("");
  }

  private List<string> SplitLine(string line)
  {
    return line.Split(";").ToList();
  }
}