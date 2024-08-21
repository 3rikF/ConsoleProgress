
//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleProgressBar;

public record ConsoleProgressStyle(
	bool ShowFractions
	, bool ShowFrame
	, string FrameLeft
	, string FrameRight
	, char CharEmpty
	, char CharDone
	, char[] ProgressCharFractions
	)
{
	// maybe see: https://github.com/MonoLisaFont/feedback/issues/133
	private const char CHAR_EMPTY = ' ';

	public static ConsoleProgressStyle Default
		=> ModernEx;

	public static ConsoleProgressStyle Block
		{ get; } = new ConsoleProgressStyle(false, true,	"▕", "▏", ' ', '█', FractionsSets.None);

	public static ConsoleProgressStyle BlockEx
		{ get; } = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', [CHAR_EMPTY, .. FractionsSets.FineBlocks]);

	public static ConsoleProgressStyle BlockDigits
		{ get; } = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', FractionsSets.Digits);

	public static ConsoleProgressStyle BlockFine
		{get;} = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', [CHAR_EMPTY, .. FractionsSets.FineHorizontal]);

	public static ConsoleProgressStyle BlockFineVertical
		{get;} = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', [CHAR_EMPTY, .. FractionsSets.FineVertical]);

	public static ConsoleProgressStyle BlockFineBlend
		{get;} = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', [CHAR_EMPTY, .. FractionsSets.Blend]);

	public static ConsoleProgressStyle Rectangles
		{ get; } = new ConsoleProgressStyle(true, true,	" ■", "■ ", ' ', '■', FractionsSets.None);

	public static ConsoleProgressStyle RectanglesEx
		{ get; } = new ConsoleProgressStyle(true, true,		" ■", "■ ", ' ', '■', [CHAR_EMPTY, '▫', '▪', '□', '■']);

	//--- almost seamless thin line ---
	public static ConsoleProgressStyle Modern
		{ get; } = new ConsoleProgressStyle(true, true,		" [", "] ", '▬', '▬', ['▬']);

	public static ConsoleProgressStyle ModernEx
		{ get; } = new ConsoleProgressStyle(true, true,		" [", "] ", '▬', '▬', FractionsSets.AnimationRectangles);

	public static ConsoleProgressStyle ModernDigits
		{ get; } = new ConsoleProgressStyle(true, true,		" [", "] ", CHAR_EMPTY, '▬', FractionsSets.DigitsDarkCircle);

	public static ConsoleProgressStyle Millennium
		{ get; } = new ConsoleProgressStyle(true, true,		" ᜶", "᜶ ", '▱', '▰', ['▱']);

}
