using System.Text;

namespace UtilityFunctions
{
    static class UtilityFunctions
    {
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
    }
}