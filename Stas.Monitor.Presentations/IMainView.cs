namespace Stas.Monitor.Presentations;

public interface IMainView
{
    IEnumerable<string> Types { set; }

    IEnumerable<string> ThermometersNames { set; }

    IReadOnlyList<MeasurePresenterModel> Result { set; }
    IReadOnlyList<MeasurePresenterModel> UpdateResult { set; }

    event EventHandler<FilterEventArgs> FilterChanged;




    // void SetPresenter(MainPresenter presenterHimself);
    //
    // void SetFilterPresenter(FilterOption filterOption);
    //
    // string[] ThermometerNames { set; }
    //
    // IList<ISievedType> InfosThermometer { set; }
}
