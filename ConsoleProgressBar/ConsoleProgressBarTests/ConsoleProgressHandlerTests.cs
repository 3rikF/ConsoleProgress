using ConsoleProgressBar;

using Moq;

namespace ConsoleProgressBarTests;

#pragma warning disable CCD0001 // IOSP violation

public class ConsoleProgressHandlerTests
{
	/// <summary>
	/// Test the most basic use case of the progress bar:
	/// It should pass through the values of the input enumerable.
	/// </summary>
	[Fact]
	public void IteratorProxy()
	{
		//--- Arrange ---------------------------------------------------------
		byte[] testData = new byte[1000];
		new Random(08_15).NextBytes(testData);

		ProgressProxy<byte> sut = testData
			.ConsoleProgress()
			.WithDebugMode();

		//--- Act and Assert --------------------------------------------------
		int iTestData = 0;

		foreach (int item in sut)
			Assert.Equal(testData[iTestData++], item);
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
		byte[] testData = new byte[1000];
		new Random(08_15).NextBytes(testData);

		//--- mock IEnumerable<int> with Moq ---
		Mock<ICollection<byte>> mockEnumerable = new Mock<ICollection<byte>>();

		//-- check, that [GetEnumerator()] is only called once ---
		mockEnumerable.Setup(m => m.GetEnumerator())
			.Returns((testData as IEnumerable<byte>).GetEnumerator())
			.Verifiable(Times.Once);

		//--- given [WithPreCount], this should not be called ---
		mockEnumerable.Setup(m => m.Count)
			.Verifiable(Times.Never);

		ProgressProxy<byte> sut = mockEnumerable.Object
			.ConsoleProgress()
			.WithPreCount(5)
			.WithDebugMode();

		//--- Act -------------------------------------------------------------
		Assert.Equal(5, sut.TotalSteps);

		int iTestData = 0;

		foreach (int item in sut)
			Assert.Equal(testData[iTestData++], item);

		//--- Assert ----------------------------------------------------------
		mockEnumerable.VerifyAll();
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
		const int MAX_ITERATIONS = 5;

		byte[] testData = new byte[1000];
		new Random(08_15).NextBytes(testData);

		Mock<ICollection<byte>> mockEnumerable = new Mock<ICollection<byte>>();
		Mock<IEnumerator<byte>> mockEnumerator = new Mock<IEnumerator<byte>>();

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
	}
}

#pragma warning restore CCD0001 // IOSP violation