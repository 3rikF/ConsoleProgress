namespace ConsoleExample.Examples;

internal sealed class Header() : ExampleBase(string.Empty, null)
{
	protected override void ImplementExample()
	{
		Console.Clear();
		Console.WriteLine("------------------------------------------------------------");
		Console.WriteLine("Console Progress Bar Examples");
		Console.WriteLine("------------------------------------------------------------");
		Console.WriteLine();

		Thread.Sleep(500);
	}
}
