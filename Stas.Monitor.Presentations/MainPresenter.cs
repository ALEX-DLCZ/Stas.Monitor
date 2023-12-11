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
        _view.FilterChanged += OnQueryChanged;

        // _view.ThermometersNames = new []{"thermo1", "thermo2", "thermo3"};
        _view.ThermometersNames = _repository.AllThermometers;

        _view.Types = new []{"type1", "type2", "type3"};


        // _view.Types = _repository
        //     .NewQuery()
        //     .SelectDistinct(p => p.Type1);
    }

    private void OnQueryChanged(object? sender, FilterEventArgs args)
    {
        var typesAsSet = new HashSet<string>(args.Types);

        // Console.WriteLine( args.ThermometerTarget );
        // foreach (var type in args.Types)
        // {
        //     Console.WriteLine( type );
        // }
        // Console.WriteLine( args.TimeSelected );


        // var query =  _repository
        //     .NewQuery();
        // if (args.OnlyLegendary)
        // {
        //     query = query.Where(p => p.Legendary);
        // }
        // _view.Result = query.Where(p => p.Name.Contains(args.Contains, StringComparison.OrdinalIgnoreCase))
        //     .Where(p => p.Generation >= args.Generation)
        //     .Where(p => typesAsSet.Contains(p.Type1) || typesAsSet.Contains(p.Type2))
        //     .Select(pokemon => new MeasurePresenterModel(pokemon))
        //     .ToList();
        // where est une expression lambda un peut complex pour l'ai ca
    }











    /*
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
        _view.SetFilterPresenter(_filterOption);
        _view.ThermometerNames = _repository.AllThermometers;
    }

    public void Update()
    {

        // IThermometer thermometer = _repository.FindThermometer(_filterOption.GetThermoName());

        //Todo supprimer les valeur forcées
        IList <string[]> FORCETESTinfos = new List<string[]>();
        FORCETESTinfos.Add(new string[] { "20.00°", "10/19/2021" });
        FORCETESTinfos.Add(new string[] { "21.00°", "10/20/2021" });
        FORCETESTinfos.Add(new string[] { "22.00°", "10/21/2021", "10.50°" });
        FORCETESTinfos.Add(new string[] { "23.00°", "10/22/2021" });
        var FORCETESTTypePresented1 = new TypePresenter( "temperature", FORCETESTinfos );
        var FORCETESTTypePresented2 = new TypePresenter( "humidite", FORCETESTinfos );

        var infos = new List<ISievedType>();
        infos.Add(FORCETESTTypePresented1);
        infos.Add(FORCETESTTypePresented2);
        _view.InfosThermometer = infos;
        //Todo donne les mesures a la vue
    }

    */
}
