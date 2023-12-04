using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media;
using Stas.Monitor.Presentations;


namespace Stas.Monitor.Views;

public partial class MainWindow : Window , IMainView
{
    private MainPresenter? _presenter;
    private FilterOption? _filterOption;

    public MainWindow()
    {
        InitializeComponent();

        ComboBoxThermometers.SelectionChanged += ComboBox_SelectionChanged;
    }


    private void ComboBox_SelectionChanged(object? sender, EventArgs e)
    {
        if ( sender is ComboBox comboBox )
        {
            // _filterOption?.SetThermoName(comboBox.SelectedItem.ToString());
            // _presenter?.ThermometerSelected(comboBox.SelectedIndex);
            Console.WriteLine("BAAAHHHHH");
            _presenter?.Update();
        }
    }
    public string[] ThermometerNames
    {
        set
        {
            foreach ( var item in value )
            {
                ComboBoxThermometers?.Items.Add(item);
            }
            ComboBoxThermometers.SelectedIndex = 0;
        }
    }





    public void SetPresenter(MainPresenter mainPresenter)
    {
        _presenter = mainPresenter ?? throw new ArgumentException("mainPresenter");
    }
    public void SetFilterPresenter(FilterOption filterOption)
    {
        _filterOption = filterOption ?? throw new ArgumentException("filterOption");
    }

    public IList<ISievedType> InfosThermometer
    {
        set
        {
            TypeViewItems?.Items.Clear();
            //donne la couleur rouge puis bleu puis puis rose avec un SolidColorBrush avec des unit
            //TODO peut etre changer le fonctionnement de la génération des couleurs
            IList<SolidColorBrush> colors = new List<SolidColorBrush>();
            colors.Add(new SolidColorBrush(Color.FromRgb(255, 0, 0)));
            colors.Add(new SolidColorBrush(Color.FromRgb(0, 0, 255)));
            colors.Add(new SolidColorBrush(Color.FromRgb(255, 0, 255)));
            colors.Add(new SolidColorBrush(Color.FromRgb(0, 255, 0)));

            int i = 0;
            foreach ( var info in value )
            {
                var typeView = new TypeView(info, colors[i]);
                TypeViewItems?.Items.Add(typeView);
                i++;
            }
        }
    }
}
