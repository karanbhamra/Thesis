using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using BCrypt.Net;
namespace SHA512HashGenerator
{
    public static class Hash
    {

        public static string GetBCryptSalt(int workFactor = 11)
        {
            return BCrypt.Net.BCrypt.GenerateSalt(workFactor);
        }

        public static string GetBCryptHashAutoSalt(string input)
        {
            return BCrypt.Net.BCrypt.HashPassword(input);
        }
        public static string GetBCryptHash(string input, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(input, salt);
        }

        public static bool VerifyBCryptHash(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }

        private static byte[] GetHashBytes(byte[] input)
        {
            using (var generator = new SHA512Managed())
            {
                return generator.ComputeHash(input);
            }
        }

        public static string GetHashBytesAsString(byte[] input, bool useBCrypt = false)
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

            return GetHashBytesAsString(byteInput);
        }

        private static byte[] GetBytesFromString(string input)
        {
            byte[] byteResult = Encoding.UTF8.GetBytes(input);

            return byteResult;
        }

        private static string GetRandomSaltAsString()
        {
            string result = Guid.NewGuid().ToString();
            return result;
        }

        private static string GetSaltHashedAsString(string randomSalt)
        {
            byte[] randomSaltBytes = GetBytesFromString(randomSalt);

            string result = GetHashString(randomSalt);

            return result;
        }

        public static string[] GetRandomSaltWithHash()
        {
            string salt = GetRandomSaltAsString();
            string hashedSalt = GetSaltHashedAsString(salt);

            return new string[] { salt, hashedSalt };

        }


    }
}
