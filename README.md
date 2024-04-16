# MicroGauge
**MicroGauge** is a radial and linear gauge library for .Net

* **MicroGauge** utilizes the [SkiaSharp](https://github.com/mono/SkiaSharp) Library to draw radial and linear gauges (see examples below)
* Inspired by [MicroCharts](https://github.com/microcharts-dotnet/Microcharts), it provides a simple and compact way to draw gauges on a canvas.
* The radial gauge offers full-circle, half-circle, and fitted design styles.
* The linear gauge offers vertical and horizontal design styles.

---
## Gallery
![Gallery](https://github.com/galenblakeman/MicroGauge/blob/master/Doc/Gallery.png)

---
## Packages
[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge?style=flat-square&logo=nuget&label=MicroGauge)](https://www.nuget.org/packages/MicroGauge/)

The base library that you can work with directly 
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
## Tag Reference

* [Radial Gauge](https://github.com/galenblakeman/MicroGauge/blob/master/Doc/RadialGaugeTag.md)
* [Linear Gauge](https://github.com/galenblakeman/MicroGauge/blob/master/Doc/LinearGaugeTag.md)

---
##  Xamarin.Forms Usage
###  Nuget Package Install

```Dotenv
dotnet add package MicroGauge.Forms
```

###  Namespace Attribute

```XAML
xmlns:microGauge="clr-namespace:MicroGauge.Forms;assembly=MicroGauge.Forms"
```

###  XAML

```XAML
<microGauge:XfGaugeRadial Value="15"  NeedleBrush="#008000" />
<microGauge:XfGaugeLinear Value="15"  NeedleBrush="#008000" />
```

###  C#

```C#
XfGaugeRadial radialGauge = new XfGaugeRadial
{
    Value = 15.0, NeedleBrush = new SolidColorBrush(Color.Green)
};
XfGaugeLinear linearGauge = new XfGaugeLinear
{
    Value = 15.0, NeedleBrush = new SolidColorBrush(Color.Green)
};
```

###  Examples
* Project: [MicroGauge.Example.Forms](https://github.com/galenblakeman/MicroGauge/tree/master/Example/MicroGauge.Example.Forms)
* Bindings: [MainPage.xaml](https://github.com/galenblakeman/MicroGauge/blob/master/Example/MicroGauge.Example.Forms/MicroGauge.Example.Forms/MainPage.xaml)  
---

##  .Net MAUI Usage
###  Nuget Package Install

```Dotenv
dotnet add package MicroGauge.Maui
```

###  Namespace Attribute

```XAML
xmlns:microGauge="clr-namespace:MicroGauge.Maui;assembly=MicroGauge.Maui"
```

###  XAML

```XAML
<microGauge:MauiGaugeRadial Value="15" NeedleBrush="#008000" />
<microGauge:MauiGaugeLinear Value="15" NeedleBrush="#008000" />
```

###  C#

```C#
MauiGaugeRadial radialGauge = new MauiGaugeRadial
{
    Value = 15.0, NeedleBrush = new SolidColorBrush(Colors.Green)
};
MauiGaugeLinear linearGauge = new MauiGaugeLinear
{
    Value = 15.0, NeedleBrush = new SolidColorBrush(Colors.Green)
};
```

####  Examples
* Project: [MicroGauge.Example.Maui](https://github.com/galenblakeman/MicroGauge/tree/master/Example/MicroGauge.Example.Maui)
* Bindings: [MainPage.xaml](https://github.com/galenblakeman/MicroGauge/blob/master/Example/MicroGauge.Example.Maui/MainPage.xaml)
---

##  WPF Usage
###  Nuget Package Install

```Dotenv
dotnet add package MicroGauge.Wpf
```

###  Namespace Attribute

```XAML
xmlns:microGauge="clr-namespace:MicroGauge.Wpf;assembly=MicroGauge.Wpf"
```

###  XAML

```XAML
<microGauge:WpfGaugeRadial Value="15" NeedleBrush="#008000" />
<microGauge:WpfGaugeLinear Value="15" NeedleBrush="#008000" />
```

###  C#

```C#
WpfGaugeRadial radialGauge = new WpfGaugeRadial
{
    Value = 15.0, NeedleBrush = new SolidColorBrush(Colors.Green)
};
WpfGaugeLinear linearGauge = new WpfGaugeLinear
{
    Value = 15.0, NeedleBrush = new SolidColorBrush(Colors.Green)
};
```

###  Examples
* Project: [MicroGauge.Example.Wpf](https://github.com/galenblakeman/MicroGauge/tree/master/Example/MicroGauge.Example.Wpf)
* Bindings: [MainWindow.xaml](https://github.com/galenblakeman/MicroGauge/blob/master/Example/MicroGauge.Example.Wpf/MainWindow.xaml)  

---
## License
MIT © Galen Blakeman
