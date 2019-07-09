using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.CORE
{
    public static class StringExtensions
    {
        public static bool CleanEquals(this string firstText, string textToCompare)
        {
            string text1 = firstText.ToLower().Trim();
            string text2 = textToCompare.ToLower().Trim();

            if (text1.Equals(text2))
                return true;

            return false;
        }  

        public static string EncodeString(this string text)
        {
            byte[] byt = Encoding.UTF8.GetBytes(text);
            var textHash = Convert.ToBase64String(byt);
            return textHash;
        }

        public static string DecodeString(this string text)
        {
            byte[] byt = Convert.FromBase64String(text);
            var originalText = Encoding.UTF8.GetString(byt);
            return originalText;
        }
    }
}
