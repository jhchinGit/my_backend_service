using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MyBackendService.Utility
{
    public static class CryptoHelper
    {
        public static (byte[] salt, string storeSalt) GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return (salt, ConvertByteArrayToString(salt));
        }

        public static string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                    ));
        }

        public static byte[] ConvertStringToByteArray(string stringToConvert)
        {
            return Encoding.UTF8.GetBytes(stringToConvert);
        }

        public static string ConvertByteArrayToString(byte[] byteArray)
        {
            return Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
        }
    }
}