using ConsoleProgressBar;

namespace ConsoleExample.Examples;

internal sealed class DefaultColorsExample(string name, ExampleBase header) : ExampleBase(name, header)
{
	protected override void ImplementExample()
	{
		Console.WriteLine(Name);
		Console.WriteLine("------------------------------------------------------------");
		Console.WriteLine();

		IEnumerable<ConsoleProgressHandler<int>> bars = GetDefaultColors()
			.Select(colors => ExampleCollection.ConsoleProgress($"[{colors.Name}]".PadRight(20))
					.WithColors(colors)
					.WithMaxBarLength(60)
					);

		//--- parallel --------------------------------------------------------
		RunParallel(bars, ExampleCollection.Length);
	}
}
