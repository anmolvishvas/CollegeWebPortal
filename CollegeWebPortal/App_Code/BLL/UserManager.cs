using System.Data;
using System.Data.SqlClient;
using CollegeWebPortal.DAL;
using CollegeWebPortal.Helpers;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.BLL
{
    /// <summary>Business logic for authentication and account management.</summary>
    public static class UserManager
    {
        /// <summary>
        /// Validates a login. Returns the matching UserAccount or null.
        /// Uses a parameterized query + SHA-256 password verification.
        /// </summary>
        public static UserAccount ValidateLogin(string username, string password)
        {
            SqlParameter[] p = { new SqlParameter("@Username", username) };

            using (SqlDataReader reader = DBHelper.ExecuteReader(
                "SELECT UserID, Username, Password, Role, RefID FROM dbo.Users WHERE Username = @Username",
                false, p))
            {
                if (reader.Read())
                {
                    string storedHash = reader["Password"].ToString();
                    if (SecurityHelper.VerifyPassword(password, storedHash))
                    {
                        return new UserAccount
                        {
                            UserID = (int)reader["UserID"],
                            Username = reader["Username"].ToString(),
                            Role = reader["Role"].ToString(),
                            RefID = reader["RefID"] == System.DBNull.Value ? 0 : (int)reader["RefID"]
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>Changes a user's password after verifying the current one.</summary>
        public static bool ChangePassword(int userId, string currentPassword, string newPassword)
        {
            SqlParameter[] readParams = { new SqlParameter("@UserID", userId) };
            string currentHash = null;

            using (SqlDataReader reader = DBHelper.ExecuteReader(
                "SELECT Password FROM dbo.Users WHERE UserID = @UserID", false, readParams))
            {
                if (reader.Read())
                {
                    currentHash = reader["Password"].ToString();
                }
            }

            if (currentHash == null || !SecurityHelper.VerifyPassword(currentPassword, currentHash))
            {
                return false; // current password did not match
            }

            SqlParameter[] updateParams =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@Password", SecurityHelper.HashPassword(newPassword))
            };

            int rows = DBHelper.ExecuteNonQuery(
                "UPDATE dbo.Users SET Password = @Password WHERE UserID = @UserID", false, updateParams);
            return rows > 0;
        }

        /// <summary>Creates a login row (used when Admin adds a student/faculty).</summary>
        public static void CreateUser(string username, string password, string role, int refId)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", SecurityHelper.HashPassword(password)),
                new SqlParameter("@Role", role),
                new SqlParameter("@RefID", refId)
            };
            DBHelper.ExecuteNonQuery(
                "IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Username=@Username) " +
                "INSERT INTO dbo.Users (Username, Password, Role, RefID) VALUES (@Username,@Password,@Role,@RefID)",
                false, p);
        }
    }
}
