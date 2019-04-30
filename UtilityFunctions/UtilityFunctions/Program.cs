﻿using System;
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

    class Program
    {
        static void Main(string[] args)
        {
            string str =
                @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

            Console.WriteLine(str);

            Console.WriteLine("\n\n\n");

            str = str.CapitalizeEveryLetterOnSplit(' ');
            
            Console.WriteLine(str);
        }
    }
}