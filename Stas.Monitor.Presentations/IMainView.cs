namespace Stas.Monitor.Presentations;

public interface IMainView
{
    void SetPresenter(MainPresenter mainPresenter); 
    
    void SetSelectedThermometer(string thermometerName);
    string[] ThermometerNames { set; }
}