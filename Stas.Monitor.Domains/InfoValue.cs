namespace Stas.Monitor.Domains;

public class InfoValue : IInfo
{
    private readonly DateTime _dateTime;
    private readonly IMeasure _measure;

    public InfoValue( DateTime dateTime, IMeasure measure )
    {
        _dateTime = dateTime;
        _measure = measure;
    }

    public bool IsAlerte() => _measure.IsAlerte();

    public DateTime GetDateTime() => _dateTime;
}
