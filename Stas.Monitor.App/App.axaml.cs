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
using Stas.Monitor.Infrastructures.DataBase;
using Stas.Monitor.Presentations;
using Stas.Monitor.Views;

namespace Stas.Monitor.App;

public partial class App : Application
{
    private MainWindow? _mainWindow;
    private MainPresenter? _mainPresenter;

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

            //TODO gérer l'exception fatalException générée par le setup
            SetupApp(desktop?.Args ?? Array.Empty<string>());
            desktop.MainWindow = _mainWindow;

            DispatcherTimer.Run(() =>
            {
                Log.Logger.Information("You should starts checking for file updates here");
                _mainPresenter?.Update();
                return true;
            }, TimeSpan.FromSeconds(5));
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void SetupApp(string[] args)
    {
        try
        {
            ArgsExecutor argsExecutor = new ArgsExecutor(args);

            DbDialog dbDialog = new DbDialog(argsExecutor.GetConnectionString());

            var thermoRepository = new ThermometerRepository(argsExecutor.GetThermoName(), dbDialog);
            _mainPresenter = new MainPresenter(_mainWindow, thermoRepository);
            _mainPresenter.Start();
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error during app setup");
            //TODO changer la main window pour une fenetre d'erreur et continuer l'execution
            throw new FatalException("Error during app setup", e);
        }
    }
}
