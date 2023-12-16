using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public record MeasurePresenterModel(MeasureRecord Model)
{

    public string Type
        => Model.Type;


    //Model.Measure.Value = 0.6126041
    //Model.Measure.Format = 0%
    //MeasurePresenterModel.Value = 61%
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

    //si type = temperature alors couleur rouge
    //si type = humidite alors couleur bleu
    //si type = pression alors couleur vert
    //si type = vent alors couleur orange
    public string Color
        => Model.Type switch
        {
            "temperature" => "0xFFA500",
            "humidity" => "0x0000FF",
            _ => "0x00FFFF"
        };

}



// public string ValueToString() => _value.ToString(_format);
//
// public string ValueExpectedToString() => _valueExpected.ToString(_format);
