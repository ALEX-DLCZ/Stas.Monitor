namespace Stas.Monitor.Infrastructures;

public interface IConfigurationStrategy
{
  //FileInfo GetFileInfo(string pathArg);

  void ReadLine(string line);

  IDictionary<string, IDictionary<string, string>> GetSectionMaps();
}