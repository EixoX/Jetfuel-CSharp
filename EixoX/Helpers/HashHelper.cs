using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public static class HashHelper
    {
        public static string Md5Hash(string input)
        {
            byte[] iBytes = Encoding.UTF8.GetBytes(input);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] oBytes = md5.ComputeHash(iBytes);
            return Convert.ToBase64String(iBytes);
        }
    }
}
