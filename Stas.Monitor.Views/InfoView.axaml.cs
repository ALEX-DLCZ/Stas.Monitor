using Avalonia;
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Stas.Monitor.Views;

public partial class InfoView : UserControl
{
    public InfoView(string[] infos)
    {
        InitializeComponent();
        Temperature.Text = infos[0];
        Date.Text = infos[1];

        //infos varie entre 2 et 3 éléments
        if ( infos.Length == 3 )
        {
            TemperatureExpected.Text = "valeur attendue: " + infos[2];
        }
    }
}
