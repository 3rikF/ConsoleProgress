
namespace ConsoleProgressBarTests;

//-----------------------------------------------------------------------------------------------------------------------------------------
using ConsoleProgressBarTests.TestTools;

//-----------------------------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Tests for <see cref="ConsoleInterceptor"/> class.
/// </summary>
public sealed class ConsoleInterceptorTests
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Test Methods

	[Fact]
	public void Ctor_ShouldRedirectConsoleOutput()
	{
		//--- ARRANGE ---------------------------------------------------------
		TextWriter originalOutput = Console.Out;

		//--- ACT -------------------------------------------------------------
		using ConsoleInterceptor interceptor = new();

		//--- ASSERT ----------------------------------------------------------
		Assert.NotSame(originalOutput, 	Console.Out);
	}

	[Fact]
	public void Output_ShouldCaptureConsoleWrite()
	{
		//--- ARRANGE ---------------------------------------------------------
		using ConsoleInterceptor interceptor = new();
		const string TEST_TEXT = "Test output";

		//--- ACT -------------------------------------------------------------
		Console.Write(TEST_TEXT);
		string result = interceptor.Output;

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(TEST_TEXT, result);
	}

	[Fact]
	public void Output_ShouldCaptureConsoleWriteLine()
	{
		//--- ARRANGE ---------------------------------------------------------
		using ConsoleInterceptor interceptor = new();
		const string TEST_TEXT	= "Test output line";
		string expectedOutput	= TEST_TEXT + Environment.NewLine;

		//--- ACT -------------------------------------------------------------
		Console.WriteLine(TEST_TEXT);
		string result = interceptor.Output;

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedOutput, result);
	}

	[Fact]
	public void Output_ShouldCaptureMultipleWrites()
	{
		//--- ARRANGE ---------------------------------------------------------
		using ConsoleInterceptor interceptor = new();
		const string TEXT1		= "First";
		const string TEXT2		= "Second";
		string expectedOutput	= TEXT1 + TEXT2;

		//--- ACT -------------------------------------------------------------
		Console.Write(TEXT1);
		Console.Write(TEXT2);
		string result = interceptor.Output;

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedOutput, result);
	}

	[Fact]
	public void Dispose_ShouldRestoreOriginalConsoleOut()
	{
		//--- ARRANGE ---------------------------------------------------------
		TextWriter originalOutput 		= Console.Out;
		ConsoleInterceptor interceptor	= new();

		//--- ACT -------------------------------------------------------------
		interceptor.Dispose();

		//--- ASSERT ----------------------------------------------------------
		Assert.Same(originalOutput, Console.Out);
	}

	[Fact]
	public void MultipleInterceptors_ShouldWorkCorrectly()
	{
		//--- ARRANGE ---------------------------------------------------------
		TextWriter originalOutput 		= Console.Out;

		//--- ACT -------------------------------------------------------------
		using (ConsoleInterceptor interceptor1 = new())
		{
			Console.Write("First interceptor");

			using (ConsoleInterceptor interceptor2 = new())
			{
				Console.Write("Second interceptor");

				//--- ASSERT --------------------------------------------------
				Assert.Equal("Second interceptor", 	interceptor2.Output);
			}

			// After inner interceptor is disposed, we should be back to the first one
			Console.Write(" continues");
			Assert.Equal("First interceptor continues", 	interceptor1.Output);
		}

		// After both are disposed, we should be back to the original output
		Assert.Same(originalOutput, 	Console.Out);
	}

	#endregion Test Methods
}