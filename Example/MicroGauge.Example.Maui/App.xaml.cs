#pragma warning disable CA1826

namespace MicroGauge.Example.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override void OnStart()
    {
        base.OnStart();

        // Restore window position
        var window = Current?.Windows.FirstOrDefault();
        if (window == null) return;
        try
        {
            window.X = Preferences.Get("WindowX", 50.0);
            window.Y = Preferences.Get("WindowY", 50.0);
            window.Width = Preferences.Get("WindowWidth", 800.0);
            window.Height = Preferences.Get("WindowHeight", 1200.0);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Set Position: {ex.Message}");
        }

        window.Destroying += Window_Destroying;


        // Unhandled
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            Console.WriteLine($"UnhandledException:{e.ExceptionObject}");
        };
    }
   

    private void Window_Destroying(object? sender, EventArgs e)
    {
        // Save window position
        var window = Current?.Windows.FirstOrDefault();
        if (window == null) return;
        Preferences.Set("WindowX", window.X);
        Preferences.Set("WindowY", window.Y);
        Preferences.Set("WindowWidth", window.Width);
        Preferences.Set("WindowHeight", window.Height);
    }
}