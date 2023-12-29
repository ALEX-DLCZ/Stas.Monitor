using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public interface IQueryBuildAccess
{
    public string? Query { get; }

    public IEnumerable<ParameterMap>? ParameterMaps { get; }
}
