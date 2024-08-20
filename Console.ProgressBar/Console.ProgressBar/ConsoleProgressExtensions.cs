// ignore spelling: bg

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace Console.ProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
public static class ConsoleProgressExtensions
{
	public static ConsoleProgressHandler<T> WithColors<T>(this ConsoleProgressHandler<T> consoleProgress, ConsoleColor color)
	{
		consoleProgress.Colors.ActiveBar	= color;
		return consoleProgress;
	}

	public static ConsoleProgressHandler<T> WithColor<T>(this ConsoleProgressHandler<T> consoleProgress, ConsoleColor activeBar, ConsoleColor fractionBar)
	{
		consoleProgress.Colors.ActiveBar	= activeBar;
		consoleProgress.Colors.FractionBar	= fractionBar;
		return consoleProgress;
	}

	public static ConsoleProgressHandler<T> WithColor<T>(this ConsoleProgressHandler<T> consoleProgress, ConsoleColor activeBar, ConsoleColor fractionBar, ConsoleColor inactiveBar)
	{
		consoleProgress.Colors.ActiveBar	= activeBar;
		consoleProgress.Colors.FractionBar	= fractionBar;
		consoleProgress.Colors.InactiveBar	= inactiveBar;
		return consoleProgress;
	}

	public static ConsoleProgressHandler<T> WithBgColor<T>(this ConsoleProgressHandler<T> consoleProgress, ConsoleColor color)
	{
		consoleProgress.Colors.Background	= color;
		return consoleProgress;
	}

	public static ConsoleProgressHandler<T> WithStyle<T>(this ConsoleProgressHandler<T> consoleProgress, ConsoleProgressStyle style)
	{
		consoleProgress.Style = style;
		return consoleProgress;
	}

	public static ConsoleProgressHandler<T> WithNewLine<T>(this ConsoleProgressHandler<T> consoleProgress, bool startNewLine=true)
	{
		consoleProgress.StartAtNewLine		= startNewLine;
		return consoleProgress;
	}
}
