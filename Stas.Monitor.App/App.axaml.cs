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

            DbDialog dbDialog = new DbDialog(argsExecutor.GetConnectionString());

            Console.WriteLine("connectionString : " + argsExecutor.GetConnectionString());

            // foreach (var mesureList in dbDialog.allValeurGPT())
            // {
            //     Console.WriteLine(" ");
            //     Console.WriteLine(mesureList.Name);
            //     Console.WriteLine(mesureList.Type);
            //     Console.WriteLine(mesureList.Date);
            //     Console.WriteLine(mesureList.Measure.Value);
            //     Console.WriteLine(mesureList.Measure.Difference);
            //     Console.WriteLine(mesureList.Measure.Format);
            //
            // }

            var thermoRepository = new ThermometerRepository(  argsExecutor.GetThermoName(), dbDialog );
            var mainPresenter = new MainPresenter(_mainWindow, thermoRepository);
            mainPresenter.Start();
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error during app setup");
            //TODO changer la main window pour une fenetre d'erreur et continuer l'execution
            throw new FatalException("Error during app setup", e);
        }
    }

}
