using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

//crée une structure de FilterOption avec:
// private readonly string _thermoName;
// private readonly List<string> _SelectedTypes;
// private readonly double _time;
public class FilterOption : IFilterAccessor
{
    private string? _thermoName;
    private IList<string>? _selectedTypes;
    private double _time;
    private IList<IFilterSubscriber> _subscribers = new List<IFilterSubscriber>();

    //---------------Mutator----------------
    public void SetThermoName(string thermoName) => _thermoName = thermoName;

    public void SetSelectedTypes(IList<string> selectedTypes) => _selectedTypes = selectedTypes;

    public void SetTime(double time) => _time = time;

    //---------------Accessor----------------
    public string GetThermoName() => _thermoName ?? throw new ArgumentException("thermoName");

    public IList<string> GetSelectedTypes() => _selectedTypes ?? throw new ArgumentException("selectedTypes");

    public double GetTime() => _time;

    //-----------------Observer-----------------
    public void Subscribe(IFilterSubscriber filterSubscriber)
    {
        _subscribers.Add(filterSubscriber);
    }

    public void Unsubscribe(IFilterSubscriber filterSubscriber)
    {
        _subscribers.Remove(filterSubscriber);
    }

    public void FilterUpdate()
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Update(this);
        }
    }
}
