namespace Stas.Monitor.Presentations;

public record FilterEventArgs(IEnumerable<string> Types,
    int ThermometerIndex,
    // int Generation = 1,
    double TimeSelected = 60);

