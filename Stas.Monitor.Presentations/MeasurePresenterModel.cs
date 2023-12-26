using System.Globalization;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public record MeasurePresenterModel(MeasureRecord Model)
{
    public string Type
        => Model.Type;

    public string Value
        => Model.Measure.Value.ToString(Model.Measure.Format);

    public string Difference => Model.Measure.Difference == 0 ? "0" : Model.Measure.Difference.ToString(Model.Measure.Format);


    public string Date
        => Model.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.GetCultureInfo("fr-FR"));

    public string Color
        => Model.Type switch
        {
            "temperature" => "0xFFA500",
            "humidity" => "0x0000FF",
            _ => "0x00FFFF"
        };
}
