namespace Stas.Monitor.Domains;

public interface IInfoReader
{
  Queue<List<string>> GetInfo();

  List<string> LastNewInfo();
}