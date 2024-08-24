using ConsoleProgressBar;

namespace ConsoleExample.Examples;

internal sealed class DefaultStylesExample(string name, ExampleBase header) : ExampleBase(name, header)
{
	protected override void ImplementExample()
	{
		Console.WriteLine(Name);
		Console.WriteLine("------------------------------------------------------------");
		Console.WriteLine();

		IEnumerable<ConsoleProgressHandler<int>> bars = GetDefaultStyles()
			.Select(style => ExampleCollection.ConsoleProgress($"[{style.Name}]".PadRight(20))
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
