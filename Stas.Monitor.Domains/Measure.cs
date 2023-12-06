namespace Stas.Monitor.Domains;

public class Measure
{
    private readonly double _value;
    private readonly double _difference;
    private readonly string _format;

    public Measure( double value, double difference, string format )
    {
        _value = value;
        _difference = difference;
        _format = format;
    }

    // public string ValueToString() => _value.ToString(_format);
    //
    // public string ValueExpectedToString() => _valueExpected.ToString(_format);

    public double Value => _value;

    public double Difference => _difference;

    public string Format => _format;
}
