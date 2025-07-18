
// ignore spelling: bg

using System.Text;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
public interface IConsole
{
	Encoding OutputEncoding
		{ get; set;}

	int WindowWidth
		{ get; /*set;*/ }

	int CursorLeft
		{ get; set; }

	int CursorTop
		{ get; set; }

	bool CursorVisible
		{ /*get;*/ set; }

	ConsoleColor ForegroundColor
		{ get; set; }

	ConsoleColor BackgroundColor
		{ get; set; }

	void Write(char value);
	void Write(string value);

	void WriteLine();
	void WriteLine(string value);

	void SetCursorPosition(int left, int top);
}
