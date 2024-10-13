
// ignore spelling: moq bg

using ConsoleProgressBar;

using Xunit.Abstractions;

namespace ConsoleProgressColorsTests;

//[SuppressMessage("Clean Code Developer Principles", "CCD0001:IOSP violation", Justification = "pointless for tests")]
public class ConsoleProgressColorsTests(ITestOutputHelper toh)
{
	//-------------------------------------------------------------------------------------------------------------
	#region Properties

	private ITestOutputHelper TestConsole
		=> toh;

	#endregion Properties

	//-----------------------------------------------------------------------------------------------------------------
	#region Test Methods

	[Fact]
	public void ConsoleProgressColors_Constructor1()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleColor activeBar = ConsoleColor.Blue;

		//--- Act -------------------------------------------------------------
		ConsoleProgressColors uut = new ConsoleProgressColors(activeBar) { Name = "Test" };

		//--- Assert ----------------------------------------------------------
		Assert.Equal(activeBar,											uut.ActiveBar);
		Assert.Equal(activeBar,											uut.FractionBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_INACTIVE_BAR,	uut.InactiveBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_BACKGROUND,	uut.Background);
	}

	[Fact]
	public void ConsoleProgressColors_Constructor2()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleColor activeBar		= ConsoleColor.Blue;
		ConsoleColor fractionBar	= ConsoleColor.Cyan;

		//--- Act -------------------------------------------------------------
		ConsoleProgressColors uut = new ConsoleProgressColors(activeBar, fractionBar) { Name = "Test" };

		//--- Assert ----------------------------------------------------------
		Assert.Equal(activeBar,											uut.ActiveBar);
		Assert.Equal(fractionBar,										uut.FractionBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_INACTIVE_BAR,	uut.InactiveBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_BACKGROUND,	uut.Background);
	}

	[Fact]
	public void ConsoleProgressColors_Constructor3()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleColor activeBar = ConsoleColor.Blue;
		ConsoleColor fractionBar = ConsoleColor.Cyan;
		ConsoleColor inactiveBar = ConsoleColor.DarkCyan;

		//--- Act -------------------------------------------------------------
		ConsoleProgressColors uut = new ConsoleProgressColors(activeBar, fractionBar, inactiveBar) { Name = "Test" };

		//--- Assert ----------------------------------------------------------
		Assert.Equal(activeBar,											uut.ActiveBar);
		Assert.Equal(fractionBar,										uut.FractionBar);
		Assert.Equal(inactiveBar,										uut.InactiveBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_BACKGROUND,	uut.Background);
	}

	[Fact]
	public void ConsoleProgressColors_Constructor4()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleColor activeBar		= ConsoleColor.Blue;
		ConsoleColor fractionBar	= ConsoleColor.Cyan;
		ConsoleColor inactiveBar	= ConsoleColor.DarkCyan;
		ConsoleColor background		= ConsoleColor.Black;

		//--- Act -------------------------------------------------------------
		ConsoleProgressColors uut = new ConsoleProgressColors(activeBar, fractionBar, inactiveBar, background) { Name = "Test" };

		//--- Assert ----------------------------------------------------------
		Assert.Equal(activeBar,		uut.ActiveBar);
		Assert.Equal(fractionBar,	uut.FractionBar);
		Assert.Equal(inactiveBar,	uut.InactiveBar);
		Assert.Equal(background,	uut.Background);
	}
	#endregion Test Methods
}
