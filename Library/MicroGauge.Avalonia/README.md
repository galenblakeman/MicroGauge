# MicroGauge.Avalonia

Avalonia radial and linear controls backed by the MicroGauge SkiaSharp renderer. This package targets .NET 10 and Avalonia 12.1.4 and requires Avalonia's Skia renderer. The project explicitly references SkiaSharp 4.150.1; keep the host app's SkiaSharp reference on the same version.

```xml
<PackageReference Include="MicroGauge.Avalonia" Version="1.1.0.1" />
<PackageReference Include="SkiaSharp" Version="4.150.1" />
```

Add the namespace to an Avalonia window or user control:

```xml
xmlns:g="clr-namespace:MicroGauge.Avalonia;assembly=MicroGauge.Avalonia"
```

```xml
<g:AvaloniaGaugeRadial Width="240" Height="240"
                       Value="{Binding Speed}"
                       NeedleBrush="#008000"
                       NeedleAnimationDuration="250" />
<g:AvaloniaGaugeLinear Width="320" Height="120"
                       Value="{Binding Speed}"
                       ValueBarBrush="#008000" />
```

Both controls expose the shared gauge settings as Avalonia styled properties, including brushes, labels, ticks, needles, backing, and a background image. Radial and linear properties match their WPF and MAUI counterparts. Avalonia `IBrush` values support solid and linear gradients. `BackgroundImageSource` accepts an Avalonia `Bitmap`; `BackgroundImagePath` resolves relative to the app base directory. A programmatic `BackgroundImage` is caller-owned.

`Ranges` accepts a list of `GaugeRadialRange` or `GaugeLinearRange`. Replacing the list updates the control; `ObservableCollection<T>` also updates the control when items are added or removed. Call `InvalidateGauge()` after changing an existing range item or a mutable brush in place. For a single radial range, `RangeBrush` and `Range*Extent` properties are convenient alternatives.

See `Example/MicroGauge.Example.Avalonia` for bindings, radial styles, orientation, ranges, and images.
