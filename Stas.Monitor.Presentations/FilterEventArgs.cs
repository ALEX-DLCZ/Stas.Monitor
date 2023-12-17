namespace Stas.Monitor.Presentations;

public record FilterEventArgs(
    IEnumerable<string> Types,
    string ThermometerTarget,
    int TimeSelected = 60);
