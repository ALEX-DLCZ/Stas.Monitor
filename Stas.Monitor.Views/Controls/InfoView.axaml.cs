using Avalonia.Controls;
using Avalonia.Media;
using Stas.Monitor.Presentations;

namespace Stas.Monitor.Views.Controls;

public partial class InfoView : UserControl
{


    public InfoView()
    {
        InitializeComponent();
    }

    public SolidColorBrush Color
    {
        set => InfoGrid.Background = value;
    }

    public MeasurePresenterModel ViewModel
    {
        set
        {
            Measurement.Text = value.Value;
            Date.Text = value.Date;
            // LabelsPanel.Children.Clear();
            // foreach (var text in value.Labels)
            // {
            //     LabelsPanel.Children.Add(new TextBlock
            //     {
            //         Text = text,
            //         FontSize = 10
            //     });
            // }
            if (value.Difference == "0")
            {
                return;
            }

            MeasurementExpected.Text = "valeur attendue: " + value.Difference;
            InfoGrid.Background = new SolidColorBrush(value.UintColor);
        }
    }






    // public InfoView(string[] infos, SolidColorBrush color)
    // {
    //     //string|] : 0=valeurFormater, 1=dateformater, 2=expectedValue
    //     InitializeComponent();
    //     Measurement.Text = infos[0];
    //     Date.Text = infos[1];
    //
    //     //rentre dans la condition si la valeur attendue est présente
    //     if ( infos.Length > 2 )
    //     {
    //         MeasurementExpected.Text = "valeur attendue: " + infos[2];
    //         InfoGrid.Background = color;
    //     }
    // }
}
