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
    private IEnumerable<CheckBox> _checkBoxes = Array.Empty<CheckBox>();
    private IEnumerable<ComboBoxItem> _comboBoxes = Array.Empty<ComboBoxItem>();

    public FilterPanel()
    {
        InitializeComponent();
    }

    public event EventHandler<FilterEventArgs>? FilterChanged;


    public IEnumerable<string> Thermometers
    {
        set
        {
            ComboBoxThermometers?.Items.Clear();
            _comboBoxes = value.Select(type => new ComboBoxItem { Content = type, Tag = type, }).ToList();

            foreach (var comboBox in _comboBoxes)
            {
                ComboBoxThermometers?.Items.Add(comboBox);
            }

            ComboBoxThermometers.SelectedIndex = 0;
            ComboBoxThermometers.SelectionChanged += ComboBox_OnSelectionChanged;
            TimeBox.SelectionChanged += ComboBox_OnSelectionChanged;
        }
    }

    public IEnumerable<string> Types
    {
        set
        {
            TypesPanel.Children.Clear();
            _checkBoxes = value.Select(type => new CheckBox { Content = type, Tag = type, IsChecked = true }).ToList();


            foreach (var checkBox in _checkBoxes)
            {
                checkBox.IsCheckedChanged += NotifyFilterChanged;
            }

            TypesPanel.Children.AddRange(_checkBoxes);

            OnFilterChanged();
        }
    }

    private void NotifyFilterChanged(object? sender, RoutedEventArgs e)
    {
        OnFilterChanged();
    }

    private void ComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        OnFilterChanged();
    }

    private void NotifyFilterChanged(FilterEventArgs filterArgs)
    {
        FilterChanged?.Invoke(this, filterArgs);
    }


    private void OnFilterChanged()
    {
        var selectedTypes = _checkBoxes
            .Where(checkBoxes => checkBoxes.IsChecked ?? false)
            .Select(checkBoxes => checkBoxes.Tag as string ?? String.Empty);

        var selectedThermometer = _comboBoxes.ElementAt(ComboBoxThermometers.SelectedIndex).Tag as string ?? String.Empty;

        var selectedTime = TimeBox.SelectedIndex switch
        {
            0 => 30,
            1 => 60,
            2 => 300,
            _ => 60
        };


        var filterArgs = new FilterEventArgs(Types: selectedTypes,
            ThermometerTarget: selectedThermometer,
            TimeSelected: selectedTime);

        NotifyFilterChanged(filterArgs);
    }
}
