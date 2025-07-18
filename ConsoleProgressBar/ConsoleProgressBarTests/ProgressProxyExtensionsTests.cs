using System.Reflection;

using ConsoleProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBarTests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class ProgressProxyExtensionsTests
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Test Methods

	[Fact]
	public void WithDebugMode_ChangesConsoleToConsoleDebug()
	{
		//--- ARRANGE ---------------------------------------------------------
		byte[] testData = [1, 2, 3, 4, 5];
		ProgressProxy<byte> sut	= new ConsoleProgressHandler<byte>(testData);

		//--- ACT -------------------------------------------------------------
		sut = sut.WithTestMode();

		//--- ASSERT ----------------------------------------------------------
		_ = Assert.IsType<ConsoleTest>(sut.Console);
	}

	[Fact]
	public void DefaultConsoleIsConsoleReal()
	{
		//--- ARRANGE ---------------------------------------------------------
		byte[] testData = [1, 2, 3, 4, 5];

		//--- ACT -------------------------------------------------------------
		ProgressProxy<byte> sut	= new ConsoleProgressHandler<byte>(testData);

		//--- ASSERT ----------------------------------------------------------
		_ = Assert.IsType<ConsoleReal>(sut.Console);
	}

	[Fact]
	public void ConsoleProgressHandler_SingleCollectionConstructor_SetsAppropriateStrings()
	{
		//--- ARRANGE ---------------------------------------------------------
		byte[] testData = [1, 2, 3, 4, 5];

		//--- ACT -------------------------------------------------------------
		ConsoleProgressHandler<byte> sut = new(testData);

		//--- ASSERT ----------------------------------------------------------
		// Using reflection to access the protected properties
		PropertyInfo? actionDescProperty = typeof(ProgressProxy<byte>)
			.GetProperty("ActionDesc", BindingFlags.NonPublic | BindingFlags.Instance);

		PropertyInfo? itemDescProperty = typeof(ProgressProxy<byte>)
			.GetProperty("ItemDesc", BindingFlags.NonPublic | BindingFlags.Instance);

		Assert.NotNull(actionDescProperty);
		Assert.NotNull(itemDescProperty);

		string actionDesc	= (string)actionDescProperty.GetValue(sut)!;
		string itemDesc		= (string)itemDescProperty.GetValue(sut)!;

		Assert.Null(actionDesc);
		Assert.Null(itemDesc);
	}

	#endregion Test Methods
}