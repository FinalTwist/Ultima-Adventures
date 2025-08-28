using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Server.Misc
{
    public class Translator
    {
        private static readonly HttpClient client = new HttpClient();

        private static string Translate(string text, string source, string target)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            try
            {
                // NOTE: This is a blocking call. In a real-world application, this should be handled asynchronously
                // to avoid blocking the server thread. However, due to the existing synchronous nature of the codebase,
                // we are using .Result here as a compromise to avoid a major refactoring.
                // A better solution would be to make the entire call stack asynchronous.
                var apiKey = MyServerSettings.TranslationApiKey();

                object data;
                if (!string.IsNullOrEmpty(apiKey))
                {
                    data = new { q = text, source = source, target = target, api_key = apiKey };
                }
                else
                {
                    data = new { q = text, source = source, target = target };
                }

                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("http://localhost:5000/translate", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var result = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
                    return result["translatedText"];
                }
                else
                {
                    return text; // Return original text on error
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("LibreTranslate request failed: " + e.Message);
                // In case of any exception, just return the original text.
                return text;
            }
        }

        public static string ToSpanish(string text)
        {
			if (!MyServerSettings.EnableTranslation())
				return text;
            return Translate(text, "en", "es");
        }

        public static string ToEnglish(string text)
        {
			if (!MyServerSettings.EnableTranslation())
				return text;
            return Translate(text, "es", "en");
        }

		public static string TranslateCliloc(int number, string args)
		{
			if (!MyServerSettings.EnableTranslation())
				return null;

			string text = Cliloc.GetString(number, args);

			if (text != null)
				return Translate(text, "en", "es");

			return null;
		}
    }
}
