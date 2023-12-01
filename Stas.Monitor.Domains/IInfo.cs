namespace Stas.Monitor.Domains;

public interface IInfo
{

    bool IsAlerte();

    bool IsCorrectThermo(string thermoName);

    string[] GetInfoForView();
}
