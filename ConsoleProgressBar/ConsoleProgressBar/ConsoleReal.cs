
using System.Text;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBar;

//--- implement everything using the console itself ---
public sealed class ConsoleReal : IConsole
{
	public Encoding OutputEncoding
	{
		get => Console.OutputEncoding;
		set => Console.OutputEncoding = value;
	}

	public ConsoleColor ForegroundColor
	{
		get => Console.ForegroundColor;
		set => Console.ForegroundColor = value;
	}

	public ConsoleColor BackgroundColor
	{
		get => Console.BackgroundColor;
		set => Console.BackgroundColor = value;
	}

	public int WindowWidth
		=> Console.WindowWidth;

	public int CursorLeft
	{
		get => Console.CursorLeft;
		set => Console.CursorLeft = value;
	}

	public int CursorTop
	{
		get => Console.CursorTop;
		set => Console.CursorTop = value;
	}

	public bool CursorVisible
	{
		//get => Console.CursorVisible;
		set => Console.CursorVisible = value;
	}

	public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);
	public void Write(char value)		=> Console.Write(value);
	public void Write(string value)		=> Console.Write(value);
	public void WriteLine()				=> Console.WriteLine();
	public void WriteLine(string value)	=> Console.WriteLine(value);
}

