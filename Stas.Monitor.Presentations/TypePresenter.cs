namespace Stas.Monitor.Presentations;

public class TypePresenter : ISievedType
{
    private readonly string _typeName;
    private readonly IList<string[]> _infos;

    public TypePresenter(string typeName, IList<string[]> infos)
    {
        _typeName = typeName;
        _infos = infos;
    }

    public string GetTypeName() => _typeName;

    public IList<string[]> GetInfos() => _infos;
}
