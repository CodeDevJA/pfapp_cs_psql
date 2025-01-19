namespace ConsoleColorUtilities
{
    // ANSI Escape Sequences
    public static class TextColor
    {
        // Reset
        public static string Reset => "\u001b[0m";

        // Standard Colors
        public static string Black => "\u001b[30m";
        public static string Red => "\u001b[31m";
        public static string Green => "\u001b[32m";
        public static string Yellow => "\u001b[33m";
        public static string Blue => "\u001b[34m";
        public static string Magenta => "\u001b[35m";
        public static string Cyan => "\u001b[36m";
        public static string White => "\u001b[37m";

        // Bright Colors
        public static string BrightBlack => "\u001b[90m";
        public static string BrightRed => "\u001b[91m";
        public static string BrightGreen => "\u001b[92m";
        public static string BrightYellow => "\u001b[93m";
        public static string BrightBlue => "\u001b[94m";
        public static string BrightMagenta => "\u001b[95m";
        public static string BrightCyan => "\u001b[96m";
        public static string BrightWhite => "\u001b[97m";

        // Background Colors
        public static string BgBlack => "\u001b[40m";
        public static string BgRed => "\u001b[41m";
        public static string BgGreen => "\u001b[42m";
        public static string BgYellow => "\u001b[43m";
        public static string BgBlue => "\u001b[44m";
        public static string BgMagenta => "\u001b[45m";
        public static string BgCyan => "\u001b[46m";
        public static string BgWhite => "\u001b[47m";

        // Bright Background Colors
        public static string BgBrightBlack => "\u001b[100m";
        public static string BgBrightRed => "\u001b[101m";
        public static string BgBrightGreen => "\u001b[102m";
        public static string BgBrightYellow => "\u001b[103m";
        public static string BgBrightBlue => "\u001b[104m";
        public static string BgBrightMagenta => "\u001b[105m";
        public static string BgBrightCyan => "\u001b[106m";
        public static string BgBrightWhite => "\u001b[107m";

        // Text Styles
        public static string Bold => "\u001b[1m";
        public static string Dim => "\u001b[2m";
        public static string Italic => "\u001b[3m";
        public static string Underline => "\u001b[4m";
        public static string Blink => "\u001b[5m";
        public static string Reverse => "\u001b[7m";
        public static string Hidden => "\u001b[8m";
        public static string Strikethrough => "\u001b[9m";

        // Combined Styles
        public static string BoldRed => "\u001b[1;31m";
        public static string DimGreen => "\u001b[2;32m";
        public static string UnderlineYellow => "\u001b[4;33m";
        public static string BlinkBlue => "\u001b[5;34m";
        public static string ReverseMagenta => "\u001b[7;35m";
        public static string StrikethroughCyan => "\u001b[9;36m";

        // Utility Method to Wrap Text with a Specific Color or Style
        public static string ApplyStyle(string text, string style) => $"{style}{text}{Reset}";
    }
}
