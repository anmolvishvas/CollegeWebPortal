using System;
using System.Security.Cryptography;
using System.Text;

namespace CollegeWebPortal.Helpers
{
    /// <summary>
    /// Password hashing utility.
    /// Produces an UPPERCASE hex SHA-256 hash of  (password|salt) so that the
    /// value matches HASHBYTES('SHA2_256', ...) used in the SQL sample data.
    /// </summary>
    public static class SecurityHelper
    {
        // NOTE: keep this value identical to the @salt used in CollegePortalDB.sql
        private const string Salt = "C0lleg3P0rt@l$alt";

        /// <summary>Hash a plain-text password.</summary>
        public static string HashPassword(string password)
        {
            if (password == null) password = string.Empty;

            using (SHA256 sha = SHA256.Create())
            {
                byte[] input = Encoding.UTF8.GetBytes(password + "|" + Salt);
                byte[] hash = sha.ComputeHash(input);

                StringBuilder sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2")); // uppercase hex, matches SQL style 2
                }
                return sb.ToString();
            }
        }

        /// <summary>Compare a plain-text password with a stored hash.</summary>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash)) return false;
            return string.Equals(HashPassword(password), storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
