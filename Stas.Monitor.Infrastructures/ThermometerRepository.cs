using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public class ThermometerRepository : IThermometerRepository
{
  private readonly Thermometer[] _thermometers;
  private readonly InfoCreator _infoCreator;
  private LinkedList<IInfo> _allInfos;
  

  public ThermometerRepository(IConfigurationReader reader)
  {
    Configuration config = new Configuration(reader);
    _thermometers = config.Thermometers;
    
    var paths = config.GetPaths();
    _infoCreator = new InfoCreator(new MainInfoReader(paths[0]), new MainInfoReader(paths[1]));
    _allInfos = _infoCreator.GetInfos();
  }
  
  public string[] AllThermometers => _thermometers.Select(t => t.ToString()).ToArray();

  public LinkedList<IInfo> AllInfos => _allInfos;
  
  public void UpdateInfos()
  {
    _allInfos.AddFirst(_infoCreator.UpdateInfoMesure());
    _allInfos.AddFirst(_infoCreator.UpdateInfoAlerte());
  }
  
}

