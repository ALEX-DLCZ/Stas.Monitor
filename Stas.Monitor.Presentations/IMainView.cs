using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public interface IMainView
{
    void SetPresenter(MainPresenter mainPresenter); 
    
    string[] ThermometerNames { set; }
    
    LinkedList<string[]> InfosThermometer { set; }
}