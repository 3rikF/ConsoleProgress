
using ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBarTests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public class ConsoleProgressStyleTests
{
	[Fact]
	public void Ctor_AssignsPropertiesCorrectly()
	{
		//--- ARRANGE ---------------------------------------------------------
		const bool TEST_SHOWFRACTIONS			= true;
		const bool TEST_SHOWFRAME				= false;
		const string TEST_FRAMELEFT				= "<";
		const string TEST_FRAMERIGHT			= ">";
		const char TEST_CHAREMPTY				= '-';
		const char TEST_CHARDONE				= '+';
		const string TEST_NAME					= "TestStyle";
		char[] TEST_PROGRESSCHARFRACTIONS		= ['a', 'b', 'c'];

		//--- ACT -------------------------------------------------------------
		ConsoleProgressStyle uut = new(
			TEST_SHOWFRACTIONS,
			TEST_SHOWFRAME,
			TEST_FRAMELEFT,
			TEST_FRAMERIGHT,
			TEST_CHAREMPTY,
			TEST_CHARDONE,
			TEST_PROGRESSCHARFRACTIONS,
			TEST_NAME
		);

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(TEST_SHOWFRACTIONS,			uut.ShowFractions);
		Assert.Equal(TEST_SHOWFRAME,				uut.ShowFrame);
		Assert.Equal(TEST_FRAMELEFT,				uut.FrameLeft);
		Assert.Equal(TEST_FRAMERIGHT,				uut.FrameRight);
		Assert.Equal(TEST_CHAREMPTY,				uut.CharEmpty);
		Assert.Equal(TEST_CHARDONE,					uut.CharDone);
		Assert.Equal(TEST_PROGRESSCHARFRACTIONS, 	uut.ProgressCharFractions);
		Assert.Equal(TEST_NAME,						uut.Name);
	}

	[Fact]
	public void Default_ReturnsModernEx()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleProgressStyle expected	= ConsoleProgressStyle.ModernEx;

		//--- ACT -------------------------------------------------------------
		ConsoleProgressStyle actual		= ConsoleProgressStyle.Default;

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expected, actual);
	}
}
