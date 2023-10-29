namespace Stas.Monitor.Domains;

public class Thermometer
{
  private string _name;

  public Thermometer(string name)
  {
    _name = name;
  }

  private string Name => _name;

  public override string ToString()
  {
    return Name;
  }
}