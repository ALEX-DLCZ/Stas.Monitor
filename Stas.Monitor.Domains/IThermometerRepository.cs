namespace Stas.Monitor.Domains;

public interface IThermometerRepository
{
    string[] AllThermometers { get; }

    IRequest NewRequest();
}
