using System;
using System.Text;

namespace CollegeWebPortal.Helpers
{
    /// <summary>
    /// Generates simple random alphanumeric captcha codes used on the Login page.
    /// (Ambiguous characters such as 0/O and 1/I are excluded for readability.)
    /// </summary>
    public static class CaptchaHelper
    {
        private const string Chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        private static readonly Random Rnd = new Random();

        public static string Generate(int length = 5)
        {
            StringBuilder sb = new StringBuilder(length);
            lock (Rnd)
            {
                for (int i = 0; i < length; i++)
                {
                    sb.Append(Chars[Rnd.Next(Chars.Length)]);
                }
            }
            return sb.ToString();
        }
    }
}
