﻿
// ignore spelling: bg

using System.Text;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace Console.ProgressBar;

// TODO: Why is this necessary? Why has it to be done *after* the namespace declaration?
using Console = System.Console;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class ConsoleProgressHandler<T>(IEnumerable<T> collection, string? action = null, string? item = null)
	: ProgressProxy<T>(collection, action, item)
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Fields

	private int _topPos;
	private int _lastProgress = -1;

	#endregion Fields

	//-----------------------------------------------------------------------------------------------------------------
	#region Properties

	public bool StartAtNewLine
		{ get; internal set; } = false;

	public int? PreCountElements
		{ get; internal set; } = null;

	public ConsoleProgressStyle Style
		{ get; internal set; } = ConsoleProgressStyle.Default;

	public ConsoleProgressColors Colors
		{ get; internal set; } = ConsoleProgressColors.Default;

	#endregion Properties

	//-----------------------------------------------------------------------------------------------------------------
	#region Methods

	//--- only for satisfying the IOS-principle (IOSP) ---
	private static void NewLine()
		=> Console.WriteLine();

	private static void ClearConsolLine(int topPos)
	{
		int width = Console.WindowWidth;
		Console.SetCursorPosition(0, topPos);
		Console.Write(new string(' ', width));
	}

	private void PrintProgress(int stepNum, double progress)
	{
		int consoleWidth = Console.WindowWidth;

		//---------------------------------------------------------------------
		StringBuilder sbCaption = new ();

		if (!string.IsNullOrEmpty(ActionDesc))
		{
			_ = sbCaption
				.Append(ActionDesc)
				.Append(':');
		}

		//--- left frame ---
		if (Style.showFrame)
		{
			_ =	sbCaption
				.Append(Style.frameLeft)
				;
		}

		//---------------------------------------------------------------------
		StringBuilder sbEnding = new ();

		//--- right frame ---
		if (Style.showFrame)
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
		int barSpace	= consoleWidth -caption.Length -ending.Length;

		//---------------------------------------------------------------------
		double progressWidthF	= barSpace * progress;
		int progressWidth		= (int)Math.Floor(progressWidthF);

		byte fractionIndex		= (byte)((progressWidthF - progressWidth) * Style.progressCharFractions.Length);
		bool hasFraction		= Style.showFractions && progressWidth < barSpace && Style.progressCharFractions.Length > 0;

		_lastProgress = progressWidth;

		//---------------------------------------------------------------------
		Console.SetCursorPosition(0, _topPos);
		Console.Write($"{caption}");

		ConsoleColor oldColorFG	= Console.ForegroundColor;
		ConsoleColor oldColorBG = Console.BackgroundColor;

		try
		{
			//--- print finished part ---
			Console.BackgroundColor = Colors.Background;
			Console.ForegroundColor	= Colors.ActiveBar;

			Console.Write(new string(Style.charDone, _lastProgress));

			if (hasFraction)
			{
				Console.ForegroundColor	= Colors.FractionBar;
				Console.Write(Style.progressCharFractions[fractionIndex]);
			}

			//--- print unfinished part ---
			Console.ForegroundColor	= Colors.InactiveBar;
			Console.Write(new string(Style.charEmpty, barSpace -_lastProgress -(hasFraction ? 1 : 0)));
		}

		finally
		{
			Console.ForegroundColor	= oldColorFG;
			Console.BackgroundColor = oldColorBG;
		}

		Console.Write(ending);
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