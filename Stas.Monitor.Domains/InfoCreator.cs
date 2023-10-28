namespace Stas.Monitor.Domains;

public class InfoCreator
{
  private IInfoReader _readerMesure;
  private IInfoReader _readerAlerte;

  public InfoCreator(IInfoReader readerMesure, IInfoReader readerAlerte)
  {
    _readerMesure = readerMesure;
    _readerAlerte = readerAlerte;
  }

  public List<IInfo> GetInfos()
  {
    var infos = new List<IInfo>();
    var info = _readerMesure.GetInfo();
    while ( info.Count > 0 )
    {
      infos.Add(CreateInfoMesure(info.Dequeue()));
    }

    info = _readerAlerte.GetInfo();
    while ( info.Count > 0 )
    {
      infos.Add(CreateInfoAlert(info.Dequeue()));
    }

    return infos;
  }

  public InfoMesure UpdateInfoMesure()
  {
    var info = _readerMesure.LastNewInfo();
    return CreateInfoMesure(info);
  }

  public InfoAlerte UpdateInfoAlerte()
  {
    var info = _readerAlerte.LastNewInfo();
    return CreateInfoAlert(info);
  }


  private InfoMesure CreateInfoMesure(List<string> info)
  {
    var nomThermometre = info[0];
    var dateHeureMesure = DateTime.Parse(info[1]);
    var typeMesure = info[2];
    var valeurMesure = double.Parse(info[3].Replace('.', ','));


    return new InfoMesure(nomThermometre, dateHeureMesure, typeMesure, valeurMesure);
  }

  private InfoAlerte CreateInfoAlert(List<string> info)
  {
    var nomThermometre = info[0];
    var dateHeureAlerte = DateTime.Parse(info[1]);
    var temperatureAttendue = double.Parse(info[2].Replace('.', ','));
    var ecartTemperature = double.Parse(info[3].Replace('.', ','));

    return new InfoAlerte(nomThermometre, dateHeureAlerte, temperatureAttendue, ecartTemperature);
  }
}