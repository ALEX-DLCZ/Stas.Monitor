namespace Stas.Monitor.Domains;

public interface IThermometer
{
    string Name { get; }

    IList<IType> Types { get; }
}
