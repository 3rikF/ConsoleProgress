
using System.Text;
using ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBarTests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class TestConsoleTests
{
	[Fact]
	public void OutputEncoding_GetSet_ForwardsToConsole()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut			= new();
		Encoding originalEncoding	= Console.OutputEncoding;
		Encoding expectedEncoding	= Encoding.UTF8;

		try
		{
			//--- ACT -------------------------------------------------------------
			uut.OutputEncoding = expectedEncoding;
			Encoding actualEncoding = uut.OutputEncoding;

			//--- ASSERT ----------------------------------------------------------
			Assert.Equal(expectedEncoding,	actualEncoding);
			Assert.Equal(expectedEncoding,	Console.OutputEncoding);
		}
		finally
		{
			// Restore original encoding
			Console.OutputEncoding = originalEncoding;
		}
	}

	[Fact]
	public void ForegroundColor_GetSet_ForwardsToConsole()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut						= new();
		const ConsoleColor EXPECTED_COLOR	= ConsoleColor.Magenta;
		var originalConsoleColor			= Console.ForegroundColor;

		//--- ACT ------------------------------------------------------------
		uut.ForegroundColor					= EXPECTED_COLOR;

		//--- ASSERT ---------------------------------------------------------
		Assert.Equal(EXPECTED_COLOR, uut.ForegroundColor);

		//--- will not change the actual console color ---
		// windows returns gray, linux returns -1
		Assert.Equal(originalConsoleColor,	Console.ForegroundColor);
	}

	[Fact]
	public void BackgroundColor_GetSet_ForwardsToConsole()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut				= new();
		ConsoleColor expectedColor	= ConsoleColor.DarkCyan;
		var originalConsoleColor	= Console.BackgroundColor;

		//--- ACT ---------------------------------------------------------
		uut.BackgroundColor = expectedColor;

		//--- ASSERT ------------------------------------------------------
		Assert.Equal(expectedColor,	uut.BackgroundColor);

		//--- will not change the actual console color ---
		Assert.Equal(originalConsoleColor,	Console.BackgroundColor);
	}

	[Fact]
	public void WindowWidth_Get_ReturnsDebugConsoleWidth()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut	= new();
		int expectedWidth	= ConsoleTest.DEBUG_CONSOLE_WIDTH;

		//--- ACT -------------------------------------------------------------
		int actualWidth		= uut.WindowWidth;

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedWidth, actualWidth);
	}

	[Fact]
	public void CursorLeft_GetSet_RetainsValue()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut		= new();
		int expectedPosition	= 42;

		//--- ACT -------------------------------------------------------------
		uut.CursorLeft = expectedPosition;

		//--- ASSERT ----------------------------------------------------------
		int actualPosition = uut.CursorLeft;
		Assert.Equal(expectedPosition, actualPosition);
	}

	[Fact]
	public void CursorTop_GetSet_RetainsValue()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut		= new();
		int expectedPosition	= 24;

		//--- ACT -------------------------------------------------------------
		uut.CursorTop = expectedPosition;

		//--- ASSERT ----------------------------------------------------------
		int actualPosition = uut.CursorTop;
		Assert.Equal(expectedPosition, actualPosition);
	}

	[Fact]
	public void CursorVisible_Set_DoesNotThrowException()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut = new();
		Exception? exception;

		//--- ACT & ASSERT ----------------------------------------------------
		// The setter doesn't do anything but should not throw an exception
		exception = Record.Exception(() => uut.CursorVisible = false);
		Assert.Null(exception);

		exception = Record.Exception(() => uut.CursorVisible = true);
		Assert.Null(exception);
	}

	[Fact]
	public void SetCursorPosition_DoesNotThrowException()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut = new();

		//--- ACT -------------------------------------------------------------
		// Method doesn't do anything but should not throw an exception
		Exception? exception = Record.Exception(
			() => uut.SetCursorPosition(10, 20));

		//--- ASSERT ----------------------------------------------------------
		Assert.Null(exception);
	}

	[Fact]
	public void Write_String_ForwardsToConsole()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut				= new();
		const string TEST_TEXT			= "Test string";
		StringWriter consoleOutput		= new();
		TextWriter originalOutput		= Console.Out;

		try
		{
			Console.SetOut(consoleOutput);

			//--- ACT -------------------------------------------------------------
			uut.Write(TEST_TEXT);

			//--- ASSERT ----------------------------------------------------------
			string actualOutput = consoleOutput.ToString();
			Assert.Equal(TEST_TEXT, actualOutput);
		}
		finally
		{
			// Restore original output
			Console.SetOut(originalOutput);
		}
	}

	[Fact]
	public void Write_Char_ForwardsToConsole()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut				= new();
		const char TEST_CHAR			= 'X';
		const string EXPECTED_TEXT		= "X";
		StringWriter consoleOutput		= new();
		TextWriter originalOutput		= Console.Out;

		try
		{
			Console.SetOut(consoleOutput);

			//--- ACT -------------------------------------------------------------
			uut.Write(TEST_CHAR);

			//--- ASSERT ----------------------------------------------------------
			string actualOutput = consoleOutput.ToString();
			Assert.Equal(EXPECTED_TEXT, actualOutput);
		}
		finally
		{
			// Restore original output
			Console.SetOut(originalOutput);
		}
	}

	[Fact]
	public void WriteLine_Empty_ForwardsToConsole()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleTest uut				= new();
		string expectedText				= Environment.NewLine;
		StringWriter consoleOutput		= new();
		TextWriter originalOutput		= Console.Out;

		try
		{
			Console.SetOut(consoleOutput);

			//--- ACT -------------------------------------------------------------
			uut.WriteLine();

			//--- ASSERT ----------------------------------------------------------
			string actualOutput = consoleOutput.ToString();
			Assert.Equal(expectedText, actualOutput);
		}
		finally
		{
			// Restore original output
			Console.SetOut(originalOutput);
		}
	}

	[Fact]
	public void WriteLine_String_ForwardsToConsole()
	{
		//--- ARRANGE ---------------------------------------------------------
		const string TEST_TEXT			= "Test string";
		string expectedText				= TEST_TEXT + Environment.NewLine;
		ConsoleTest uut				= new();
		StringWriter consoleOutput		= new();
		TextWriter originalOutput		= Console.Out;

		try
		{
			Console.SetOut(consoleOutput);

			//--- ACT -------------------------------------------------------------
			uut.WriteLine(TEST_TEXT);

			//--- ASSERT ----------------------------------------------------------
			string actualOutput = consoleOutput.ToString();
			Assert.Equal(expectedText, actualOutput);
		}
		finally
		{
			// Restore original output
			Console.SetOut(originalOutput);
		}
	}
}