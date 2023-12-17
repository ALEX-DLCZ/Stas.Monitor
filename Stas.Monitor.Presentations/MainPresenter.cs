using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public class MainPresenter
{
    private readonly IMainView _view;
    private readonly IThermometerRepository _repository;
    private FilterEventArgs? _args;

    public MainPresenter(IMainView view, IThermometerRepository repository)
    {
        _view = view;
        _repository = repository;
    }

    public void Start()
    {
        _view.FilterChanged += OnQueryChanged;

        _view.ThermometersNames = _repository.AllThermometers;

        _view.Types = _repository
            .NewRequest()
            .SelectDistinct("type");
    }

    public void OnQueryChanged(object? sender, FilterEventArgs? args)
    {
        _args = args;

        var request = _repository
            .NewRequest();

        _view.Result = request.Where("thermometerName", val => $"LIKE '%{val}%'", args?.ThermometerTarget)
            .Where("type", val => $"IN ('{val}')", string.Join("','", args?.Types ?? Array.Empty<string>()))
            .Where("datetime", val => $">= (SELECT MAX(datetime) FROM Mesures) - INTERVAL {val} SECOND", args!.TimeSelected)
            .Select(mesure => new MeasurePresenterModel(mesure)).ToList();
    }

    public void Update()
    {
        var request = _repository
            .NewRequest();

        _view.UpdateResult = request.Where("thermometerName", val => $"LIKE '%{val}%'", _args?.ThermometerTarget)
            .Where("type", val => $"IN ('{val}')", string.Join("','", _args?.Types ?? Array.Empty<string>()))
            .WhereUpdate()
            .Select(mesure => new MeasurePresenterModel(mesure)).ToList();
    }
}
