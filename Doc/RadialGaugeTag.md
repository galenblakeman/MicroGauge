# Radial Gauge Tag

## Layout (Positioning and padding)
* RadialStyle - 0 = Full, 1 = Half, 2 = Notch
* TopExtent - Top padding extent of surface height
* BottomExtent - Bottom padding extent of surface height
* LeftExtent - Left padding extent of surface width
* RightExtent - Right padding extent of surface width

## Backing (Background of gauge)
* BackingBrush - Backing solid or linear gradient brush
* BackingOutlineBrush - Backing outline solid or linear gradient brush
* BackingStrokeWidth - Stroke with for backing edge

## Scale (Angle and range)
* ScaleStartAngle - start angle of scale
* ScaleEndAngle - stop angle of scale
* MinValue - Min value that corresponds to start
* MaxValue - Max value that corresponds to start
* Value - Current Value for scale

## Tick (Major and Minor for scale)
* TickBrush - Tick solid or linear gradient brush
* MinorTickBrush - Minor tick solid or linear gradient brush
* TickStrokeWidth - Tick width
* MinorTickStrokeWidth - Minor tick width
* TickInterval - Tick interval for scale
* MinorTickInterval - Minor tick interval for scale
* TickStartExtent - Tick start extent of radius
* TickEndExtent - Tick end extent of radius
* MinorTickStartExtent - Minor tick start extent of radius
* MinorTickEndExtent - Minor tick end extent of radius

## Label (Number for scale)
* LabelFormatString - Format string applied to label values, ex. "{0:N0}"
* LabelFontSize - Label font size
* LabelFontBrush - Label font solid or linear gradient brush
* LabelFontFamily - Label font family name
* LabelFontWeight - Label SKFontStyleWeight (Light, Normal, Bold, etc.)
* LabelInterval - Label interval

## Value (Current Value Display)
* ValueLocation - Value location on gauge (TopCenter, BottomCenter, LeftCenter, RightCenter)
* ValueFormatString - Format string applied to Value values, ex. "{0:N0}"
* ValueFontSize - Value font size
* ValueFontBrush - Value font solid or linear gradient brush
* ValueFontFamily - Value font family name
* ValueFontWeight - Value SKFontStyleWeight (Light, Normal, Bold, etc.)
* ValueExtent - Value extent from center in direction of value location

## Needle (Main Value Needle)
* NeedleBrush - Needle solid or linear gradient brush
* NeedleStartWidth - Width of needle at start
* NeedleEndWidth - Width of needle at end
* NeedleStartExtent - Needle start as extent of width
* NeedleEndExtent - Needle end as extent of width
* NeedlePivotEndExtent - Pivot end extent of radius
* NeedlePivotBrush - Solid or linear gradient brush for of needle pivot
* NeedlePivotOutlineWidth - width of needle pivot outline
* NeedleOutlineWidth - Width of needle outline
* NeedleOutlineBrush  Needle outline solid or linear gradient shader


## Set Needle (Indicator Needle)
* SetNeedleValue - Set Needle Value
* SetNeedleBrush - set needle solid or linear gradient brush
* SetNeedleStartWidth - Width of set needle at start
* SetNeedleEndWidth - Width of set needle at end
* SetNeedleStartExtent - Set needle start as extent of width
* SetNeedleEndExtent - Set needle end as extent of width
* SetNeedleOutlineWidth - Width of set needle outline
* SetNeedleOutlineBrush  - Set needle outline solid or linear gradient shader

## Range (Shown behind scale)
* RangeBrush - Solid or linear gradient brush drawn behind tick scale
* RangeInnerStartExtent - Drawing range inner boundary start at extent of radius
* RangeInnerEndExtent - Drawing range inner boundary end at extent of radius
* RangeOuterStartExtent - Drawing range outer boundary start at extent of radius
* RangeOuterEndExtent - Drawing range outer boundary end at extent of radius


## Ranges (Multiple sections behind scale)
* Ranges - List of GaugeRadialRange
* GaugeRadialRange: StartValue - Value where range starts
* GaugeRadialRange: EndValue - Value where range ends
* GaugeRadialRange: InnerStartExtent - Drawing range inner boundary start at extent of radius
* GaugeRadialRange: InnerEndExtent - Drawing range inner boundary end at extent of radius
* GaugeRadialRange: OuterStartExtent - Drawing range outer boundary start at extent of radius
* GaugeRadialRange: OuterEndExtent - Drawing range outer boundary end at extent of radius
* GaugeRadialRange: Brush - Guage Brush Color
* GaugeRadialRange: BrushHex - Hex Color Code to convert to Guage Brush