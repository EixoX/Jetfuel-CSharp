using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace EixoX.Web
{
    public static class Gravatar
    {
        private static readonly MD5 _md5 = MD5.Create();

        public static string HashEncode(string email)
        {
            email = email.Trim().ToLower();
            byte[] inbytes = Encoding.UTF8.GetBytes(email);
            byte[] outbytes = _md5.ComputeHash(inbytes);
            return StringHelper.HexEncode(outbytes);
        }

        public static string GetGravatarImage(string email, bool ishttps, int width)
        {

            return 
                ishttps ? 
                   string.Concat("https://www.gravatar.com/avatar/", HashEncode(email), "?s=", width):
                   string.Concat("http://www.gravatar.com/avatar/", HashEncode(email), "?s=", width);

        }

        public static string GetGravatarImage(string email, int width)
        {
            return GetGravatarImage(email, false, width);
        }

        public static string GetGravatarImage(string email, bool ishttps)
        {
            return GetGravatarImage(email, ishttps, 80);
        }

        public static string GetGravatarImage(string email)
        {
            return GetGravatarImage(email, false, 80);
        }


        
    }
}
