using ConsoleProgressBar;

using Moq;

namespace ConsoleProgressBarTests
{
	public class ConsoleProgressHandlerTests
	{
		[Fact]
		public void PreCountSetDoesNotCallCount()
		{



			IEnumerable<int> blah = [1, 2, 3, 4, 5];

			//--- mock IEnumerable<int> with moq ---
			Mock<ICollection<int>> mockEnumerable = new Mock<ICollection<int>>();

			//-- check, that [GetEnumerator()] is only called once ---
			//mockEnumerable.Setup(m => m.GetEnumerator())
			//	.Verifiable(Times.Never);
			mockEnumerable.Setup(m => m.GetEnumerator())
				.Returns(blah.GetEnumerator());


			mockEnumerable.Setup(m => m.Count)
				.Verifiable(Times.Never);
				//.Throws(new Exception("Count should not be called"));

			//--- Arrange -----------------------------------------------------
			ProgressProxy<int> sut = mockEnumerable.Object
				.ConsoleProgress()
				.WithPreCount(5)
				.DebugMode();


			//--- Act ---------------------------------------------------------
			sut.UpdateTotalStepsIfUnset();

			Assert.Equal(5, sut.TotalSteps);

			foreach (int item in sut)
			{
				// do nothing
			}


			//--- Assert ------------------------------------------------------
			mockEnumerable.VerifyAll();

			//Assert.True(handler.StartAtNewLine);
			//Assert.Equal(5, handler.PreCountElements);
			//Assert.Equal(ConsoleProgressStyle.Block, handler.Style);
			//Assert.Equal(ConsoleProgressColors.Default, handler.Colors);

		}
	}
}