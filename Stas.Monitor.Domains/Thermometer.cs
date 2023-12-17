namespace Stas.Monitor.Domains;

public class Thermometer : IThermometer
{
    private readonly string _thermometerName;

    public Thermometer(string thermometerName)
    {
        _thermometerName = thermometerName;
    }

    public string Name => _thermometerName;
}
