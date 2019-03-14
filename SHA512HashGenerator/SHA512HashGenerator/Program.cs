using System;
using System.Text;

namespace SHA512HashGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "a";

            Console.WriteLine(Hash.GetHashString(test));

            Console.ReadKey();
        }
    }
}
