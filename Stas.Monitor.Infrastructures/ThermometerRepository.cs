using Stas.Monitor.Domains;
using Stas.Monitor.Infrastructures.DataBase;

namespace Stas.Monitor.Infrastructures;

public class ThermometerRepository : IThermometerRepository
{
    private readonly IDbQuery _dialoger;

    public ThermometerRepository( IDbQuery dialoger)
    {
        _dialoger = dialoger;
    }

    // public IRequest NewRequest() => new DbRequest(_dialoger);

    public IQueryManager NewQueryManager() => new MainQueryManager(_dialoger);
}







// public class ThermometerRepository : IThermometerRepository
// {
//     private readonly IList<Thermometer> _thermometers;
//     private readonly IDialoger _dialoger;
//
//     public ThermometerRepository(IDictionary<string, string> thermometersName, IDialoger dialoger)
//     {
//         _dialoger = dialoger;
//         _thermometers = thermometersName.Select(t => new Thermometer(t.Value)).ToList();
//     }
//
//     public string[] AllThermometers => _thermometers.Select(t => t.Name).ToArray();
//
//     public IRequest NewRequest() => new DbRequest(_dialoger);
// }
