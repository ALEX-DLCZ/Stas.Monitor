namespace Stas.Monitor.Domains;

public record Measure(
    double Value,
    double Difference,
    string Format);
