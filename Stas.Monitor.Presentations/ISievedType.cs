namespace Stas.Monitor.Presentations;

public interface ISievedType
{
    string GetTypeName();

    IList<string[]> GetInfos();
}
