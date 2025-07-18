
// ignore spelling: bg

using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("ConsoleProgressBarTests")]

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class ConsoleProgressHandler<T> : ProgressProxy<T>
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Fields

	private int _topPos;
	private int _lastProgress = -1;

	#endregion Fields

	//-------------------------------------------------------------------------------------------------------------
	#region Construction

	public ConsoleProgressHandler(IEnumerable<T> collection, string? action, string? item)
		: base(collection, action, item)
	{
		if (Console.OutputEncoding != Encoding.UTF8)
			Console.OutputEncoding = Encoding.UTF8;
	}

	public ConsoleProgressHandler(IEnumerable<T> collection)
		: base(collection)
	{
		if (Console.OutputEncoding != Encoding.UTF8)
			Console.OutputEncoding = Encoding.UTF8;
	}

	#endregion Construction

	//-----------------------------------------------------------------------------------------------------------------
	#region Properties

	public bool StartAtNewLine
		{ get; internal set; } = false;

	public int MaxBarLength
		{ get; internal set; } = -1;

	public ConsoleProgressStyle Style
		{ get; internal set; } = ConsoleProgressStyle.Default;

	public ConsoleProgressColors Colors
		{ get; internal set; } = ConsoleProgressColors.Default;

	#endregion Properties

	//-----------------------------------------------------------------------------------------------------------------
	#region Methods

	//--- only for satisfying the IOS-principle (IOSP) ---
	private void NewLine()
		=> Console.WriteLine();

	private void ClearConsolLine(int topPos)
	{
		Console.SetCursorPosition(0, topPos);
		Console.Write(new string(' ', Console.WindowWidth));
	}

	private void PrintProgress(int stepNum, double progress)
	{

		//---------------------------------------------------------------------
		StringBuilder sbCaption = new ();

		if (!string.IsNullOrEmpty(ActionDesc))
		{
			_ = sbCaption
				.Append(ActionDesc)
				.Append(':');
		}

		//--- left frame ---
		if (Style.ShowFrame)
		{
			_ =	sbCaption
				.Append(Style.FrameLeft)
				;
		}

		//---------------------------------------------------------------------
		StringBuilder sbEnding = new ();

		//--- right frame ---
		if (Style.ShowFrame)
		{
			_ = sbEnding
				.Append(Style.FrameRight)
				;
		}

		_ = sbEnding
			.Append($" [{stepNum}/{TotalSteps}, ")
			.Append($"{progress:P0}]")
			;

		//---------------------------------------------------------------------
		string caption	= sbCaption.ToString();
		string ending	= sbEnding.ToString();
		int barSpace	= Console.WindowWidth -caption.Length -ending.Length;

		if (MaxBarLength > 0)
			barSpace = int.Min(MaxBarLength, barSpace);

		//---------------------------------------------------------------------
		double progressWidthF	= barSpace * progress;
		int progressWidth		= (int)Math.Floor(progressWidthF);

		byte fractionIndex		= (byte)((progressWidthF - progressWidth) * Style.ProgressCharFractions.Length);
		bool hasFraction		= Style.ShowFractions && progressWidth < barSpace && Style.ProgressCharFractions.Length > 0;

		_lastProgress = progressWidth;

		//---------------------------------------------------------------------
		Console.CursorVisible = false;
		Console.SetCursorPosition(0, _topPos);

		Console.Write(caption);

		ConsoleColor oldColorFG	= Console.ForegroundColor;
		ConsoleColor oldColorBG = Console.BackgroundColor;

		try
		{
			//--- print finished part ---
			Console.BackgroundColor = Colors.Background;
			Console.ForegroundColor	= Colors.ActiveBar;

			Console.Write(new string(Style.CharDone, _lastProgress));

			if (hasFraction)
			{
				Console.ForegroundColor	= Colors.FractionBar;
				Console.Write(Style.ProgressCharFractions[fractionIndex]);
			}

			//--- print unfinished part ---
			Console.ForegroundColor	= Colors.InactiveBar;
			Console.Write(new string(Style.CharEmpty, barSpace -_lastProgress -(hasFraction ? 1 : 0)));
		}

		finally
		{
			Console.ForegroundColor	= oldColorFG;
			Console.BackgroundColor = oldColorBG;
		}

		Console.Write(ending);
		Console.CursorVisible = true;
	}

	#endregion Methods

	//-----------------------------------------------------------------------------------------------------------------
	#region IProgressHandler

	protected override void InitProgress()
	{
		base.InitProgress();

		//--- get current cursor pos from console ---
		if (StartAtNewLine)
			NewLine();

		_topPos = Console.CursorTop;

		//--- clear whole console line  ---
		ClearConsolLine(_topPos);

		//--- print progress ---
		PrintProgress(0, 0D);
	}

	protected override void FinishProgress()
		=> Console.WriteLine();

	protected override void UpdateProgress(int stepNum, double progress, T? item)
		=> PrintProgress(stepNum, progress);

	#endregion IProgressHandler
}