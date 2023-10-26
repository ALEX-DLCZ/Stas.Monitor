using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public class MainPresenter
{
  private readonly IMainView _view;
  private readonly IThermometerRepository _repository;


  public MainPresenter(IMainView view, IThermometerRepository repository)
  {
    _view = view;
    _repository = repository;
  }

  public void Start()
  {
    _view.ThermometerNames = _repository.AllThermometers;
  }
}