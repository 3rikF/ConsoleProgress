namespace ConsoleExample.Examples
{
	internal sealed class PageInteraction(int NumPages, int startPage=0)
	{
		private int _curPage = int.Clamp(startPage, 0, NumPages - 1);

		public event Action<int> ShowPage		= iPage => Console.WriteLine($"Page {iPage + 1} of {NumPages}");
		public event Func<int, int> NextPage	= iPage => Math.Min(++iPage, NumPages - 1);
		public event Func<int, int> PrevPage	= iPage => Math.Max(--iPage, 0);
		public event Action Exit				= () => Environment.Exit(0);

		public event Func<int, bool> HasNextPage = curPage => curPage < NumPages - 1;
		public event Func<int, bool> HasPrevPage = curPage => curPage > 0;


		public void Start()
		{
			while (true)
			{
				bool hasPrevPage = HasPrevPage(_curPage);
				bool hasNextPage = HasNextPage(_curPage);

				ShowPage(_curPage);

				Console.Write("[ R: reload current page ");

				if (hasPrevPage)
					Console.Write("| P: previous page ");

				if (hasNextPage)
					Console.Write("| N: next page ");

				Console.WriteLine(" | E to exit ]");

				while(true)
				{
					ConsoleKeyInfo key = Console.ReadKey();

					if (key.Key == ConsoleKey.R)
						break;

					else if (key.Key == ConsoleKey.N && hasNextPage)
					{
						_curPage = NextPage(_curPage);
						break;
					}

					else if (key.Key == ConsoleKey.P && hasPrevPage)
					{
						_curPage = PrevPage(_curPage);
						break;
					}

					else if (key.Key == ConsoleKey.E)
						Exit();

					// delete last invalid char from console
					Console.Write("\b \b");
				}
			}
		}
	}
}
