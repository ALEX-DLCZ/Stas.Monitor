using System.Globalization;

namespace Stas.Monitor.Domains;

public class InfoAlerte : IInfo
{
    private readonly string _nomThermometre;
    private readonly string _typeMesure;
    private readonly DateTime _dateHeureAlerte;
    private readonly double _temperatureAttendue;
    private readonly double _ecartTemperature;

    public InfoAlerte(string nomThermometre, string typeMesure, DateTime dateHeureAlerte, double temperatureAttendue,
        double ecartTemperature)
    {
        _nomThermometre = nomThermometre;
        _typeMesure = typeMesure;
        _dateHeureAlerte = dateHeureAlerte;
        _temperatureAttendue = temperatureAttendue;
        _ecartTemperature = ecartTemperature;
    }

    public bool IsAlerte()
    {
        return true;
    }

    public bool IsCorrectThermo(string thermoName)
    {
        return _nomThermometre == thermoName;
    }

    public string[] GetInfoForView()
    {
        return new[]
        {
            _temperatureAttendue.ToString(CultureInfo.CurrentCulture), _dateHeureAlerte.ToString(CultureInfo.CurrentCulture), _ecartTemperature.ToString(CultureInfo.CurrentCulture),
        };
    }
}
