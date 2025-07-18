# Console Progress Bar

## Current Status

[![Current Repository Status](https://github.com/3rikF/ConsoleProgress/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/3rikF/ConsoleProgress/actions) 
[![Current Repository Status](https://github.com/3rikF/ConsoleProgress/actions/workflows/dotnet-linux.yml/badge.svg)](https://github.com/3rikF/ConsoleProgress/actions) 
[![Codecov Test Coverage](https://codecov.io/gh/3rikF/ConsoleProgress/graph/badge.svg?token=6DBLGNQC73)](https://codecov.io/gh/3rikF/ConsoleProgress) 
[![WakaTime Tracking](https://wakatime.com/badge/user/ccce5eac-49f0-481f-998c-1183a3cd0b18/project/22dc24f9-97a1-4b21-a674-e2b3a8c44b91.svg?Style=flat)](https://wakatime.com/badge/user/ccce5eac-49f0-481f-998c-1183a3cd0b18/project/22dc24f9-97a1-4b21-a674-e2b3a8c44b91)
[![NuGet](https://img.shields.io/nuget/v/ErikForwerk.ConsoleTools.ProgressBar.svg)](https://www.nuget.org/packages/ErikForwerk.ConsoleTools.ProgressBar/)

## Table of Contents
- [Console Progress Bar](#console-progress-bar)
	- [Table of Contents](#table-of-contents)
	- [Basics](#basics)
	- [Styling](#styling)
	- [Colors](#colors)
	- [Random Examples](#random-examples)
	- [Additional Features](#additional-features)

## Basics

This project provides functions to easily and conveniently display a progress bar in a C# console window when iterating over an enumeration. (similar to `tqdm` in python)

This is a private project and was mainly done for fun and learning purposes. It is not intended to be used in production code.

```csharp
using ConsoleProgressBar;

int[] numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

foreach (int number in numbers.ConsoleProgress())
{
	// Do something with the number
}
```

## Styling

There are different predefined styles and color sets that can be used to change the appearance of the progress bar. All default styles can be accessed through the `ConsoleProgressStyle` class.

```csharp
foreach (int number in numbers
	.ConsoleProgress()
	.WithStyle(ConsoleProgressStyle.Millennium) )
{
	// Do something with the number
}
```

![Default Styles Example GIF](https://raw.githubusercontent.com/3rikF/ConsoleProgress/main/Readme/1_default_styles.gif)

Of course it is possible to define own custom styles using the `ConsoleProgressStyle` class.

```csharp
var customStyle = new ConsoleProgressStyle(true, true, ">>", "<<", '*', '#', FractionsSets.AnimationCircle),

foreach (int number in numbers
	.ConsoleProgress()
	.WithStyle(customStyle) )
{
	// Do something with the number
}
```
![Custom Styles Example GIF](https://raw.githubusercontent.com/3rikF/ConsoleProgress/main/Readme/2_custom_styles.gif)

## Colors

You can apply different color sets to the progress bar using the `.WithColors(…)`. All default color sets can be accessed through the `ConsoleProgressColors` class.

```csharp
foreach (int number in numbers
	.ConsoleProgress()
	.WithColors(ConsoleProgressColors.Red) )
{
	// your code here
}
```

![Default Colors Example GIF](https://raw.githubusercontent.com/3rikF/ConsoleProgress/main/Readme/3_default_colors.gif)

Colors can also be customized using the `.WithColor(…)` and `.WithBgColor(…)` extension methods.

```csharp
foreach (int number in numbers
	.ConsoleProgress()
	.WithColor(ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkGray)
	.WithBgColor(ConsoleColor.Red) )
{
	// your code here
}
```

## Random Examples

The example console application `ConsoleExample` shows some random combinations of the available styles and colors for the progress bar.

![Default Colors Example GIF](https://raw.githubusercontent.com/3rikF/ConsoleProgress/main/Readme/4_random_examples.gif)

## Additional Features

When iterating over huge external data (e.g. entity framework table rows) it might be useful to provide the progress bar with the total number of elements to be processed. This can be done by providing the total number of elements with `.WithPreCount(…)` extension method.
This will prevent the progress bar from trying to count the elements itself which all items to be iterated over which in turn will lead to executing the DB query twice.

For debugging it might be useful to cancel the iteration after a certain number of elements. This can be done by providing the maximum number of elements with `.CancelAfter(…)` extension method.

For testing purposes, there is the extension method `.WithDebugMode()` which will prevent the progress bar from trying to read the window size and cursor positions which might not work in the testing environment.
