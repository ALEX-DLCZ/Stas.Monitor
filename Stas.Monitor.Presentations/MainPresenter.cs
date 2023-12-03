using Stas.Monitor.Domains;
using Stas.Monitor.Presentations.DataPresenter;

namespace Stas.Monitor.Presentations;

public class MainPresenter
{
    private readonly IMainView _view;
    private readonly IThermometerRepository _repository;
    private readonly FilterOption _filterOption;

    public MainPresenter(IMainView view, IThermometerRepository repository)
    {
        _view = view ?? throw new ArgumentException("view");

        _view.SetPresenter(this);
        _repository = repository ?? throw new ArgumentException("repository");
        _filterOption = new FilterOption();
    }

    public void Start()
    {
        _view.ThermometerNames = _repository.AllThermometers;
    }



    public void Update()
    {
        IThermometer thermometer = _repository.FindThermometer(_filterOption.GetThermoName());
    }
}
