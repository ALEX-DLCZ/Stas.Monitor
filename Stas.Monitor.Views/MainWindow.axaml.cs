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
  private ComboBox? _comboBox;
  private ItemsControl? _infoViewItems;

  public MainWindow()
  {
    InitializeComponent();
    _comboBox = this.FindControl<ComboBox>("ComboBoxThermometers");
    _comboBox.SelectionChanged += ComboBox_SelectionChanged;

    _infoViewItems = this.FindControl<ItemsControl>("InfoViewItems");

  }

  private void InitializeComponent()
  {
    AvaloniaXamlLoader.Load(this);
  }


  public string[] ThermometerNames
  {
    set
    {
      foreach ( var item in value )
      {
        _comboBox?.Items.Add(item);
      }

      _comboBox.SelectedIndex = 0;
    }
  }


  /*
        // Définissez le nombre de fois que vous souhaitez afficher le ContentControl (par exemple 5 fois)
      int numberOfInfoViews = 5;

      // Créez et ajoutez dynamiquement les instances de InfoView au ItemsControl
      for (int i = 0; i < numberOfInfoViews; i++)
      {
        var infoView = new InfoView();
        _infoViewItems.Items.Add(infoView);
      }
       */
  public LinkedList<string[]> InfosThermometer
  {
    set
    {
      _infoViewItems?.Items.Clear();
      foreach ( var info in value )
      {
        var infoView = new InfoView(info);
        _infoViewItems?.Items.Add(infoView);
      }
    }
  }


  private void ComboBox_SelectionChanged(object? sender, EventArgs e)
  {
    if ( sender is ComboBox comboBox )
    {
      _presenter?.ThermometerSelected(comboBox.SelectedIndex);
    }
  }


  public void SetPresenter(MainPresenter mainPresenter)
  {
    _presenter = mainPresenter ?? throw new ArgumentException("mainPresenter");
  }
}