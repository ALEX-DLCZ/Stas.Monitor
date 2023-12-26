using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Serilog;
using Stas.Monitor.Infrastructures;
using Stas.Monitor.Infrastructures.DataBase;
using Stas.Monitor.Infrastructures.PersonalExceptions;
using Stas.Monitor.Presentations;
using Stas.Monitor.Views;

namespace Stas.Monitor.App;

/**
 * <summary>
 *     l'applications se lance dans Stas.Monitor.App avec la commande :
 * dotnet run -- --config-file config.ini
 *
 * --config-file config.ini : permet de spécifier le fichier de configuration
 * la connexion a la base de donnée est spécifié dans le fichier config.ini et est obligatoire.
 *
 * !!! ATTENTION !!!
 * les mesures affichées se base sur la mesure la plus récente TOUT THERMOMETRE CONFONDUS.
 * </summary>

 */
public class App : Application
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

            SetupApp(desktop.Args ?? Array.Empty<string>());
            desktop.MainWindow = _mainWindow;

            DispatcherTimer.Run(() =>
            {
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
            var argsExecutor = new ArgsExecutor(args);

            var dbDialog = new DbDialog(argsExecutor.GetConnectionString());

            var thermoRepository = new ThermometerRepository(argsExecutor.GetThermoName(), dbDialog);
            if (_mainWindow != null)
            {
                _mainPresenter = new MainPresenter(_mainWindow, thermoRepository);
            }

            _mainPresenter?.Start();
        }
        catch (DbConnectionException e)
        {
            Log.Logger.Error(e.Message);

            //TODO changer la main window pour une fenetre d'erreur et continuer l'execution
            // throw new FatalException("Error during app setup", e);
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error during app setup");

            //TODO changer la main window pour une fenetre d'erreur et continuer l'execution
            // throw new FatalException("Error during app setup", e);
        }
    }
}
