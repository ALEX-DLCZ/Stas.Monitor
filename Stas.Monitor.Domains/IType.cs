namespace Stas.Monitor.Domains;

public interface IType
{
    string GetTypeName();

    IList<IInfo> GetInfos();
}
