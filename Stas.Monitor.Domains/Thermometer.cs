namespace Stas.Monitor.Domains;

public class Thermometer
{
  public Thermometer(string name)
  {
    Name = name;
  }

  private string Name { get; }

  public override string ToString()
  {
    return Name;
  }
}