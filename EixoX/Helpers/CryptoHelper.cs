using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public static class CryptoHelper
    {
        public static string Md5Hash(string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create("SHA1");
            byte[] output = md5.ComputeHash(data);
            return Convert.ToBase64String(output);
        }
    }
}
