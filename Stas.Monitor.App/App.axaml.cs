using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Serilog;
using Stas.Monitor.App.PersonalExceptions;
using Stas.Monitor.Domains;
using Stas.Monitor.Infrastructures;
using Stas.Monitor.Presentations;
using Stas.Monitor.Views;

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
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _mainWindow = new MainWindow();
            SetupApp(desktop?.Args ?? Array.Empty<string>());

            desktop.MainWindow = _mainWindow;

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
            //TODO implémenter le MainConfigurationReader pour récupérer les noms des thermomètres et lacces en base de donnée
            ArgsExecutor argsExecutor = new ArgsExecutor(args);

            var thermoRepository = new ThermometerRepository(  argsExecutor.GetThermoName() );
            var mainPresenter = new MainPresenter(_mainWindow, thermoRepository);
            mainPresenter.Start();
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error during app setup");
            throw new FatalException("Error during app setup", e);
        }
    }

}
