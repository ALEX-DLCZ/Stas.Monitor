using System.Globalization;

namespace Stas.Monitor.Domains;

public class InfoMesure : IInfo
{
  //Elle est composée du nom du thermomètre, de la date et heure de la mesure, du type de la mesure et de la valeur mesurée. 

  private readonly string _nomThermometre;
  private readonly DateTime _dateHeureMesure;
  private readonly string _typeMesure;
  private readonly double _valeurMesure;

  public InfoMesure(string nomThermometre, DateTime dateHeureMesure, string typeMesure,
    double valeurMesure)
  {
    _nomThermometre = nomThermometre;
    _dateHeureMesure = dateHeureMesure;
    _typeMesure = typeMesure;
    _valeurMesure = valeurMesure;
  }

  public List<string> GetInfo()
  {
    return new List<string>()
    {
      _nomThermometre, 
      _dateHeureMesure.ToString(CultureInfo.CurrentCulture),
      _typeMesure,
      _valeurMesure.ToString(CultureInfo.CurrentCulture)
    };
  }
  
  public bool IsAlerte()
  {
    return false;
  }
  
  public bool IsCorrectThermo(string thermoName)
  {
    return _nomThermometre == thermoName;
  }
  public string[] GetInfoForView()
  {
    return new[] {_valeurMesure.ToString(CultureInfo.CurrentCulture), _dateHeureMesure.ToString(CultureInfo.CurrentCulture)};
  }
}