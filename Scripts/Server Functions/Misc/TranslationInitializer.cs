using System;

namespace Server.Misc
{
    public static class TranslationInitializer
    {
        [CallPriority(10)]
        public static void Configure()
        {
            Server.Translation.TranslateToSpanish = new Func<string, string>(Server.Misc.Translator.ToSpanish);
            Server.Translation.TranslateToEnglish = new Func<string, string>(Server.Misc.Translator.ToEnglish);
            Server.Translation.TranslateCliloc = new Func<int, string, string>(Server.Misc.Translator.TranslateCliloc);
        }
    }
}
