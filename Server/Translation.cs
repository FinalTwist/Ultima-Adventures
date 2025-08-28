using System;

namespace Server
{
    public static class Translation
    {
        // These are hooks that the script assembly will populate at startup.
        // The core server code will call these delegates to perform translation.
        // This avoids a direct dependency from the core assembly to the scripts assembly.

        public static Func<string, string> TranslateToSpanish { get; set; }
        public static Func<string, string> TranslateToEnglish { get; set; }
        public static Func<int, string, string> TranslateCliloc { get; set; }
    }
}
