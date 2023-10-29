using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Stas.Monitor.Views;

public partial class InfoView : UserControl
{
  
  
  public InfoView()
  {
    InitializeComponent();
  }

  private void InitializeComponent()
  {
    AvaloniaXamlLoader.Load(this);
  }
  /*
  public string[] Infos
  {
    set
    {
      Temperature.Text = value[0];
      Date.Text = value[1];
      TemperatureExpected.Text = value[2];
    }
  }
  */
  
  

  
  
  
}