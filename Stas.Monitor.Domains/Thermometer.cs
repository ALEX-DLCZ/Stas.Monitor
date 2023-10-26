namespace Stas.Monitor.Domains;

public class Thermometer
{
  private string _name;
  
  public Thermometer(string name)
  {
    _name = name;
  }
  
  public string Name => _name;
}