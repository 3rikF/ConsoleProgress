using System.Collections;

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

	internal bool DebugFlag
		{ get; set; }

	#endregion Properties

	//-------------------------------------------------------------------------------------------------------------
	#region Methods

	internal void UpdateTotalStepsIfUnset()
	{
		if (TotalSteps is null)
		{
			TotalSteps	= _collection.TryGetNonEnumeratedCount(out int count) ? count : _collection.Count();
			TotalSteps	= Math.Max(1, TotalSteps.Value);
		}
	}

	protected virtual void InitProgress()
	{
		UpdateTotalStepsIfUnset();
	}

	protected abstract void UpdateProgress(int stepNum, double progress, T? item);

	protected abstract void FinishProgress();

	private double GetProgress(int stepNum)
	{
		if (TotalSteps is null)
			return 0D;

		else if (TotalSteps == 0)
			return 1D;

		else
			return Math.Max(0D, Math.Min(1D, stepNum / (double)TotalSteps));
	}

	private bool ShouldCancel(int stepNum)
		=> CancelAfter.HasValue && stepNum >= CancelAfter;

	#endregion Methods

	//-------------------------------------------------------------------------------------------------------------
	#region IEnumerable

	public IEnumerator<T> GetEnumerator()
	{
		InitProgress();
		int stepNum = 0;

		foreach (T item in _collection)
		{
			//--- pass-through the item ---
			yield return item;

			//--- report progress ---
			UpdateProgress(++stepNum, GetProgress(stepNum), item);

			//--- premature canceling the iteration (see extension .CancelAfter(), mainly for debugging purposes) ---
			if (ShouldCancel(stepNum))
				break;
		}

		FinishProgress();
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	#endregion IEnumerable
}
