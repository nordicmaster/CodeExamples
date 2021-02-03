using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
    public static class Converts
    {
        public static string ToBase64(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static string FromBase64(string text)
        {
            byte[] bytes = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
