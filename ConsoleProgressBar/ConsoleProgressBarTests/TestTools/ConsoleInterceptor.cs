
//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBarTests.TestTools;

//-----------------------------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Reroutes the console output to a string writer.
/// Resets the standard console output to the original one when disposing.
/// </summary>
internal sealed class ConsoleInterceptor : IDisposable
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
