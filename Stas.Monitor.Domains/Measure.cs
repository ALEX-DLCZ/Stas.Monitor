namespace Stas.Monitor.Domains;

public class Measure : IMeasure
{
    private readonly double _value;
    private readonly double _valueExpected;
    private readonly string _format;

    public Measure( double value, double valueExpected, string format )
    {
        _value = value;
        _valueExpected = valueExpected;
        _format = format;
    }

    public bool IsAlerte() => _value > _valueExpected;

    public string ValueToString() => _value.ToString(_format);

    public string ValueExpectedToString() => _valueExpected.ToString(_format);
}
