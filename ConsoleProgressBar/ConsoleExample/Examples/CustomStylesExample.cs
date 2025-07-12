
using ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleExample.Examples;

//-----------------------------------------------------------------------------------------------------------------------------------------
internal sealed class CustomStylesExample(string name, ExampleBase header) : ExampleBase(name, header)
{
	private readonly ConsoleProgressStyle[] _customProgressStyles =
	[
		new ConsoleProgressStyle(true, true, ">>", "<<", '*', '#', FractionsSets.AnimationCircle),
		new ConsoleProgressStyle(true, true, " |", "| ", '_', 'Z', ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z']),
		new ConsoleProgressStyle(true, true, " |", "| ", '>', '>', [' ', '>', ' ', '>']),
	];

	protected override void ImplementExample()
	{
		Console.WriteLine(Name);
		Console.WriteLine("------------------------------------------------------------");
		Console.WriteLine();

		IEnumerable<ConsoleProgressHandler<int>> bars = _customProgressStyles
			.Select(style => ExampleCollection.ConsoleProgress($"[Custom Style]".PadRight(20))
			.WithStyle(style)
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
