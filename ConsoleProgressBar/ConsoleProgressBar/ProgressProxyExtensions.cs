
//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
public static class ProgressProxyExtensions
{
	/// <summary>
	/// Prevents the call to [Count()] on the enumerable collection,
	/// wich, depending on the implementation or source, may be very slow
	/// and/or needs to load all data from a database for example.
	/// </summary>
	/// <typeparam name="T">Element Type</typeparam>
	/// <param name="progress">The <see cref="ProgressProxy{T}"/> object</param>
	/// <param name="preCountElements">The number of pre-count elements in the collection</param>
	/// <returns>The <see cref="ProgressProxy{T}"/> object for fluent API like calls</returns>
	public static ProgressProxy<T> WithPreCount<T>(this ProgressProxy<T> progress, int preCountElements)
	{
		progress.TotalSteps = preCountElements;
		return progress;
	}

	/// <summary>
	/// Cancels the iteration after a certain number of steps.
	/// This is probably mainly for debugging purposes.
	/// </summary>
	/// <typeparam name="T">ElementType</typeparam>
	/// <param name="progress">The <see cref="ProgressProxy{T}"/> object</param>
	/// <param name="cancelAfter">Number of iteration steps to cancel the iteration after</param>
	/// <returns>The <see cref="ProgressProxy{T}"/> object for fluent API like calls</returns>
	public static ProgressProxy<T> CancelAfter<T>(this ProgressProxy<T> progress, int cancelAfter)
	{
		progress.CancelAfter = cancelAfter;
		return progress;
	}

	internal static ProgressProxy<T> WithDebugMode<T>(this ProgressProxy<T> progress)
	{
		progress.Console = new ConsoleTest();
		return progress;
	}
}
