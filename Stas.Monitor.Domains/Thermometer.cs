namespace Stas.Monitor.Domains;

public class Thermometer : IThermometer
{
    private readonly string _thermometerName;
    private readonly IList<IType> _types;

    public Thermometer(string thermometerName)
    {
        _thermometerName = thermometerName;
        _types = new List<IType>();
    }

    public string Name => _thermometerName;

    public IList<IType> Types => _types;
}
