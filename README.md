# MicroGauge
**MicroGauge** is a .Net library for easily creating customizable radial and linear gauges. Inspired by [MicroCharts](https://github.com/microcharts-dotnet/Microcharts), it minimalist library designed just for gauges. It uses the [SkiaSharp](https://github.com/mono/SkiaSharp) graphics library to deliver sharp and efficient visuals.

## Key Features
* Radial Gauges: Choose from full-circle, half-circle, and fitted styles to meet design aesthetics. 
* Linear Gauges: Implement vertical or horizontal orientations to meet interface needs. 
* Customization: Configure backing, scale, tick (minor/major), label, value, needle, and range via attributes.
* Background Images: Style gauge faces with textures or images, clipped to the gauge shape (concept credit: [tsoriano64](https://github.com/tsoriano64/MicroGauge)). 
* Cross-Platform Compatibility: Works with all the platforms that [SkiaSharp](https://github.com/mono/SkiaSharp) supports. 
* Framework: MAUI, WPF, and Blazor libraries are provided with tags and bindings for customization (.NET 10, SkiaSharp 4).

---
## Gallery
![Gallery](https://github.com/galenblakeman/MicroGauge/blob/master/Doc/Gallery.png)

---
## Nuget Packages
[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge?style=flat-square&logo=nuget&label=MicroGauge)](https://www.nuget.org/packages/MicroGauge/)

[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge.Forms?style=flat-square&logo=nuget&label=MicroGauge.Forms)](https://www.nuget.org/packages/MicroGauge.Forms/) *(legacy, frozen at 1.0.1.x)*

[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge.Maui?style=flat-square&logo=nuget&label=MicroGauge.Maui)](https://www.nuget.org/packages/MicroGauge.Maui/)

[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge.Wpf?style=flat-square&logo=nuget&label=MicroGauge.Wpf)](https://www.nuget.org/packages/MicroGauge.Wpf/)

[![NuGet Version](https://img.shields.io/nuget/v/MicroGauge.Blazor?style=flat-square&logo=nuget&label=MicroGauge.Blazor)](https://www.nuget.org/packages/MicroGauge.Blazor/)

---
## Tag Reference

* [Radial Gauge](https://github.com/galenblakeman/MicroGauge/blob/master/Doc/RadialGaugeTag.md)
* [Linear Gauge](https://github.com/galenblakeman/MicroGauge/blob/master/Doc/LinearGaugeTag.md)

---
##  Xamarin.Forms (Legacy)

Xamarin.Forms reached end-of-life in May 2024. The [MicroGauge.Forms](https://www.nuget.org/packages/MicroGauge.Forms/) package remains available on NuGet frozen at version 1.0.1.x (SkiaSharp 2.x) for legacy applications, but it is no longer part of this repository and will not receive updates. See the [1.0.1.1 tag](https://github.com/galenblakeman/MicroGauge/tree/f2c6732) for its source and examples.

---

##  .Net MAUI Usage
###  Nuget Package Install

```Dotenv
dotnet add package MicroGauge.Maui
```

###  Namespace Attribute

```XML
xmlns:microGauge="clr-namespace:MicroGauge.Maui;assembly=MicroGauge.Maui"
```

###  XAML

```XML
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

```XML
xmlns:microGauge="clr-namespace:MicroGauge.Wpf;assembly=MicroGauge.Wpf"
```

###  XAML

```HTML
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
---

##  Blazor Usage
###  Nuget Package Install

```Dotenv
dotnet add package MicroGauge.Blazor
```

###  Namespace Using 

```XML
@using MicroGauge.Blazor
```

###  Razor

```XML
<BlazorGaugeRadial Value="15" NeedleBrush="GaugeBrushes.Green" />
<BlazorGaugeLinear Value="15" NeedleBrush="GaugeBrushes.Green" />
```

###  C#

```C#
BlazorGaugeRadial radialGauge = new BlazorGaugeRadial
{
    Value = 15.0, NeedleBrush = GaugeBrushes.Green
};
BlazorGaugeLinear linearGauge = new BlazorGaugeLinear
{
    Value = 15.0, NeedleBrush = GaugeBrushes.Green
};
}
```

###  Examples
* Project: [MicroGauge.Example.Blazor](https://github.com/galenblakeman/MicroGauge/tree/master/Example/MicroGauge.Example.Blazor)
* Bindings: [Home.razor](https://github.com/galenblakeman/MicroGauge/blob/develop/Example/MicroGauge.Example.Blazor/Pages/Home.razor)  

## License
MIT © Galen Blakeman
