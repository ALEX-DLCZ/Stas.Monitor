namespace Stas.Monitor.Domains;

public interface IThermometerRepository
{
    string[] AllThermometers { get; }

    IThermometer  FindThermometer(string thermoName);
}
