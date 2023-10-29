using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Stas.Monitor.Domains;
using Stas.Monitor.Presentations;

namespace Stas.Monitor.Views;

public partial class MainWindow : Window, IMainView
{

  
  private MainPresenter? _presenter;
  private ComboBox? _comboBox;
  private ContentControl? _infoViewContent;
  
    public MainWindow()
    {
        InitializeComponent();
        _comboBox = this.FindControl<ComboBox>("ComboBoxThermometers");
        _comboBox.SelectionChanged += ComboBox_SelectionChanged;
        
        _infoViewContent = this.FindControl<ContentControl>("InfoViewContent");
        var infoView = new InfoView();
        _infoViewContent.Content = infoView;

        
    }
    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
      
    }
   

    public string[] ThermometerNames
    {
      set
      {
        foreach (var item in value)
        {
          _comboBox?.Items.Add(item);
        }
        _comboBox.SelectedIndex = 0;
      }
    }
    
    public LinkedList<IInfo> InfosThermometer
    {
      set
      {
        /*
        var textBlock = this.FindControl<TextBlock>("TextBlockInfos");
        textBlock.Text = "";
        foreach (var info in value)
        {
          textBlock.Text += info.ToString() + "\n";
        }
        */
      }
    }


    

    private void ComboBox_SelectionChanged(object? sender, EventArgs e)
    {
      if (sender is ComboBox comboBox)
      {
        _presenter?.ThermometerSelected(comboBox.SelectedIndex);
      }
    }
    
    
    
    public void SetPresenter(MainPresenter mainPresenter)
    {
      _presenter = mainPresenter ?? throw new ArgumentException("mainPresenter");
    }
}