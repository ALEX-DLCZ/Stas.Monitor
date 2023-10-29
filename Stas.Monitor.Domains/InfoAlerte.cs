namespace Stas.Monitor.Domains;

public class InfoAlerte : IInfo
{
  //. Elle est composée du nom du thermomètre, de la date et heure de l’alerte, de la température
  //attendue, de l’écart avec la température attendue ou de la température effectivement mesurée. 

  private string _nomThermometre;
  private DateTime _dateHeureAlerte;
  private double _temperatureAttendue;
  private double _ecartTemperature;

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
      _dateHeureAlerte.ToString(),
      _temperatureAttendue.ToString(),
      _ecartTemperature.ToString()
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
}