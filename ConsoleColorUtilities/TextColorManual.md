# TextColorManual

## 1. Create the Static Class for Colors
- This class contains all the ANSI color codes as static properties and methods to make it reusable.


namespace ConsoleColorUtilities
{
    public static class TextColor
    {
        // ANSI Escape Sequences
        public static string Reset => "\u001b[0m";
        public static string Red => "\u001b[31m";
        public static string Green => "\u001b[32m";
        public static string Yellow => "\u001b[33m";
        public static string Blue => "\u001b[34m";
        public static string Magenta => "\u001b[35m";
        public static string Cyan => "\u001b[36m";
        public static string White => "\u001b[37m";

        // Additional utility method (optional): Wrap text with a specific color
        public static string ApplyColor(string text, string color) => $"{color}{text}{Reset}";
    }
}

## 2. Using the Class in Your Main Program
- Here’s how you can refer to the TextColor class in a separate namespace and use it for text formatting.

using System;
using ConsoleColorUtilities; // Include the namespace containing TextColor

class Program
{
    static void Main()
    {
        // Use TextColor directly in interpolated strings
        Console.WriteLine($"This is {TextColor.Red}red{TextColor.Reset} text.");
        Console.WriteLine($"This is {TextColor.Green}green{TextColor.Reset} text.");
        Console.WriteLine($"This is {TextColor.Blue}blue{TextColor.Reset} text.");

        // Wrap text dynamically using ApplyColor method
        string yellowText = TextColor.ApplyColor("yellow", TextColor.Yellow);
        Console.WriteLine($"This text contains {yellowText} inside it.");
    }
}

## 3. Key Features of the Solution
- Color Codes as Static Properties: Each color is defined as a public static string for easy access.
- Flexible Usage: You can directly reference colors in interpolated strings or use the ApplyColor utility method to dynamically format text.
- Namespace-Based Design: The TextColor class is placed in its own namespace (ConsoleColorUtilities) to promote reusability and modularity.

---

## Example Output
- When you run the code, you’ll see:

This is red text.
This is green text.
This is blue text.
This text contains yellow inside it.

- Each part of the text will appear in its respective color.

---

## Benefits
- Separation of Concerns: The color definitions are in a dedicated class, making the solution clean and reusable.
- Namespace Accessibility: You only need to include the ConsoleColorUtilities namespace to gain access to the color functionality.
- Flexible and Readable: The ApplyColor method allows dynamic text styling, while interpolated strings maintain readability.

---

# Comprehensive TextColor Class
- Here’s an exhaustive list of all possible ANSI escape sequences for text styling, colors, background colors, boldness, underlining, and more. This list includes every major styling option available in ANSI-supported terminals.

namespace ConsoleColorUtilities
{
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

## Explanation of All Options
- Reset: \u001b[0m resets all styles to the default terminal appearance.
- Standard Foreground Colors:
    - Black, Red, Green, Yellow, Blue, Magenta, Cyan, White (30m–37m).
- Bright Foreground Colors:
    - BrightBlack, BrightRed, BrightGreen, BrightYellow, BrightBlue, BrightMagenta, BrightCyan, BrightWhite (90m–97m).
- Standard Background Colors:
    - BgBlack, BgRed, BgGreen, BgYellow, BgBlue, BgMagenta, BgCyan, BgWhite (40m–47m).
- Bright Background Colors:
    - BgBrightBlack, BgBrightRed, BgBrightGreen, BgBrightYellow, BgBrightBlue, BgBrightMagenta, BgBrightCyan, BgBrightWhite (100m–107m).
- Text Styles:
    - Bold: \u001b[1m
    - Dim (Faint): \u001b[2m
    - Italic: \u001b[3m
    - Underline: \u001b[4m
    - Blink: \u001b[5m (rarely supported)
    - Reverse: \u001b[7m (swaps foreground and background colors)
    - Hidden: \u001b[8m (makes text invisible)
    - Strikethrough: \u001b[9m
- Combined Styles:
    - Combine multiple styles with semicolons (e.g., 1;31m for bold red).

---

## Usage Examples 
using System;
using ConsoleColorUtilities; // Namespace containing TextColor

class Program
{
    static void Main()
    {
        // Standard colors
        Console.WriteLine($"This is {TextColor.Red}red{TextColor.Reset} text.");
        Console.WriteLine($"This is {TextColor.Blue}blue{TextColor.Reset} text.");

        // Bright colors
        Console.WriteLine($"This is {TextColor.BrightGreen}bright green{TextColor.Reset} text.");
        Console.WriteLine($"This is {TextColor.BgBrightYellow}text with bright yellow background{TextColor.Reset}.");

        // Text styles
        Console.WriteLine($"This is {TextColor.Bold}bold{TextColor.Reset} text.");
        Console.WriteLine($"This is {TextColor.Italic}italicized{TextColor.Reset} text.");
        Console.WriteLine($"This is {TextColor.Underline}underlined{TextColor.Reset} text.");

        // Combined styles
        Console.WriteLine($"This is {TextColor.BoldRed}bold red{TextColor.Reset} text.");
        Console.WriteLine($"This is {TextColor.UnderlineYellow}underlined yellow{TextColor.Reset} text.");
    }
}

## Full Feature List in the Console
- With this class, you can now:

    - Use 16 standard foreground and background colors.
    - Use 8 bright foreground and background colors.
    - Apply text styles like bold, underline, dim, italic, strikethrough, etc.
    - Combine styles for advanced customization.

