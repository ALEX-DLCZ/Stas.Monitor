using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Stas.Monitor.Presentations;

namespace Stas.Monitor.Views;

public partial class MainWindow : Window, IMainView
{

  
  private MainPresenter? _presenter;
  
    public MainWindow()
    {
        InitializeComponent();
        
    }
   
    public void SetPresenter(MainPresenter mainPresenter)
    {
      _presenter = mainPresenter ?? throw new ArgumentException("mainPresenter");
    }
    public void SetSelectedThermometer(string thermometerName)
    {
    }
    
    public string[] ThermometerNames { get; set; }
    
    
    
    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
      var comboBox = this.FindControl<ComboBox>("ComboBoxThermometers");
      comboBox.SelectionChanged += ComboBox_SelectionChanged;
    }
    

    private void ComboBox_SelectionChanged(object? sender, EventArgs e)
    {
      if (sender is ComboBox comboBox)
      {
        int selectedIndex = comboBox.SelectedIndex;
        Console.WriteLine(selectedIndex);
      }
    }
}