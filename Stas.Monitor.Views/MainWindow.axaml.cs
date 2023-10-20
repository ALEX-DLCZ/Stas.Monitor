using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Stas.Monitor.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    public void Select_Name_OnClick(object? sender, RoutedEventArgs e)
    {

      System.Diagnostics.Debug.WriteLine("Select_Name_OnClick");
        
    }
    
    
}