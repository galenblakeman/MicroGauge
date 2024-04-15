// ReSharper disable UnusedMember.Global
namespace MicroGauge.Example.Wpf;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        if (Properties.Default.WindowWidth < 100 || Properties.Default.WindowHeight < 100) return;
        Left = Properties.Default.WindowLeft;
        Top = Properties.Default.WindowTop;
        Width = Properties.Default.WindowWidth;
        Height = Properties.Default.WindowHeight;
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        Properties.Default.WindowLeft = Left;
        Properties.Default.WindowTop = Top;
        Properties.Default.WindowWidth = Width;
        Properties.Default.WindowHeight = Height;
        Properties.Default.Save();
    }
}