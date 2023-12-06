namespace Stas.Monitor.Presentations;

public interface IMainView
{
    IEnumerable<string> Types { set; }

    IReadOnlyList<MeasurePresenterModel> Result { set; }

    event EventHandler<FilterEventArgs> FilterChanged;




    // void SetPresenter(MainPresenter presenterHimself);
    //
    // void SetFilterPresenter(FilterOption filterOption);
    //
    // string[] ThermometerNames { set; }
    //
    // IList<ISievedType> InfosThermometer { set; }
}
