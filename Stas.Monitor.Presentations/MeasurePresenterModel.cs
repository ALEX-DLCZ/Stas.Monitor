using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public record MeasurePresenterModel(MeasureRecord Model)
{

    public string Type
        => Model.Type;


    public string Value
        => $"#{Model.Measure.Value:Model.Measure.Format}";

    public string Difference
        => Model.Measure.Difference == 0 ? "0" : $"#{Model.Measure.Difference:Model.Measure.Format}";

    public string Date
        => Model.Date.ToString("dd/MM/yyyy HH:mm:ss");

    //si type = temperature alors couleur rouge
    //si type = humidite alors couleur bleu
    //si type = pression alors couleur vert
    //si type = vent alors couleur orange
    public uint UintColor
        => Model.Type switch
        {
            "temperature" => 0xFFA500,
            "humidite" => 0x0000FF,
            _ => 0x00FFFF
        };

}



// public string ValueToString() => _value.ToString(_format);
//
// public string ValueExpectedToString() => _valueExpected.ToString(_format);
