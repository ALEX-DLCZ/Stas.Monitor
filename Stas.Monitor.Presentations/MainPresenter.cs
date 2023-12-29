using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public class MainPresenter
{
    private readonly IMainView _view;
    private readonly IThermometerRepository _repository;
    private FilterEventArgs? _args;

    public MainPresenter(IMainView view, IThermometerRepository repository, IDictionary<string, string> thermometersName)
    {
        _view = view;
        _view.ThermometersNames = thermometersName.Select(t => t.Value);

        _repository = repository;
    }

    public void Start()
    {
        _view.FilterChanged += OnQueryChanged;
        var queryManager = _repository.NewQueryManager();
        queryManager.ConstructQuery().SelectDistinct("type");
        var building = queryManager.Building(queryManager.ConstructQuery());

        _view.Types = queryManager.ExecuteSimple(building);




        // _view.Types = _repository
        //     .NewRequest()
        //     .SelectDistinct("type");
    }

    public void OnQueryChanged(object? sender, FilterEventArgs? args)
    {
        _args = args;

        var complexRequest = _repository.NewQueryManager();
        var complexconstructor = complexRequest.ConstructQuery();
        var complexcondition = complexconstructor
            .SelectSpecific(args!.TimeSelected)
            .Equal("thermometerName", args!.ThermometerTarget)
            .In("type", args?.Types ?? Array.Empty<string>())
            .UpperThan("datetime", args!.TimeSelected);
        complexconstructor.Conditions(complexcondition);
        var complexbuilding = complexRequest.Building(complexRequest.ConstructQuery());


        var queryManager = _repository.NewQueryManager();
        var constructor = queryManager.ConstructQuery();
        var condition = constructor
            .SelectAll()
            .Equal("thermometerName", args!.ThermometerTarget)
            .In("type", args?.Types ?? Array.Empty<string>())
            .UpperThan("datetime", args!.TimeSelected)
            .UpperOtherBuild("datetime", complexbuilding);
        constructor.Conditions(condition);

        var building = queryManager.Building(queryManager.ConstructQuery());
        Console.WriteLine(building.Query);
        foreach (var (key, value) in building.ParameterMaps)
        {
            Console.WriteLine($"{key} : {value}");
        }
        _view.Result = queryManager.ExecuteMesure(building).Select(mesure => new MeasurePresenterModel(mesure)).ToList();



        // var query = _repository.NewQueryManager();
        // var constructor = query.ConstructQuery();
        // var condition = constructor
        //     .SelectAll()
        //     .Equal("thermometerName", args!.ThermometerTarget);
        // constructor.Conditions(condition);
        // var building = query.Building(query.ConstructQuery());
        // Console.WriteLine(building.Query);
        // foreach (var (key, value) in building.ParameterMaps)
        // {
        //     Console.WriteLine($"{key} : {value}");
        // }
        // _view.Result =  query.ExecuteMesure(building).Select(mesure => new MeasurePresenterModel(mesure)).ToList();



        // var complexRequest = $"(SELECT MAX(datetime) FROM Mesures WHERE thermometerName = '{args?.ThermometerTarget}') - INTERVAL {args!.TimeSelected} SECOND";
        // _view.Result = request
        //     .Where("thermometerName", val => $"LIKE '%{val}%'", args?.ThermometerTarget)
        //     .Where("type", val => $"IN ('{val}')", string.Join("','", args?.Types ?? Array.Empty<string>()))
        //     .Where("datetime", val => $">=  {val} ", complexRequest)
        //     .Select(mesure => new MeasurePresenterModel(mesure)).ToList();
    }

    public void Update()
    {
        var request = _repository
            .NewQueryManager();

        // _view.UpdateResult = request.Where("thermometerName", val => $"LIKE '%{val}%'", _args?.ThermometerTarget)
        //     .Where("type", val => $"IN ('{val}')", string.Join("','", _args?.Types ?? Array.Empty<string>()))
        //     .WhereUpdate()
        //     .Select(mesure => new MeasurePresenterModel(mesure)).ToList();
    }
}
