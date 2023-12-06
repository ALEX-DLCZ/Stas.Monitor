namespace Stas.Monitor.Domains;

public record MeasureRecord(
    string Name,
    string Type,
    DateTime Date,
    Measure Measure);
