using System.Globalization;

namespace Stas.Monitor.Domains;

public class InfoAlerte : IInfo
{
  //. Elle est composée du nom du thermomètre, de la date et heure de l’alerte, de la température
  //attendue, de l’écart avec la température attendue ou de la température effectivement mesurée. 

  private readonly string _nomThermometre;
  private readonly DateTime _dateHeureAlerte;
  private readonly double _temperatureAttendue;
  private readonly double _ecartTemperature;

  public InfoAlerte(string nomThermometre, DateTime dateHeureAlerte, double temperatureAttendue,
    double ecartTemperature)
  {
    _nomThermometre = nomThermometre;
    _dateHeureAlerte = dateHeureAlerte;
    _temperatureAttendue = temperatureAttendue;
    _ecartTemperature = ecartTemperature;
  }

  public List<string> GetInfo()
  {
    return new List<string>()
    {
      _nomThermometre,
      _dateHeureAlerte.ToString(CultureInfo.CurrentCulture),
      _temperatureAttendue.ToString(CultureInfo.CurrentCulture),
      _ecartTemperature.ToString(CultureInfo.CurrentCulture)
    };
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
      _temperatureAttendue.ToString(CultureInfo.CurrentCulture), _dateHeureAlerte.ToString(CultureInfo.CurrentCulture), _ecartTemperature.ToString(CultureInfo.CurrentCulture)
    };
  }
}