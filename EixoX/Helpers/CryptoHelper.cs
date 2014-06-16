using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EixoX
{
    public static class CryptoHelper
    {
        public static string Md5Hash(string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] output = md5.ComputeHash(data);
            return Convert.ToBase64String(output);
        }

        public static string Sha1Hash(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
        }

        public static string Sha1(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            byte[] outbuffer = cryptoTransformSHA1.ComputeHash(buffer);
            return Convert.ToBase64String(outbuffer);
        }
    }
}
