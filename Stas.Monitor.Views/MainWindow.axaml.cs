using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Stas.Monitor.Presentations;
using Stas.Monitor.Views.Controls;


namespace Stas.Monitor.Views;

public partial class MainWindow : Window , IMainView
{

    // private readonly IList<TypeView> _filterResult = new List<TypeView>();
    private readonly IDictionary<string, TypeView> _filterResultDict = new Dictionary<string, TypeView>();

    public MainWindow()
    {
        InitializeComponent();

    }

    public IEnumerable<string> Types
    {
        set => FilterPanel.Types = value; // Forward call
    }
    public IEnumerable<string> ThermometersNames
    {
        set => FilterPanel.Thermometers = value; // Forward call
    }

    public IReadOnlyList<MeasurePresenterModel> Result
    {
        set
        {
            ResultPanel.Children.Clear();
            _filterResultDict.Clear();


            foreach (var type in value)
            {
                //vérifie si le type est déjà présent dans le dictionnaire
                if (_filterResultDict.ContainsKey(type.Type))
                {
                    //si oui on ajoute l'info au type
                    _filterResultDict[type.Type].AddInfoView(new InfoView() { ViewModel = type });
                }
                else
                {
                    //si non on crée un nouveau type et on l'ajoute au dictionnaire
                    var typeView = new TypeView() { ViewTypeName = type.Type };
                    typeView.AddInfoView(new InfoView() { ViewModel = type });
                    _filterResultDict.Add(type.Type, typeView);
                    // _filterResult.Add(typeView);
                    ResultPanel.Children.Add(typeView);


                }
            }


            // var delta = _filterResult.Count - value.Count;// < 0 => not enough elements
            // if (delta < 0)
            // {
            //     var oldCount = _filterResult.Count;
            //     for (int i = 0; i < Math.Abs(delta); ++i)
            //     {
            //         //todo créer un if pour savoir si c'est un type connu et le mettre dedans OU créer un type et mettre mesure dedans
            //         var view = new TypeView() { ViewModel = value[oldCount + i] };
            //         _filterResult.Add(view);
            //         ResultPanel.Children.Add(view);
            //     }
            // }
            //
            // if (delta > 0) // >0 => too much elements Reset them
            // {
            //     for (int i = value.Count; i < _filterResult.Count; ++i)
            //     {
            //         _filterResult[i].Reset();
            //     }
            // }
            //
            // for (int i = 0; i < value.Count; ++i)
            // {
            //     _filterResult[i].ViewModel = value[i];
            // }
        }
    }

    public IReadOnlyList<MeasurePresenterModel> UpdateResult
    {
        set
        {

            foreach (var type in value)
            {
                _filterResultDict[type.Type].AddInfoView(new InfoView() { ViewModel = type });
            }
        }
    }

    public event EventHandler<FilterEventArgs>? FilterChanged;

    private void FilterPanel_OnFilterChanged(object? sender, FilterEventArgs e)
    {
        //todo c'est la qu'on s'abone a l'événement
        FilterChanged?.Invoke(this, e);
    }






    /*
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
    */
}
