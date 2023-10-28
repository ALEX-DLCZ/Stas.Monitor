namespace Stas.Monitor.Domains;

public interface IThermometerRepository
{
  string[] AllThermometers { get; }

  LinkedList<IInfo> AllInfos { get; }
}