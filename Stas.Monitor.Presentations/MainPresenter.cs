using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public class MainPresenter
{
  private readonly IMainView _view;
  private readonly IThermometerRepository _repository;


  public MainPresenter(IMainView view, IThermometerRepository repository)
  {
    _view = view ?? throw new ArgumentException("view");
    _view.SetPresenter(this);
    _repository = repository ?? throw new ArgumentException("repository");
  }

  public void Start()
  {
    _view.ThermometerNames = _repository.AllThermometers;
  }
  
  public void SelectedThermometer(int thermometerName)
  {
    var fsf = _repository.AllInfos;
  }
  
  public void ThermometerSelected(int thermometerName)
  {
    Console.WriteLine(thermometerName);
  }
}