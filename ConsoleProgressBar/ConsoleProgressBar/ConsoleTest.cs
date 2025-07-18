
using System.Text;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class ConsoleTest : IConsole
{
	internal const int DEBUG_CONSOLE_WIDTH = 80;

	public Encoding OutputEncoding
	{
		get => Console.OutputEncoding;
		set => Console.OutputEncoding = value;
	}

	public void Write(char value)
		=> Console.Write(value);

	public void Write(string value)
		=> Console.Write(value);

	public void WriteLine()
		=> Console.WriteLine();

	public void WriteLine(string value)
		=> Console.WriteLine(value);

	//--- not supported while testing console ---

	/// <summary>
	/// Will not create an exception when called with the real console during tests,
	/// but will not be change the color either.
	/// </summary>
	public ConsoleColor BackgroundColor
		{ get; set; }

	/// <summary>
	/// Will not create an exception when called with the real console during tests,
	/// but will not be change the color either.
	/// </summary>
	public ConsoleColor ForegroundColor
		{ get; set; }

	public int WindowWidth			=> DEBUG_CONSOLE_WIDTH;
	public int CursorLeft			{ get; set; } = 0;
	public int CursorTop			{ get; set; } = 0;

	public bool CursorVisible		{ set { } }

	public void SetCursorPosition(int left, int top)
	{ }
}

