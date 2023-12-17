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


    public MeasurePresenterModel ViewModel
    {
        set
        {
            Measurement.Text = value.Value;
            Date.Text = value.Date;

            if (value.Difference == "0")
            {
                return;
            }

            MeasurementExpected.Text = "Différence de: " + value.Difference;
            var color = System.Drawing.ColorTranslator.FromHtml(value.Color);
            InfoGrid.Background = new SolidColorBrush(new Color(color.A, color.R, color.G, color.B));
        }
    }
}
