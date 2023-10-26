namespace Stas.Monitor.Infrastructures;

public interface IInfoStrategy
{
  List<string> GetSoloLine(string line);
}