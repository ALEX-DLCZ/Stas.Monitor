using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Stas.Monitor.Views;

public partial class InfoView : UserControl
{
  /*
   
        <StackPanel Grid.Row="0" Grid.Column="0" Orientation="Horizontal">
            <TextBlock Text="{Binding Temperature}"/>
            <TextBlock Text="°"/>
            <TextBlock Text="{Binding Date}"/>
            <TextBlock Text="{Binding TemperatureExpected}"/>
        </StackPanel>
   */
  
  
  
  public InfoView()
  {
    InitializeComponent();
  }

  private void InitializeComponent()
  {
    AvaloniaXamlLoader.Load(this);
  }
  
  public string[] Infos
  {
    set
    {
      Temperature.Text = value[0];
      Date.Text = value[1];
      TemperatureExpected.Text = value[2];
    }
  }
  
  

  
  
  
}