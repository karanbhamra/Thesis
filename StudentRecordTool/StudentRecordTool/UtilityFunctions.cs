using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace UtilityFunctions
{
    public class JsonSettings
    {
        public string devUrl { get; set; }
        public string liveUrl { get; set; }
        public string cosmosUrl { get; set; }
        public string cosmosAccessKey { get; set; }
    }

    static class UtilityFunctions
    {
        public static bool IsLocalEnvironment()
        {
            bool isLocal = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));

            return isLocal;

        }
        public static string CapitalizeEveryLetterOnSplit(this string str, char sep)
        {
            StringBuilder output = new StringBuilder();

            string[] words = str.Split(sep);

            for (int i = 0; i < words.Length; i++)
            {
                StringBuilder temp = new StringBuilder(words[i][0].ToString().ToUpper());

                for (int j = 1; j < words[i].Length; j++)
                {
                    temp.Append(words[i][j]);
                }

                if (i != words.Length - 1)
                {
                    output.Append(temp + " ");
                }
                else
                {
                    output.Append(temp);
                }
            }

            return output.ToString();
        }

        public static string GetValueOfSetting(string setting)
        {
            // load the local.settings.json

            using (StreamReader r = new StreamReader("local.settings.json"))
            {
                string json = r.ReadToEnd();

                JsonSettings settings = JsonConvert.DeserializeObject<JsonSettings>(json);

                if (setting == "liveUrl")
                {
                    return settings.liveUrl;
                }
                else if (setting == "devUrl")
                {
                    return settings.devUrl;
                }
                else if (setting == "cosmosUrl")
                {
                    return settings.cosmosUrl;
                }
                else if (setting == "cosmosAccessKey")
                {
                    return settings.cosmosAccessKey;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}