using Stas.Monitor.Domains;

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
}
