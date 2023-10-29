namespace Stas.Monitor.Domains;

public interface IThermometerRepository
{
  string[] AllThermometers { get; }


  public LinkedList<IInfo> AllInfos(int thermometerId);
}