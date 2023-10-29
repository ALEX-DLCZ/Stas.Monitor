namespace Stas.Monitor.Domains;

public class InfoMesure : IInfo
{
  //Elle est composée du nom du thermomètre, de la date et heure de la mesure, du type de la mesure et de la valeur mesurée. 

  private string _nomThermometre;
  private DateTime _dateHeureMesure;
  private string _typeMesure;
  private double _valeurMesure;

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
      _dateHeureMesure.ToString(),
      _typeMesure,
      _valeurMesure.ToString()
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
    return new string[] {_valeurMesure.ToString(), _dateHeureMesure.ToString()};
  }
}