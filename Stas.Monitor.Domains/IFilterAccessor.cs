namespace Stas.Monitor.Domains;

public interface IFilterAccessor
{
    string GetThermoName();

    IList<string> GetSelectedTypes();

    double GetTime();
}
