
// ignore spelling: bg

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
public static class ConsoleProgressExtensions
{
	public static ConsoleProgressHandler<T> ConsoleProgress<T>(this IEnumerable<T> consoleProgress, string? action = null, string? item = null)
		=> new (consoleProgress, action, item);

	/// <summary>
	/// Sets the maximum progress bar length in characters.
	/// The actual bar might be shorter, if the console window is too narrow.
	/// </summary>
	/// <typeparam name="T">Enumeration item type</typeparam>
	/// <param name="consoleProgress">The <see cref="ConsoleProgressHandler{T}"/> object</param>
	/// <param name="maxBarLength">Maximum bar length</param>
	/// <returns>The <see cref="ConsoleProgressHandler{T}"/> object for fluent-API like command concatenation.</returns>
	public static ConsoleProgressHandler<T> WithMaxBarLength<T>(this ConsoleProgressHandler<T> consoleProgress, int maxBarLength)
	{
		consoleProgress.MaxBarLength = maxBarLength;
		return consoleProgress;
	}

	public static ConsoleProgressHandler<T> WithColor<T>(this ConsoleProgressHandler<T> consoleProgress, ConsoleColor activeBar)
	{
		consoleProgress.Colors.ActiveBar	= activeBar;
		consoleProgress.Colors.FractionBar	= activeBar;
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

	public static ConsoleProgressHandler<T> WithColors<T>(this ConsoleProgressHandler<T> consoleProgress, ConsoleProgressColors preset)
	{
		consoleProgress.Colors = preset;
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
