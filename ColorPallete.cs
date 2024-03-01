using System.Collections.Generic;

namespace NotABook
{
    internal static class ColorPalette
    {
        private static readonly Dictionary<int, string> TextColors = new Dictionary<int, string>
        {
            { 50, "#f6f0ee" },
            { 100, "#ede1de" },
            { 200, "#dcc3bc" },
            { 300, "#caa59b" },
            { 400, "#b98779" },
            { 500, "#a76958" },
            { 600, "#865446" },
            { 700, "#643f35" },
            { 800, "#432a23" },
            { 900, "#211512" },
            { 950, "#110b09" }
        };

        private static readonly Dictionary<int, string> PrimaryColors = new Dictionary<int, string>
        {
            { 50, "#faeeea" },
            { 100, "#f6ded5" },
            { 200, "#edbdab" },
            { 300, "#e39c82" },
            { 400, "#da7b58" },
            { 500, "#d1592e" },
            { 600, "#a74825" },
            { 700, "#7d361c" },
            { 800, "#542412" },
            { 900, "#2a1209" },
            { 950, "#150b05" }
        };

        private static readonly Dictionary<int, string> SecondaryColors = new Dictionary<int, string>
        {
            { 50, "#fdede8" },
            { 100, "#fbdcd0" },
            { 200, "#f6b8a2" },
            { 300, "#f29573" },
            { 400, "#ee7144" },
            { 500, "#e94e16" },
            { 600, "#bb3e11" },
            { 700, "#8c2f0d" },
            { 800, "#5d1f09" },
            { 900, "#2f1004" },
            { 950, "#170802" }
        };

        private static readonly Dictionary<int, string> AccentColors = new Dictionary<int, string>
        {
            { 50, "#ffece6" },
            { 100, "#ffdacc" },
            { 200, "#feb49a" },
            { 300, "#fe8f67" },
            { 400, "#fe6a34" },
            { 500, "#fe4501" },
            { 600, "#cb3701" },
            { 700, "#982901" },
            { 800, "#651b01" },
            { 900, "#330e00" },
            { 950, "#190700" }
        };

        private static readonly Dictionary<int, string> BackgroundColors = new Dictionary<int, string>
        {
            { 50, "#f9f0ec" },
            { 100, "#f2e1d9" },
            { 200, "#e6c3b3" },
            { 300, "#d9a48c" },
            { 400, "#cc8666" },
            { 500, "#bf6840" },
            { 600, "#995333" },
            { 700, "#733e26" },
            { 800, "#4d2a19" },
            { 900, "#26150d" },
            { 950, "#130a06" }
        };

        public static string Text(int shade)
        {
            return TextColors.TryGetValue(shade, out var color) ? color : "";
        }

        public static string Primary(int shade)
        {
            return PrimaryColors.TryGetValue(shade, out var color) ? color : "";
        }

        public static string Secondary(int shade)
        {
            return SecondaryColors.TryGetValue(shade, out var color) ? color : "";
        }

        public static string Accent(int shade)
        {
            return AccentColors.TryGetValue(shade, out var color) ? color : "";
        }

        public static string Background(int shade)
        {
            return BackgroundColors.TryGetValue(shade, out var color) ? color : "";
        }
    }
}
