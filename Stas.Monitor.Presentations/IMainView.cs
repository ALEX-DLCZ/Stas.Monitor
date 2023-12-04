namespace Stas.Monitor.Presentations;

public interface IMainView
{
    void SetPresenter(MainPresenter presenterHimself);

    void SetFilterPresenter(FilterOption filterOption);

    string[] ThermometerNames { set; }

    IList<ISievedType> InfosThermometer { set; }
}
