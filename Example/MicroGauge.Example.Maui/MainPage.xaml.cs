using SkiaSharp;

namespace MicroGauge.Example.Maui;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (WindSpeedGauge.BackgroundImage != null) return;
        try
        {
            await using (var stream = await FileSystem.OpenAppPackageFileAsync("aluminium_texture_radial.png"))
            {
                WindSpeedGauge.BackgroundImage = SKImage.FromEncodedData(SKData.Create(stream));
            }

            await using (var stream = await FileSystem.OpenAppPackageFileAsync("copper_texture_plain.png"))
            {
                BatteryGauge.BackgroundImage = SKImage.FromEncodedData(SKData.Create(stream));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Background image load failed: {ex.Message}");
        }
    }
}
