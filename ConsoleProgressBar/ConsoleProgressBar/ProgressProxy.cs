using System.Collections;
using System.Diagnostics.CodeAnalysis;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
public abstract class ProgressProxy<T>(IEnumerable<T> collection, string? action = null, string? item = null)
	: IEnumerable<T>
{
	//-------------------------------------------------------------------------------------------------------------
	#region Fields

	private readonly IEnumerable<T> _collection = collection ?? throw new ArgumentNullException(nameof(collection));

	#endregion Fields

	//-------------------------------------------------------------------------------------------------------------
	#region Properties

	internal IConsole Console
		{ get; set; } = new ConsoleReal();

	internal int? TotalSteps
		{ get; set; }

	protected string? ActionDesc
		{ get; private set; } = action;

	protected string? ItemDesc
		{ get; private set; } = item;

	/// <summary>
	/// Mainly for debugging purposes, to prematurely
	/// cancel the iteration after a certain number of steps.
	/// </summary>
	public int? CancelAfter
		{ get; internal set; }

	#endregion Properties

	//-------------------------------------------------------------------------------------------------------------
	#region Methods

	[MemberNotNull(nameof(TotalSteps))]
	internal void UpdateTotalStepsIfUnset()
		=> TotalSteps ??= Math.Max(0, _collection.Count());

	[MemberNotNull(nameof(TotalSteps))]
	protected virtual void InitProgress()
		=> UpdateTotalStepsIfUnset();

	protected abstract void UpdateProgress(int stepNum, double progress, T? item);

	protected abstract void FinishProgress();

	/// <summary>Calculates the progress of a process as a fraction of completion.</summary>
	/// <remarks>Cannot be called if <see cref="TotalSteps"/> is not set or null.</remarks>
	/// <param name="stepNum">The current step number, which must be a non-negative integer.</param>
	/// <returns>A double value representing the progress, clamped between 0.0 and 1.0.</returns>
	private double GetProgress(int stepNum)
		=> double.Clamp(stepNum / (double)TotalSteps!, 0.0, 1.0);

	/// <summary>
	/// If <see cref="CancelAfter"/> is set, the enumeration will be limited to this number of items.
	/// There is a special logic in place to handle (EntityFramework) queries.
	/// </summary>
	/// <returns>New enumerable with the actual number of items to iterate over</returns>
	private IEnumerable<T> GetActualEnumeration()
	{
		//--- [AsQueryable] is important when the enumerable is a EntityFramework query ---
		//--- otherwise the query will be executed completely even if the foreach loop is canceled ---
		if (CancelAfter.HasValue)
			return _collection.AsQueryable().Take(CancelAfter.Value);

		else
			return _collection;
	}

	#endregion Methods

	//-------------------------------------------------------------------------------------------------------------
	#region IEnumerable

	public IEnumerator<T> GetEnumerator()
	{
		InitProgress();
		int stepNum = 0;

		foreach (T item in GetActualEnumeration())
		{
			//--- pass-through the item ---
			yield return item;

			//--- report progress ---
			UpdateProgress(++stepNum, GetProgress(stepNum), item);
		}

		FinishProgress();
		yield break;
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	#endregion IEnumerable
}
