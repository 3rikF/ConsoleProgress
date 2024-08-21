
namespace ConsoleProgressBar;

public record ConsoleProgressColors
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Fields

	private const ConsoleColor DEFAULT_COLOR_INACTIVE_BAR	= ConsoleColor.DarkGray;
	private const ConsoleColor DEFAULT_COLOR_BACKGROUND		= ConsoleColor.Black;

	#endregion Fields

	//-----------------------------------------------------------------------------------------------------------------
	#region Construction

	public ConsoleProgressColors(ConsoleColor activeBar)
	{
		ActiveBar	= activeBar;
		FractionBar = activeBar;
		InactiveBar	= DEFAULT_COLOR_INACTIVE_BAR;
		Background	= DEFAULT_COLOR_BACKGROUND;
	}

	public ConsoleProgressColors(ConsoleColor activeBar, ConsoleColor fractionBar)
	{
		ActiveBar	= activeBar;
		FractionBar = fractionBar;
		InactiveBar	= DEFAULT_COLOR_INACTIVE_BAR;
		Background	= DEFAULT_COLOR_BACKGROUND;
	}

	public ConsoleProgressColors(ConsoleColor activeBar, ConsoleColor fractionBar, ConsoleColor inactiveBar)
	{
		ActiveBar	= activeBar;
		FractionBar = fractionBar;
		InactiveBar	= inactiveBar;
		Background	= DEFAULT_COLOR_BACKGROUND;
	}

	public ConsoleProgressColors(ConsoleColor activeBar, ConsoleColor fractionBar, ConsoleColor inactiveBar, ConsoleColor background)
	{
		ActiveBar	= activeBar;
		FractionBar	= fractionBar;
		InactiveBar	= inactiveBar;
		Background	= background;
	}

	#endregion Construction

	//-----------------------------------------------------------------------------------------------------------------
	#region Presets

	public static ConsoleProgressColors Default			=> Cyan;
	public static ConsoleProgressColors Blue			=> new (ConsoleColor.Blue, ConsoleColor.Cyan);
	public static ConsoleProgressColors Green			=> new (ConsoleColor.DarkGreen, ConsoleColor.Green);
	public static ConsoleProgressColors Red				=> new (ConsoleColor.Red, ConsoleColor.Yellow);
	public static ConsoleProgressColors Yellow			=> new (ConsoleColor.Yellow, ConsoleColor.White);
	public static ConsoleProgressColors Cyan			=> new (ConsoleColor.DarkCyan, ConsoleColor.Cyan);
	public static ConsoleProgressColors Magenta			=> new (ConsoleColor.Magenta, ConsoleColor.Magenta);
	public static ConsoleProgressColors White			=> new (ConsoleColor.White, ConsoleColor.White);
	public static ConsoleProgressColors Gray			=> new (ConsoleColor.Gray, ConsoleColor.White);

	#endregion Presets

	//-----------------------------------------------------------------------------------------------------------------
	#region Properties

	public ConsoleColor Background
		{ get; set; }

	public ConsoleColor ActiveBar
		{ get; set; }

	public ConsoleColor InactiveBar
		{ get; set; }

	public ConsoleColor FractionBar
		{ get; set; }

	#endregion Properties
}
