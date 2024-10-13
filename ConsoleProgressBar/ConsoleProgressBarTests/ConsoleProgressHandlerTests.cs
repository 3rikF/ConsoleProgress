
// ignore spelling: moq bg

using ConsoleProgressBar;

using Moq;

using Xunit.Abstractions;

namespace ConsoleProgressBarTests;

//[SuppressMessage("Clean Code Developer Principles", "CCD0001:IOSP violation", Justification = "pointless for tests")]
public class ConsoleProgressHandlerTests(ITestOutputHelper toh)
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Nested Types

	/// <summary>
	/// Reroutes the console output to a string writer.
	/// Resets the standard console output to the original one when disposing.
	/// </summary>
	private sealed class ConsoleInterceptor : IDisposable
	{
		private readonly StringWriter _sw;
		private readonly TextWriter _oldOut;

		public ConsoleInterceptor()
		{
			_sw		= new StringWriter();
			_oldOut	= Console.Out;
			Console.SetOut(_sw);
		}

		public string Output
			=> _sw.ToString();

		public void Dispose()
		{
			Console.SetOut(_oldOut);
			_sw.Dispose();
		}
	}

	#endregion Nested Types

	//-------------------------------------------------------------------------------------------------------------
	#region Properties

	private ITestOutputHelper TestConsole
		=> toh;

	#endregion Properties

	//-----------------------------------------------------------------------------------------------------------------
	#region Test Methods

	private static byte[] GetRandomBytes(int length)
	{
		byte[] testData = new byte[length];
		new Random(08_15).NextBytes(testData);
		return testData;
	}

	/// <summary>
	/// Test the most basic use case of the progress bar:
	/// It should pass through the values of the input enumerable.
	/// </summary>
	[Fact]
	public void IteratorProxy()
	{
		//--- Arrange ---------------------------------------------------------
		byte[] testData = GetRandomBytes(1000);

		ProgressProxy<byte> sut = testData
			.ConsoleProgress()
			.WithDebugMode();

		//--- Act and Assert --------------------------------------------------
		int iTestData = 0;

		foreach (int item in sut)
			Assert.Equal(testData[iTestData++], item);

		TestConsole.WriteLine("[OK ✔️] All enumeration items have been passed through the iteration proxy.");
	}

	/// <summary>
	/// Tests the fact, that if a pre-count is set, the progress bar
	/// must not call the [Count] property of the input enumerable
	/// or iterate overt the enumerable to count the elements.
	/// </summary>
	[Fact]
	public void PreCountSetDoesNotCallCount()
	{
		//--- Arrange ---------------------------------------------------------
		const int PRECOUNT	= 5;
		byte[] testData		= GetRandomBytes(1000);

		Mock<ICollection<byte>> mockEnumerable = new ();

		//-- check, that [GetEnumerator()] is only called once ---
		mockEnumerable.Setup(m => m.GetEnumerator())
			.Returns((testData as IEnumerable<byte>).GetEnumerator())
			.Verifiable(Times.Once);

		//--- given [WithPreCount], this should not be called ---
		mockEnumerable.Setup(m => m.Count)
			.Verifiable(Times.Never);

		ProgressProxy<byte> sut = mockEnumerable.Object
			.ConsoleProgress()
			.WithPreCount(PRECOUNT)
			.WithDebugMode();

		//--- Act -------------------------------------------------------------
		Assert.Equal(PRECOUNT, sut.TotalSteps);

		int iTestData = 0;

		foreach (int item in sut)
			Assert.Equal(testData[iTestData++], item);

		//--- Assert ----------------------------------------------------------
		mockEnumerable.VerifyAll();

		TestConsole.WriteLine("[OK ✔️] The Enumerator was only called once on the enumeration");
		TestConsole.WriteLine("[OK ✔️] [Count] was never called on the original enumeration");
	}

	/// <summary>
	/// Tests the fact, that [CancelAfter] cancels the progress bar
	/// after the given number of iterations.
	/// When canceling the iteration, the remaining items must not be processed.
	/// </summary>
	[Fact]
	public void CancelAfterCancels()
	{
		//--- Arrange ---------------------------------------------------------
		const int MAX_ITERATIONS	= 5;
		byte[] testData				= GetRandomBytes(1000);

		Mock<ICollection<byte>> mockEnumerable = new ();
		Mock<IEnumerator<byte>> mockEnumerator = new ();

		//--- this will be called internally by [ProgressProxy] to get the enumerator ---
		mockEnumerable.Setup(m => m.GetEnumerator())
			.Returns((testData as IEnumerable<byte>).GetEnumerator())
			.Verifiable(Times.Once);

		//--- this will be called internally by [ProgressProxy] to get the item count for the progress bar ---
		mockEnumerable.Setup(m => m.Count)
			.Returns(testData.Length)
			.Verifiable(Times.Once);

		//--- this will be called by the [ProgressProxy] to iterate over the items ---
		mockEnumerator.Setup(m => m.MoveNext())
			.Returns(true)
			.Verifiable(Times.Exactly(MAX_ITERATIONS));

		//--- create the progress bar, canceling after [MAX_ITERATIONS] iterations ---
		ProgressProxy<byte> sut = mockEnumerable.Object
			.ConsoleProgress()
			.CancelAfter(MAX_ITERATIONS)
			.WithDebugMode();

		//--- Act -------------------------------------------------------------
		int iIterationCounter = 0;
		foreach (int item in sut)
			iIterationCounter++;

		//--- Assert ----------------------------------------------------------
		//--- assert that the local loop iterated exactly [MAX_ITERATIONS] times ---
		Assert.Equal(MAX_ITERATIONS, iIterationCounter);

		//--- assert that the enumerator was called exactly [MAX_ITERATIONS] times ---
		// this in turn asserts, that the iterator was "canceled" after [MAX_ITERATIONS] iterations
		mockEnumerable.VerifyAll();

		TestConsole.WriteLine("[OK ✔️] After canceling the enumeration, no further item was processed.");
	}

	/// <summary>
	/// Combined test of [WithPreCount] and [CancelAfter].
	/// </summary>
	[Fact]
	public void CancelAfterCancelsWithPreCount()
	{
		//--- Arrange ---------------------------------------------------------
		const int MAX_ITERATIONS	= 5;
		const int PRECOUNT			= 10;
		byte[] testData				= GetRandomBytes(1000);

		Mock<ICollection<byte>> mockEnumerable = new ();
		Mock<IEnumerator<byte>> mockEnumerator = new ();

		//--- this will be called internally by [ProgressProxy] to get the enumerator ---
		mockEnumerable.Setup(m => m.GetEnumerator())
			.Returns((testData as IEnumerable<byte>).GetEnumerator())
			.Verifiable(Times.Once);

		//--- this will be called internally by [ProgressProxy] to get the item count for the progress bar ---
		mockEnumerable.Setup(m => m.Count)
			.Returns(testData.Length)
			.Verifiable(Times.Never);

		//--- this will be called by the [ProgressProxy] to iterate over the items ---
		mockEnumerator.Setup(m => m.MoveNext())
			.Returns(true)
			.Verifiable(Times.Exactly(MAX_ITERATIONS));

		//--- create the progress bar, canceling after [MAX_ITERATIONS] iterations ---
		ProgressProxy<byte> sut = mockEnumerable.Object
			.ConsoleProgress()
			.WithPreCount(PRECOUNT)
			.CancelAfter(MAX_ITERATIONS)
			.WithDebugMode();

		//--- Act -------------------------------------------------------------
		int iIterationCounter = 0;
		foreach (int item in sut)
			iIterationCounter++;

		//--- Assert ----------------------------------------------------------
		//--- assert that the local loop iterated exactly [MAX_ITERATIONS] times ---
		Assert.Equal(MAX_ITERATIONS, iIterationCounter);

		//--- assert that the enumerator was called exactly [MAX_ITERATIONS] times ---
		// this in turn asserts, that the iterator was "canceled" after [MAX_ITERATIONS] iterations
		mockEnumerable.VerifyAll();

		TestConsole.WriteLine("[OK ✔️] The Enumerator was only called once on the enumeration");
		TestConsole.WriteLine("[OK ✔️] [Count] was never called on the original enumeration");
		TestConsole.WriteLine("[OK ✔️] After canceling the enumeration, no further item was processed.");
	}

	/// <summary>
	/// Tests, if the current text-formatting of the progress bar is as expected.
	/// Checks all expected updates of the progress bar.
	/// Without proper cursor positioning during the test run, this will results in multiples lines of output.
	/// </summary>
	[Fact]
	public void TextFormatting()
	{
		//--- Arrange ---------------------------------------------------------
		const string EXPECTED_ACTION_TEXT	= "Action Text";
		const string EXPECTED_ITEM_TEXT		= "Step Text";
		const int NUM_ITEMS					= 10;
		const int EXPECTED_LINES			= NUM_ITEMS +1;

		byte[] testData						= GetRandomBytes(NUM_ITEMS);


		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress(EXPECTED_ACTION_TEXT, EXPECTED_ITEM_TEXT)
			.WithDebugMode();

		using ConsoleInterceptor ci = new();

		//--- Act ---------------------------------------------------------
		foreach (int _ in sut)
		{ }

		//--- Assert ------------------------------------------------------
		string[] lines =
			ci.Output
			.Split("%]")
			.Where(line => !string.IsNullOrWhiteSpace(line))
			.Select(line => line.Trim()+"%]")
			.ToArray();

		Assert.Equal(EXPECTED_LINES, lines.Length);

		//--- each line must contain the action text and a progress-value ---
		for (int i = 0; i < lines.Length; i++)
		{
			string line = lines[i];
			Assert.Contains(EXPECTED_ACTION_TEXT, line);

			// NOT IMPLEMENTED YET
			//Assert.Contains(TEXT_ITEM, line);

			// 0/10 -> 1/10 -> ... 10/10
			Assert.Contains($"{i}/{testData.Length}", line);

			// 0% -> 10% -> ... 100%
			Assert.Contains($"{i * 10}%", line);

			TestConsole.WriteLine($"[OK ✔️] The text formatting of line [{i+1}/{EXPECTED_LINES}] is as expected.");
		}

		TestConsole.WriteLine($"[OK ✔️] The text formatting of all lines is as expected.");
	}

	/// <summary>
	/// Tests, if the maximum bar length setting is respected.
	/// </summary>
	[Fact]
	public void MaxBarLength()
	{
		//--- Arrange ---------------------------------------------------------
		const int MAX_BAR_LENGTH	= 10;
		byte[] testData				= [42];		//--- one item will result in only two steps: 0% and 100% ---

		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithMaxBarLength(MAX_BAR_LENGTH)
			.WithDebugMode();

		char barChar = sut.Style.CharDone;

		using ConsoleInterceptor ci = new();

		//--- Act ---------------------------------------------------------
		foreach (int _ in sut)
		{ }

		//--- Assert ------------------------------------------------------
		//--- somewhere in the console output, there must be a bar with [MAX_BAR_LENGTH] characters ... ---
		Assert.Contains(new string(barChar, MAX_BAR_LENGTH), ci.Output);

		//... and not a single character more ---
		Assert.DoesNotContain(new string(barChar, MAX_BAR_LENGTH + 1), ci.Output);
	}

	[Fact]
	public void FluentApi_DefaultColors()
	{
		//--- Arrange ---------------------------------------------------------
		byte[] testData = GetRandomBytes(10);

		//--- Act ---------------------------------------------------------
		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithDebugMode();

		//--- Assert ------------------------------------------------------
		Assert.Equal(ConsoleProgressColors.Default.ActiveBar,	sut.Colors.ActiveBar);
		Assert.Equal(ConsoleProgressColors.Default.FractionBar,	sut.Colors.FractionBar);
		Assert.Equal(ConsoleProgressColors.Default.InactiveBar,	sut.Colors.InactiveBar);
		Assert.Equal(ConsoleProgressColors.Default.Background,	sut.Colors.Background);
	}

	[Fact]
	public void FluentApi_WithColor_A()
	{
		//--- Arrange ---------------------------------------------------------
		const ConsoleColor ACTIVE_BAR_COLOR		= ConsoleColor.Red;

		byte[] testData = GetRandomBytes(10);

		//--- Act ---------------------------------------------------------
		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithColor(ACTIVE_BAR_COLOR)
			.WithDebugMode();

		//--- Assert ------------------------------------------------------
		Assert.Equal(ACTIVE_BAR_COLOR, sut.Colors.ActiveBar);
		Assert.Equal(ACTIVE_BAR_COLOR, sut.Colors.FractionBar);

		Assert.Equal(ConsoleProgressColors.Default.InactiveBar,	sut.Colors.InactiveBar);
		Assert.Equal(ConsoleProgressColors.Default.Background,	sut.Colors.Background);
	}

	[Fact]
	public void FluentApi_WithColor_B()
	{
		//--- Arrange ---------------------------------------------------------
		const ConsoleColor ACTIVE_BAR_COLOR		= ConsoleColor.Red;
		const ConsoleColor FRACTION_BAR_COLOR	= ConsoleColor.Green;

		byte[] testData = GetRandomBytes(10);

		//--- Act ---------------------------------------------------------
		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithColor(ACTIVE_BAR_COLOR, FRACTION_BAR_COLOR)
			.WithDebugMode();


		//--- Assert ------------------------------------------------------
		Assert.Equal(ACTIVE_BAR_COLOR, sut.Colors.ActiveBar);
		Assert.Equal(FRACTION_BAR_COLOR, sut.Colors.FractionBar);

		Assert.Equal(ConsoleProgressColors.Default.InactiveBar,	sut.Colors.InactiveBar);
		Assert.Equal(ConsoleProgressColors.Default.Background,	sut.Colors.Background);
	}

	[Fact]
	public void FluentApi_WithColor_C()
	{
		//--- Arrange ---------------------------------------------------------
		const ConsoleColor ACTIVE_BAR_COLOR		= ConsoleColor.Red;
		const ConsoleColor FRACTION_BAR_COLOR	= ConsoleColor.Green;
		const ConsoleColor INACTIVE_BAR_COLOR	= ConsoleColor.Blue;

		byte[] testData = GetRandomBytes(10);

		//--- Act ---------------------------------------------------------
		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithColor(ACTIVE_BAR_COLOR, FRACTION_BAR_COLOR, INACTIVE_BAR_COLOR)
			.WithDebugMode();


		//--- Assert ------------------------------------------------------
		Assert.Equal(ACTIVE_BAR_COLOR,		sut.Colors.ActiveBar);
		Assert.Equal(FRACTION_BAR_COLOR,	sut.Colors.FractionBar);
		Assert.Equal(INACTIVE_BAR_COLOR,	sut.Colors.InactiveBar);

		Assert.Equal(ConsoleProgressColors.Default.Background,	sut.Colors.Background);
	}

	[Fact]
	public void FluentApi_WithBgColor()
	{
		//--- Arrange ---------------------------------------------------------
		const ConsoleColor BACKGROUND_BAR_COLOR	= ConsoleColor.Green;
		byte[] testData = GetRandomBytes(10);

		//--- Act ---------------------------------------------------------
		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithBgColor(BACKGROUND_BAR_COLOR)
			.WithDebugMode();


		//--- Assert ------------------------------------------------------
		Assert.Equal(ConsoleProgressColors.Default.ActiveBar,	sut.Colors.ActiveBar);
		Assert.Equal(ConsoleProgressColors.Default.FractionBar,	sut.Colors.FractionBar);
		Assert.Equal(ConsoleProgressColors.Default.InactiveBar,	sut.Colors.InactiveBar);

		Assert.Equal(BACKGROUND_BAR_COLOR,	sut.Colors.Background);
	}

	public static TheoryData<ConsoleProgressColors> ColorPresets
		=> new()
		{
			ConsoleProgressColors.Default,
			ConsoleProgressColors.Blue,
			ConsoleProgressColors.Green,
			ConsoleProgressColors.Red,
			ConsoleProgressColors.Yellow,
			ConsoleProgressColors.Cyan,
			ConsoleProgressColors.Magenta,
			ConsoleProgressColors.White,
			ConsoleProgressColors.Gray
		};

	/// <summary>
	/// Of course this is silly: if it works for one, it works for all.
	/// But it increases the test coverage due to those color-presets being "touched" by a test.
	/// </summary>
	/// <param name="expectedColors"></param>
	[Theory]
	[MemberData(nameof(ColorPresets))]
	public void FluentApi_WithColorS(ConsoleProgressColors expectedColors)
	{
		//--- Arrange ---------------------------------------------------------
		byte[] testData = GetRandomBytes(10);

		//--- Act ---------------------------------------------------------
		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithColors(expectedColors)
			.WithDebugMode();


		//--- Assert ------------------------------------------------------
		Assert.Equal(expectedColors.ActiveBar,		sut.Colors.ActiveBar);
		Assert.Equal(expectedColors.FractionBar,	sut.Colors.FractionBar);
		Assert.Equal(expectedColors.InactiveBar,	sut.Colors.InactiveBar);
		Assert.Equal(expectedColors.Background,		sut.Colors.Background);
	}

	[Fact]
	public void FluentApi_WithStyle()
	{
		//--- Arrange ---------------------------------------------------------
		ConsoleProgressStyle EXPECTED_STYLE	= new ConsoleProgressStyle(true, false, "FL", "FR", 'X', 'Y', ['1','2','3'], "TESTNAME");

		byte[] testData = GetRandomBytes(10);

		//--- Act ---------------------------------------------------------
		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithStyle(EXPECTED_STYLE)
			.WithDebugMode();


		//--- Assert ------------------------------------------------------
		Assert.Equal(EXPECTED_STYLE.ShowFrame,				sut.Style.ShowFrame);
		Assert.Equal(EXPECTED_STYLE.ShowFractions,			sut.Style.ShowFractions);

		Assert.Equal(EXPECTED_STYLE.FrameLeft,				sut.Style.FrameLeft);
		Assert.Equal(EXPECTED_STYLE.FrameRight,				sut.Style.FrameRight);

		Assert.Equal(EXPECTED_STYLE.CharDone,				sut.Style.CharDone);
		Assert.Equal(EXPECTED_STYLE.CharEmpty,				sut.Style.CharEmpty);
		Assert.Equal(EXPECTED_STYLE.ProgressCharFractions,	sut.Style.ProgressCharFractions);
	}

	/// <summary>
	/// With <see cref="WithNewLine"/> sett to <c>true</c>, the progress bar should start on a new line.
	/// </summary>
	[Fact]
	public void FluentApi_WithNewLine_Confirm()
	{
		//--- Arrange ---------------------------------------------------------
		//--- one item will result in only two steps: 0% and 100% ---
		byte[] testData = GetRandomBytes(1);

		//--- Act ---------------------------------------------------------
		using ConsoleInterceptor ci = new();

		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithNewLine()
			.WithDebugMode();

		foreach (int _ in sut)
		{ }

		//--- Assert ------------------------------------------------------
		Assert.StartsWith(Environment.NewLine, ci.Output);
	}

	/// <summary>
	/// Likewise, with <see cref="WithNewLine"/> sett to <c>false</c>, the progress bar should not start on a new line.
	/// </summary>
	[Fact]
	public void FluentApi_WithNewLine_Falsify()
	{
		//--- Arrange ---------------------------------------------------------
		//--- one item will result in only two steps: 0% and 100% ---
		byte[] testData = GetRandomBytes(1);

		//--- Act ---------------------------------------------------------
		using ConsoleInterceptor ci = new();

		ConsoleProgressHandler<byte> sut = (ConsoleProgressHandler<byte>)testData
			.ConsoleProgress()
			.WithDebugMode();

		foreach (int _ in sut)
		{ }

		//--- Assert ------------------------------------------------------
		Assert.True(!ci.Output.StartsWith(Environment.NewLine));
	}

	#endregion Test Methods
}
