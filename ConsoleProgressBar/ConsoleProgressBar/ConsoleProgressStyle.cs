
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
	, string Name = ""
	)
{
	// maybe see: https://github.com/MonoLisaFont/feedback/issues/133
	private const char CHAR_EMPTY = ' ';

	public static ConsoleProgressStyle Default
		=> ModernEx;

	public static ConsoleProgressStyle Block
		{ get; } = new ConsoleProgressStyle(false, true,	"▕", "▏", ' ', '█', FractionsSets.None, nameof(Block));

	public static ConsoleProgressStyle BlockEx
		{ get; } = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', [CHAR_EMPTY, .. FractionsSets.FineBlocks], nameof(BlockEx));

	public static ConsoleProgressStyle BlockDigits
		{ get; } = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', FractionsSets.Digits, Name:nameof(BlockDigits));

	public static ConsoleProgressStyle BlockFine
		{get;} = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', [CHAR_EMPTY, .. FractionsSets.FineHorizontal], nameof(BlockFine));

	public static ConsoleProgressStyle BlockFineVertical
		{get;} = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', [CHAR_EMPTY, .. FractionsSets.FineVertical], nameof(BlockFineVertical));

	public static ConsoleProgressStyle BlockFineBlend
		{get;} = new ConsoleProgressStyle(true, true,		"▕", "▏", ' ', '█', [CHAR_EMPTY, .. FractionsSets.Blend], nameof(BlockFineBlend));

	public static ConsoleProgressStyle Rectangles
		{ get; } = new ConsoleProgressStyle(true, true,	" ■", "■ ", ' ', '■', FractionsSets.None, nameof(Rectangles));

	public static ConsoleProgressStyle RectanglesEx
		{ get; } = new ConsoleProgressStyle(true, true,		" ■", "■ ", ' ', '■', [CHAR_EMPTY, '▫', '▪', '□', '■'], nameof(RectanglesEx));

	//--- almost seamless thin line ---
	public static ConsoleProgressStyle Modern
		{ get; } = new ConsoleProgressStyle(true, true,		" [", "] ", '▬', '▬', ['▬'], nameof(Modern));

	public static ConsoleProgressStyle ModernEx
		{ get; } = new ConsoleProgressStyle(true, true,		" [", "] ", '▬', '▬', FractionsSets.AnimationRectangles, nameof(ModernEx));

	public static ConsoleProgressStyle ModernDigits
		{ get; } = new ConsoleProgressStyle(true, true,		" [", "] ", CHAR_EMPTY, '▬', FractionsSets.DigitsDarkCircle, nameof(ModernDigits));

	public static ConsoleProgressStyle Millennium
		{ get; } = new ConsoleProgressStyle(true, true,		" ᜶", "᜶ ", '▱', '▰', ['▱'], nameof(Millennium));

}
