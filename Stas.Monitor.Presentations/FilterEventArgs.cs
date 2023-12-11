namespace Stas.Monitor.Presentations;

public record FilterEventArgs(IEnumerable<string> Types,
    string ThermometerTarget,

    // int Generation = 1,
    int TimeSelected);
