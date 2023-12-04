using Avalonia.Controls;
using Avalonia.Media;

namespace Stas.Monitor.Views;

public partial class InfoView : UserControl
{
    public InfoView(string[] infos, SolidColorBrush color)
    {
        //string|] : 0=valeurFormater, 1=dateformater, 2=expectedValue
        InitializeComponent();
        Measurement.Text = infos[0];
        Date.Text = infos[1];

        //rentre dans la condition si la valeur attendue est présente
        if ( infos.Length > 2 )
        {
            MeasurementExpected.Text = "valeur attendue: " + infos[2];
            InfoGrid.Background = color;
        }
    }
}
