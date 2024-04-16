# Linear Gauge Tag

## Layout (Positioning and padding)
* IsVertical - True draw bar vertical, else horizontal
* ValueWidthExtent - Percent of surface width for value bar
* TopExtent - Top padding extent of surface height
* BottomExtent - Bottom padding extent of surface height
* LeftExtent - Left padding extent of surface width
* RightExtent - Right padding extent of surface width

## Backing (Background of gauge)
* BackingBrush - Backing solid or linear gradient brush
* BackingOutlineBrush - Backing outline solid or linear gradient brush
* BackingStrokeWidth - Stroke with for backing edge

## Scale (Angle and range)
* MinValue - Min value that corresponds to start
* MaxValue - Max value that corresponds to start
* Value - Current Value for scale

## Tick (Major and Minor for scale)
* TickWidthExtent - Percent of bar width for major ticks
* MinorTickWidthExtent - Percent of bar width for minor ticks
* TickBrush - Tick solid or linear gradient brush
* MinorTickBrush - Minor tick solid or linear gradient brush
* TickStrokeWidth - Tick width
* MinorTickStrokeWidth - Minor tick width
* TickInterval - Tick interval for scale
* MinorTickInterval - Minor tick interval for scale

## Label (Number for scale)
* LabelFormatString - Format string applied to label values, ex. "{0:N0}"
* LabelFontSize - Label font size
* LabelFontBrush - Label font solid or linear gradient brush
* LabelFontFamily - Label font family name
* LabelFontWeight - Label SKFontStyleWeight (Light, Normal, Bold, etc.)
* LabelInterval - Label interval
* LabelExtent - Label extent from center

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

## Set Needle (Indicator Needle)
* SetNeedleValue - Set Needle Value
* SetNeedleBrush - set needle solid or linear gradient brush
* SetNeedleStartWidth - Width of set needle at start
* SetNeedleEndWidth - Width of set needle at end
* SetNeedleStartExtent - Set needle start as extent of width
* SetNeedleEndExtent - Set needle end as extent of width