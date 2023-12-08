using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Stas.Monitor.Presentations;

namespace Stas.Monitor.Views.Controls;

public partial class FilterPanel : UserControl
{
    private IEnumerable<ToggleSwitch> _toggleSwitches = Array.Empty<ToggleSwitch>();

    public FilterPanel()
    {
        InitializeComponent();
    }

    public event EventHandler<FilterEventArgs>? FilterChanged;

    public IEnumerable<string> Types
    {
        set
        {
            TypesPanel.Children.Clear();
            _toggleSwitches = value.Select(type => new ToggleSwitch
            {
                OffContent = $"Ignore {type}",
                OnContent = $"Include {type}",
                Tag = type, // Associe une valeur au contrôle. Utile pour la création d'événements
                IsChecked = false
            }).ToList();

            foreach (var toggleSwitch in _toggleSwitches)
            {
                toggleSwitch.IsCheckedChanged += NotifyFilterChanged;
            }

            TypesPanel.Children.AddRange(_toggleSwitches);
        }
    }
    public IEnumerable<string> Thermometers
    {
        set
        {
            foreach ( var item in value )
            {
                ComboBoxThermometers?.Items.Add(item);
            }
        }
    }

    private void NotifyFilterChanged(object? sender, RoutedEventArgs e)
    {
        OnFilterChanged();
    }

    private void ComboBoxThermometers_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        OnFilterChanged();
    }

    private void NameBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        OnFilterChanged();
    }

    private void GenerationSlider_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        OnFilterChanged();
    }

    private void NotifyFilterChanged(FilterEventArgs filterArgs)
    {
        FilterChanged?.Invoke(this, filterArgs);
    }


    private void OnFilterChanged()
    {
        var selectedTypes = _toggleSwitches
            .Where(toggleSwitch => toggleSwitch.IsChecked ?? false)
            .Select(toggleSwitch => toggleSwitch.Tag as string ?? String.Empty);

        // var queryArgs = new FilterEventArgs(Types: selectedTypes,
        //     Contains: NameBox.Text ?? String.Empty,
        //     Generation: (int)GenerationSlider.Value,
        //     OnlyLegendary: LegendarySwitch.IsChecked ?? false);
        var filterArgs = new FilterEventArgs(Types: selectedTypes,
            ThermometerIndex: ComboBoxThermometers.SelectedIndex,
            TimeSelected: 60.0);

        NotifyFilterChanged(filterArgs);
    }

}
