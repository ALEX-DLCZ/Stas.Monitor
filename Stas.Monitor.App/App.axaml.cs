using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Serilog;
using Stas.Monitor.Infrastructures;
using Stas.Monitor.Views;
using Stas.Monitor.Presentations;

namespace Stas.Monitor.App;

public partial class App : Application
{
  private MainWindow? _mainWindow;

  public override void Initialize()
  {
    AvaloniaXamlLoader.Load(this);

    var log = new LoggerConfiguration()
      .WriteTo.Console()
      .CreateLogger();

    Log.Logger = log; // Définit le log dans un singleton (Beurk)
  }

  public override void OnFrameworkInitializationCompleted()
  {
    if ( ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop )
    {
      _mainWindow = new MainWindow();
      desktop.MainWindow = _mainWindow;
      SetupApp(desktop?.Args ?? Array.Empty<string>());

      DispatcherTimer.Run(() =>
      {
        Log.Logger.Information("You should starts checking for file updates here");
        return true;
      }, TimeSpan.FromSeconds(1));
    }

    base.OnFrameworkInitializationCompleted();
  }

  private void SetupApp(string[] args)
  {
    try
    {
      var mainConfigurationReader = new MainConfigurationReader(args[1]);
      var thermoRepository = new ThermometerRepository(mainConfigurationReader);
      var mainPresenter = new MainPresenter(_mainWindow, thermoRepository);
      mainPresenter.Start();
    }
    catch ( Exception e )
    {
      Console.WriteLine("BHAA SUPER ERREUR PROBLEMEEEEEEE");
    }

    Console.WriteLine("Hello World!");
  }
}