
// ignore spelling: moq bg

using ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBarTests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class ConsoleProgressColorsTests
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Test Methods

	[Fact]
	public void CTor_SingleColor()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleColor activeBar		= ConsoleColor.Blue;

		//--- Act -------------------------------------------------------------
		ConsoleProgressColors uut	= new(activeBar) { Name = "Test" };

		//--- Assert ----------------------------------------------------------
		Assert.Equal(activeBar,											uut.ActiveBar);
		Assert.Equal(activeBar,											uut.FractionBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_INACTIVE_BAR,	uut.InactiveBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_BACKGROUND,	uut.Background);
	}

	[Fact]
	public void Ctor_ActiveAndFractionColor()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleColor activeBar		= ConsoleColor.Blue;
		ConsoleColor fractionBar	= ConsoleColor.Cyan;

		//--- Act -------------------------------------------------------------
		ConsoleProgressColors uut	= new(activeBar, fractionBar) { Name = "Test" };

		//--- Assert ----------------------------------------------------------
		Assert.Equal(activeBar,											uut.ActiveBar);
		Assert.Equal(fractionBar,										uut.FractionBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_INACTIVE_BAR,	uut.InactiveBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_BACKGROUND,	uut.Background);
	}

	[Fact]
	public void Ctor_ActiveInactiveAndFractionColor()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleColor activeBar		= ConsoleColor.Blue;
		ConsoleColor fractionBar	= ConsoleColor.Cyan;
		ConsoleColor inactiveBar	= ConsoleColor.DarkCyan;

		//--- Act -------------------------------------------------------------
		ConsoleProgressColors uut	= new(activeBar, fractionBar, inactiveBar) { Name = "Test" };

		//--- Assert ----------------------------------------------------------
		Assert.Equal(activeBar,											uut.ActiveBar);
		Assert.Equal(fractionBar,										uut.FractionBar);
		Assert.Equal(inactiveBar,										uut.InactiveBar);
		Assert.Equal(ConsoleProgressColors.DEFAULT_COLOR_BACKGROUND,	uut.Background);
	}

	[Fact]
	public void Ctor_AllTheColors()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleColor activeBar		= ConsoleColor.Blue;
		ConsoleColor fractionBar	= ConsoleColor.Cyan;
		ConsoleColor inactiveBar	= ConsoleColor.DarkCyan;
		ConsoleColor background		= ConsoleColor.Black;

		//--- Act -------------------------------------------------------------
		ConsoleProgressColors uut = new(activeBar, fractionBar, inactiveBar, background) { Name = "Test" };

		//--- Assert ----------------------------------------------------------
		Assert.Equal(activeBar,		uut.ActiveBar);
		Assert.Equal(fractionBar,	uut.FractionBar);
		Assert.Equal(inactiveBar,	uut.InactiveBar);
		Assert.Equal(background,	uut.Background);
	}

	[Fact]
	public void Record_EqualityAndHashCode()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleProgressColors uut1 = new(ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.Black) { Name = "Test1" };
		ConsoleProgressColors uut2 = new(ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.Black) { Name = "Test1" };
		ConsoleProgressColors uut3 = new(ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.Black) { Name = "Test2" };

		//--- Act & Assert ----------------------------------------------------
		Assert.Equal(uut1,		uut2);
		Assert.NotEqual(uut1,	uut3);

		Assert.True(uut1 == uut2);
		Assert.True(uut1 != uut3);

		Assert.False(uut1 != uut2);
		Assert.False(uut1 == uut3);

		Assert.Equal(uut1.GetHashCode(), uut2.GetHashCode());
		Assert.NotEqual(uut1.GetHashCode(), uut3.GetHashCode());
	}

	[Fact]
	public void Record_CloneWith()
	{
		//--- ARRANGE ---------------------------------------------------------
		ConsoleProgressColors original = new(ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.Black) { Name = "Original" };

		//--- ACT -------------------------------------------------------------
		ConsoleProgressColors clone = original with { Name = "Clone" };

		//--- ASSERT ----------------------------------------------------------
		Assert.NotSame( original,			clone );
		Assert.Equal( original.ActiveBar,	clone.ActiveBar );
		Assert.Equal( original.FractionBar,	clone.FractionBar );
		Assert.Equal( original.InactiveBar,	clone.InactiveBar );
		Assert.Equal( original.Background,	clone.Background );
		Assert.NotEqual( original.Name,		clone.Name );
		Assert.Equal( "Original",			original.Name );
		Assert.Equal( "Clone",				clone.Name );
	}

	#endregion Test Methods
}
