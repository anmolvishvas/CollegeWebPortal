using System.Web;
using System.Web.SessionState;

namespace CollegeWebPortal.Helpers
{
    /// <summary>
    /// Strongly-typed wrapper around the ASP.NET Session for the logged-in user.
    /// Demonstrates Session state management used across the whole portal.
    /// </summary>
    public static class SessionHelper
    {
        public const string KeyUserId = "UserID";
        public const string KeyUsername = "Username";
        public const string KeyRole = "Role";
        public const string KeyRefId = "RefID";     // StudentID or FacultyID

        private static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        public static bool IsLoggedIn
        {
            get { return Session != null && Session[KeyUserId] != null; }
        }

        public static int UserId
        {
            get { return Session[KeyUserId] != null ? (int)Session[KeyUserId] : 0; }
        }

        public static string Username
        {
            get { return Session[KeyUsername] as string ?? string.Empty; }
        }

        public static string Role
        {
            get { return Session[KeyRole] as string ?? string.Empty; }
        }

        public static int RefId
        {
            get { return Session[KeyRefId] != null ? (int)Session[KeyRefId] : 0; }
        }

        public static void SignIn(int userId, string username, string role, int refId)
        {
            Session[KeyUserId] = userId;
            Session[KeyUsername] = username;
            Session[KeyRole] = role;
            Session[KeyRefId] = refId;
        }

        public static void SignOut()
        {
            Session.Clear();
            Session.Abandon();
        }
    }
}
