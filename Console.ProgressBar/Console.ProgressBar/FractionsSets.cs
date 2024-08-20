
//-----------------------------------------------------------------------------------------------------------------------------------------
namespace Console.ProgressBar;

//-----------------------------------------------------------------------------------------------------------------------------------------
public class FractionsSets
{
	public static char[] None
		{get;} = [];

	public static char[] FineBlocks
		{get;} = ['▖', '▌', '▙', '█'];

	public static char[] FineHorizontal
		{get;} = ['▏', '▎', '▍', '▌', '▋', '▊', '▉'];

	public static char[] FineVertical
		{get;} = ['▁', '▂', '▃', '▄', '▅', '▆', '▇'];

	public static char[] Blend
		{get;} = ['░', '▒', '▓'];

	public static char[] Digits
		{get;} = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

	public static char[] DigitsBrightCircle
		{get;} = ['➀','➁','➂','➃','➄','➅','➆','➇','➈'];		// normal size

	public static char[] DigitsDarkCircle
		{get;} = ['➊','➋','➌','➍','➎','➏','➐','➑','➒'];		// normal size

	public static char[] DigitsDarkCircleLarge
		{get;} = ['❶','❷','❸','❹','❺','❻','❼','❽','❾'];		// over-size (may seem normal depending on the font)

	public static char[] AnimationDots
		{get;} = ['⠋', '⠙', '⠹', '⠸', '⠼', '⠴', '⠦', '⠧', '⠇', '⠏'];

	public static char[] AnimationLines
		{get;} = ['|', '/', '-', '\\', '|', '/', '-', '\\'];

	public static char[] AnimationCircle
		{get;} = ['◜', '◝', '◞', '◟','◜', '◝', '◞', '◟'];

	public static char[] AnimationQuarters
		{get;} = ['○', '◔', '◑', '◕', '●'];

	public static char[] AnimationTriangles
		{get;} = ['♺','♳','♴','♵','♶','♷','♸','♹' ];

	public static char[] AnimationFractions
		{get;} = ['⅛','¼','⅜','½','⅝','¾','⅞'];

	public static char[] AnimationCrossLines
		{get;} = ['╵','└','├','┼','╀','╄','╊','╋','╬'];

	public static char[] AnimationRectangles
		{ get;} = ['▪', '■', '▪', '■', '▪', '■', '▪', '■', '▪', '■'];

	public static char[] AnimationSmallRectangles
		{ get;} = ['▪', '▫', '▪', '▫', '▪', '▫', '▪', '▫', '▪', '▫'];
}
