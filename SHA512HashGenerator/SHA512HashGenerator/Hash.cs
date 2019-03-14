using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace SHA512HashGenerator
{
    public static class Hash
    {
        public static byte[] GetHash(byte[] input)
        {
            using (var generator = new SHA512Managed())
            {
                return generator.ComputeHash(input);
            }

        }

        public static string GetHashString(byte[] input)
        {
            using (var generator = new SHA512Managed())
            {
                byte[] output = generator.ComputeHash(input);

                StringBuilder hexString = new StringBuilder();

                foreach (var outputByte in output)
                {
                    hexString.AppendFormat("{0:X2}", outputByte);
                }

                return hexString.ToString();
            }
        }

        public static string GetHashString(string input)
        {
            byte[] byteInput = Encoding.UTF8.GetBytes(input);

            return GetHashString(byteInput);
        }
    }
}
