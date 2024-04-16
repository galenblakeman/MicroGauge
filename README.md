# MicroGauge
**MicroGauge** is a simple radial and linear gauge library for .Net

* **MicroGauge** utilizes the [SkiaSharp](https://github.com/mono/SkiaSharp) Library to draw radial and linear gauge (see examples below)
* Inspired by [MicroCharts](https://github.com/microcharts-dotnet/Microcharts), it provides a simple and compact way to draw gauges on a canvas.
* The radial gauge offers full-circle, half-circle, and fitted design styles.
* The linear gauge offers vertical and horizontal design styles.

---
## Gallery
![Gallery](https://github.com/galenblakeman/MicroGauge/blob/master/Gallery.png)

---
## Packages
[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge?style=flat-square&logo=nuget&label=MicroGauge)](https://www.nuget.org/packages/MicroGauge/)

Base library that you can work with directly 
[[Source](https://github.com/galenblakeman/MicroGauge/tree/master/Library/MicroGauge)]

[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge.Forms?style=flat-square&logo=nuget&label=MicroGauge.Forms)](https://www.nuget.org/packages/MicroGauge.Forms/)

Xamarina.Forms wrapper library for **MicroGauge** that utilizes SkiaSharp.Views.Forms and BindableProperty bindings 
[[Source](https://github.com/galenblakeman/MicroGauge/tree/master/Library/MicroGauge.Forms)]

[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge.Maui?style=flat-square&logo=nuget&label=MicroGauge.Maui)](https://www.nuget.org/packages/MicroGauge.Maui/)

.Net MAUI wrapper for **MicroGauge** library that utilizes SkiaSharp.Views.Maui.Controls and BindableProperty bindings
[[Source](https://github.com/galenblakeman/MicroGauge/tree/master/Library/MicroGauge.Maui)]

[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge.Wpf?style=flat-square&logo=nuget&label=MicroGauge.Wpf)](https://www.nuget.org/packages/MicroGauge.Wpf/)

WPF wrapper library for **MicroGauge** that utilizes SkiaSharp.Views.WPF and DependencyProperty bindings
[[Source](https://github.com/galenblakeman/MicroGauge/tree/master/Library/MicroGauge.Wpf)]

---
## Usage

###  Xamarin.Forms
####  Install Package

```Dotenv
dotnet add package MicroGauge.Forms
```

####  Add Namespace Attribute

```XAML
xmlns:microGauge="clr-namespace:MicroGauge.Forms;assembly=MicroGauge.Forms"
```

####  Use Tags

```XAML
<microGauge:XfGaugeRadial Value="15" MinValue="0" MaxValue="30" />
<microGauge:XfGaugeLinear Value="15" MinValue="0" MaxValue="30" />
```

####  Examples
* Project: [MicroGauge.Example.Forms](https://github.com/galenblakeman/MicroGauge/tree/master/Example/MicroGauge.Example.Forms)
* Bindings: [MainPage.xaml](https://github.com/galenblakeman/MicroGauge/blob/master/Example/MicroGauge.Example.Forms/MicroGauge.Example.Forms/MainPage.xaml)  


###  .Net MAUI
####  Install Package

```Dotenv
dotnet add package MicroGauge.Maui
```

####  Add Namespace Attribute

```XAML
xmlns:microGauge="clr-namespace:MicroGauge.Maui;assembly=MicroGauge.Maui"
```

####  Use Tags

```XAML
<microGauge:MauiGaugeRadial Value="15" MinValue="0" MaxValue="30" />
<microGauge:MauiGaugeLinear Value="15" MinValue="0" MaxValue="30" />
```

####  Examples
* Project: [MicroGauge.Example.Maui](https://github.com/galenblakeman/MicroGauge/tree/master/Example/MicroGauge.Example.Maui)
* Bindings: [MainPage.xaml](https://github.com/galenblakeman/MicroGauge/blob/master/Example/MicroGauge.Example.Maui/MainPage.xaml)


###  Windows Presentation Foundation (WPF)
####  Install Package

```Dotenv
dotnet add package MicroGauge.Wpf
```

####  Add Namespace Attribute

```XAML
xmlns:microGauge="clr-namespace:MicroGauge.Wpf;assembly=MicroGauge.Wpf"
```

####  Use Tags

```XAML
<microGauge:WpfGaugeRadial Value="15" MinValue="0" MaxValue="30" />
<microGauge:WpfGaugeLinear Value="15" MinValue="0" MaxValue="30" />
```

####  Examples
* Project: [MicroGauge.Example.Wpf](https://github.com/galenblakeman/MicroGauge/tree/master/Example/MicroGauge.Example.Wpf)
* Bindings: [MainWindow.xaml](https://github.com/galenblakeman/MicroGauge/blob/master/Example/MicroGauge.Example.Wpf/MainWindow.xaml)  

---
## Tag Reference
* [Radial Gauge](https://github.com/galenblakeman/MicroGauge/blob/doc/RadialGaugeTag.md)
* [Linear Gauge](https://github.com/galenblakeman/MicroGauge/blob/doc/LinearGaugeTag.md)

---
## License
MIT © Galen Blakeman
