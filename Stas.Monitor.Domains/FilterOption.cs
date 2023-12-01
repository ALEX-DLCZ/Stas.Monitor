namespace Stas.Monitor.Domains;

//crée une structure de FilterOption avec:
// private readonly string _thermoName;
// private readonly List<string> _SelectedTypes;
// private readonly double _time;
public class FilterOption
{
    private readonly string _thermoName;
    private readonly IList<string> _selectedTypes;
    private readonly double _time;

    public FilterOption(string thermoName, List<string> selectedTypes, double time)
    {
        _thermoName = thermoName;
        _selectedTypes = selectedTypes;
        _time = time;
    }

    public string GetThermoName() => _thermoName;

    public IList<string> GetSelectedTypes() => _selectedTypes;

    public double GetTime() => _time;
}
