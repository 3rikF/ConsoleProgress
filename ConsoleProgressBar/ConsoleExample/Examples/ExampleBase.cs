using System.Reflection;

using ConsoleProgressBar;

namespace ConsoleExample.Examples;

internal abstract class ExampleBase(string name, ExampleBase? header)
{
	//--- Fields ------------------------------------------------------------------------------------------------------
	private const int MAX_ITEMS = 1000;

	private readonly ExampleBase? _header = header;
	private readonly int[] _exampleCollection = Enumerable.Range(0, MAX_ITEMS).ToArray();


	//--- Construction ------------------------------------------------------------------------------------------------
	public void RunExample()
	{
		_header?.ImplementExample();
		ImplementExample();
	}

	//--- Properties --------------------------------------------------------------------------------------------------
	protected int[] ExampleCollection
		=> _exampleCollection;

	public string Name
		=> name;

	//--- Methods -----------------------------------------------------------------------------------------------------
	protected abstract void ImplementExample();


	protected static void RunParallel(IEnumerable<ProgressProxy<int>> bars, int maxIterations)
	{
		//--- parallel --------------------------------------------------------
		IEnumerator<int>[] barsEnumerators = bars
			.Select(bar => bar.GetEnumerator())
			.ToArray();

		for (int i = 0; i <= maxIterations; i++)
		{
			foreach (IEnumerator<int>? bar in barsEnumerators)
			{
				bar.MoveNext();
				Console.WriteLine();
			}

			DelayStep(i);
		}
	}

	//--- Static Helper -----------------------------------------------------------------------------------------------

	protected static ConsoleProgressStyle[] GetDefaultStyles()
		=> typeof(ConsoleProgressStyle)
			.GetProperties(BindingFlags.Public | BindingFlags.Static)
			.Select(prop => (ConsoleProgressStyle)prop.GetValue(null)!)
			.ToArray();

	protected static ConsoleProgressColors[] GetDefaultColors()
		=> typeof(ConsoleProgressColors)
			.GetProperties(BindingFlags.Public | BindingFlags.Static)
			.Select(prop => (ConsoleProgressColors)prop.GetValue(null)!)
			.ToArray();

	/// <summary>
	/// Calculates a modified Gaussian function.
	/// This function will return values between 0 and 1.
	/// </summary>
	/// <param name="x">The support position</param>
	/// <param name="mu">The standard deviation (position of the maximum)</param>
	/// <param name="sigma">The variance (width of the curve)</param>
	/// <returns>Value between 0 an 1 for at the position of [x]</returns>
	// https://www.wolframalpha.com/input?i=plot+f%28x%29%3De%5E%28-%28x-m%29%5E2%2F%282*s%5E2%29%29+with+s+%3D+.1%2C+m+%3D+0.5+from+x+%3D++0+to+1%3B+y%3D0+to+1
	protected static double ModifiedGauss(double x, double mu = 0, double sigma = 1)
	{
		return Math.Exp(
			-Math.Pow(x - mu, 2)
			/
			(2 * sigma * sigma));
	}

	/// <summary>
	/// Unified calculation for the delay of the progress bar
	/// tho visualize the styles in a more fluent way.
	/// </summary>
	/// <param name="x"></param>
	/// <returns></returns>
	protected static int GaussDelayFunction(double x)
	{
		x = double.Clamp(x, 0, 1);

		double factor = ModifiedGauss(x, 0, 0.05);
		return int.Clamp((int)(100 * factor), 0, 1000);
	}

	protected static void DelayStep(int i)
	{
		int milliseconds = GaussDelayFunction(i / (double)MAX_ITEMS);
		Thread.Sleep(milliseconds);
	}
}
