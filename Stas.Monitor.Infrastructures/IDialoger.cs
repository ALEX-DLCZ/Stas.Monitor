using System.Collections;
using MySql.Data.MySqlClient;

namespace Stas.Monitor.Infrastructures;

public interface IDialoger
{
    // public IEnumerator DbRequestDialog(string request);
    public IEnumerable<string> SelectDistinctDialog(string request);
}
