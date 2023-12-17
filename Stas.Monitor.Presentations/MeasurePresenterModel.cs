using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public record MeasurePresenterModel(MeasureRecord Model)
{
    public string Type
        => Model.Type;

    public string Value
        => Model.Measure.Value.ToString(Model.Measure.Format);


    public string Difference
    {
        get
        {
            if (Model.Measure.Difference == 0)
            {
                return "0";
            }
            else
            {
                return Model.Measure.Difference.ToString(Model.Measure.Format);
            }
        }
    }

    public string Date
        => Model.Date.ToString("dd/MM/yyyy HH:mm:ss");

    public string Color
        => Model.Type switch
        {
            "temperature" => "0xFFA500",
            "humidity" => "0x0000FF",
            _ => "0x00FFFF"
        };
}
