namespace Stas.Monitor.Domains;

public interface IFilterSubscriber
{
    void Update(IFilterAccessor filterAccessor);
}
