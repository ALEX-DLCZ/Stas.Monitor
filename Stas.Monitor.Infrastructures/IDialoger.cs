using System.Collections;
using MySql.Data.MySqlClient;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public interface IDialoger
{
    // public IEnumerator DbRequestDialog(string request);
    public IEnumerable<string> SelectDistinctDialog(string request);

    public IEnumerable<MeasureRecord> AllValeur(string commande);
}
