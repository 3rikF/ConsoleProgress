using ConsoleProgressBar;

namespace ConsoleExample.Examples;

internal sealed class RandomColorsandStyleExample(string name, ExampleBase header) : ExampleBase(name, header)
{
	private const int NUM_BARS = 10;

	private static readonly Random _random = new(DateTime.Now.Microsecond);

	private readonly ConsoleProgressStyle[] _defaultStyles	= GetDefaultStyles();
	private readonly ConsoleProgressColors[] _defaultColors	= GetDefaultColors();

	private static T RandomElement<T>(T[] array)
		=> array[_random.Next(array.Length)];

	protected override void ImplementExample()
	{
		Console.WriteLine(Name);
		Console.WriteLine("------------------------------------------------------------");
		Console.WriteLine();

		IEnumerable<ConsoleProgressHandler<int>> bars = Enumerable.Range(0, 10)
			.Select(_ => (style:RandomElement(_defaultStyles), colors:RandomElement(_defaultColors)))
			.Select(it => ExampleCollection.ConsoleProgress($"[{it.style.Name} x {it.colors.Name}]".PadRight(30))
				.WithStyle(it.style)
				.WithColors(it.colors)
				.WithMaxBarLength(60)
			);

		//--- sequential ------------------------------------------------------
		//foreach (ConsoleProgressHandler<int> bar in bars)
		//{
		//	foreach (int i in bar)
		//		DelayStep(i);
		//}

		//--- parallel --------------------------------------------------------
		RunParallel(bars, ExampleCollection.Length);
	}
}
