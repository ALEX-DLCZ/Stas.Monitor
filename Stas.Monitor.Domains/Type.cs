namespace Stas.Monitor.Domains;

public class Type : IType
{
    private string _typeName;
    private IList<IInfo> _infos;

    public Type(string typeName)
    {
        _typeName = typeName;
        _infos = new List<IInfo>();
    }

    public string GetTypeName()
    {
        return _typeName;
    }

    public void AddInfo(IInfo info)
    {
        _infos.Add(info);
    }

    public IList<IInfo> GetInfos() => _infos;
}
