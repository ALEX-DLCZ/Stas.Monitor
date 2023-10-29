namespace Stas.Monitor.Domains;

public interface IInfo
{
  List<string> GetInfo();

  bool IsAlerte();
  bool IsCorrectThermo(string thermoName);

  string[] GetInfoForView();
}