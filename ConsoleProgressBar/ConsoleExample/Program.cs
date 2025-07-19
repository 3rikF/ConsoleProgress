// See https://aka.ms/new-console-template for more information
using ConsoleExample.Examples;

Header header			= new ();
ExampleBase[] examples	=
[
	new DefaultStylesExample("[1/4] Default Styles Example", header),
	new CustomStylesExample("[2/4] Custom Styles Example", header),
	new DefaultColorsExample("[3/4] Default Color Example", header),
	new RandomColorsandStyleExample("[4/4] Random Bars Example", header),
];

PageInteraction interaction = new(examples.Length);
interaction.ShowPage += iPage => examples[iPage].RunExample();
interaction.Start();
