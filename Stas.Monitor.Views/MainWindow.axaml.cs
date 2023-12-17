using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Stas.Monitor.Presentations;
using Stas.Monitor.Views.Controls;


namespace Stas.Monitor.Views;

public partial class MainWindow : Window, IMainView
{
    private readonly IDictionary<string, TypeView> _filterResultDict = new Dictionary<string, TypeView>();

    public MainWindow()
    {
        InitializeComponent();
    }

    public IEnumerable<string> Types
    {
        set => FilterPanel.Types = value;
    }

    public IEnumerable<string> ThermometersNames
    {
        set => FilterPanel.Thermometers = value;
    }

    public IReadOnlyList<MeasurePresenterModel> Result
    {
        set
        {
            ResultPanel.Children.Clear();
            _filterResultDict.Clear();

            foreach (var type in value)
            {
                if (_filterResultDict.ContainsKey(type.Type))
                {
                    _filterResultDict[type.Type].AddInfoView(new InfoView() { ViewModel = type });
                }
                else
                {
                    var typeView = new TypeView() { ViewTypeName = type.Type };
                    typeView.AddInfoView(new InfoView() { ViewModel = type });
                    _filterResultDict.Add(type.Type, typeView);
                    // _filterResult.Add(typeView);
                    ResultPanel.Children.Add(typeView);
                }
            }
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
        FilterChanged?.Invoke(this, e);
    }
}
