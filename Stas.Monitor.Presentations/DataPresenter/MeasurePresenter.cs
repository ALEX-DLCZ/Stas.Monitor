namespace Stas.Monitor.Presentations.DataPresenter;

public class MeasurePresenter
{
    private readonly IEnumerable<string[]> _infosThermometer;

    public MeasurePresenter()
    {
        _infosThermometer = new LinkedList<string[]>();
    }

    public IEnumerable<string[]> InfosThermometer => _infosThermometer;

    public void AddInfo(string[] info)
    {
        _infosThermometer.Append(info);
    }
}
