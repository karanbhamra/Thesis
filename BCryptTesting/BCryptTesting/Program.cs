using System;
using System.Diagnostics;
using BCrypt.Net;

namespace BCryptTesting
{
    class Program
    {
        private static Random gen = new Random();

        static string GetRandomString(int size)
        {
            char[] str = new char[size];

            for (int i = 0; i < str.Length; i++)
            {
                str[i] = (char) gen.Next(97, 123);
            }

            return new string(str);
        }

        static void Main(string[] args)
        {

            string temp =
                "rhxiicinsaaoxthrjevavfdclbkpqgfosdgflbolqekqnoujecjfvuhlduxhstxmumwuncghqkjqvnpxhbknwzsxvubqehtxhyvb";
            string hash = "$2a$13$Q1hTb8OGEiWuA1cizI985./kXh8FLLN/pXb7Gw7Iu/i9nlcAF7X6y";

            Console.WriteLine(BCrypt.Net.BCrypt.Verify(temp, hash));
//            string[] array = new string[100];
//
//            Console.WriteLine("Generating strings...");
//            for (int i = 0; i < array.Length; i++)
//            {
//                array[i] = GetRandomString(100);
//            }
//
//            int cost = 13;
//
//            Console.WriteLine("Finished generating the random 100 length strings");
//            Console.WriteLine("Generating random salt and hashing....");
//            Stopwatch sw = Stopwatch.StartNew();
//
//            for (int i = 0; i < array.Length; i++)
//            {
//                string temp = array[i];
//                string salt = BCrypt.Net.BCrypt.GenerateSalt(workFactor:cost);
//                string tempHash = BCrypt.Net.BCrypt.HashPassword(temp, salt: salt);
//
//                Console.WriteLine(temp);
//                Console.WriteLine(tempHash);
//            }
//
//            sw.Stop();
//
//            TimeSpan ts = sw.Elapsed;
//
//            Console.WriteLine($"Done....It took {ts.TotalMilliseconds} milliseconds or {Math.Round(ts.TotalSeconds, 2)} seconds or {Math.Round(ts.TotalMinutes, 2)} minutes");
        }
    }
}