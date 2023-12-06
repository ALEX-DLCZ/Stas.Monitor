using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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

    private void NotifyFilterChanged(object? sender, RoutedEventArgs e)
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

        var queryArgs = new FilterEventArgs(Types: selectedTypes,
            Contains: NameBox.Text ?? String.Empty,
            Generation: (int)GenerationSlider.Value,
            OnlyLegendary: LegendarySwitch.IsChecked ?? false);

        NotifyFilterChanged(queryArgs);
    }
}
