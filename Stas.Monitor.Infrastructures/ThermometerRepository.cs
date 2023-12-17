using Stas.Monitor.Domains;
using Stas.Monitor.Infrastructures.DataBase;

namespace Stas.Monitor.Infrastructures;

public class ThermometerRepository : IThermometerRepository
{
    private readonly IList<Thermometer> _thermometers;
    private readonly IDialoger _dialoger;

    public ThermometerRepository(IDictionary<string, string> thermometersName, IDialoger dialoger)
    {
        _dialoger = dialoger;
        _thermometers = thermometersName.Select(t => new Thermometer(t.Value)).ToList();
    }

    public string[] AllThermometers => _thermometers.Select(t => t.Name).ToArray();

    public IRequest NewRequest() => new DbRequest(_dialoger);
}
