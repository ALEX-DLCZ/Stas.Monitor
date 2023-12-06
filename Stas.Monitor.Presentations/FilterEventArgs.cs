namespace Stas.Monitor.Presentations;

public record FilterEventArgs(IEnumerable<string> Types,
    string Contains,
    int Generation = 1,
    bool OnlyLegendary = false);

