using System;
using System.Text;

namespace SHA512HashGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(Hash.GetHashString(test));
            //Console.WriteLine(Hash.GetRandomHashedSaltBytesAsString());
            //Console.WriteLine(Hash.GetRandomHashedSaltBytes());

            var result = Hash.GetRandomSaltWithHash();
            Console.WriteLine(result[0]);
            Console.WriteLine(result[1]);

            Console.ReadKey();
        }
    }
}
