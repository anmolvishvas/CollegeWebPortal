using System;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using CollegeWebPortal.Helpers;

namespace CollegeWebPortal
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Show the current user (from Session) in the navbar.
            lblUser.Text = SessionHelper.Username;
            lblRole.Text = SessionHelper.Role;
            litYear.Text = DateTime.Now.Year.ToString();

            if (!IsPostBack)
            {
                BuildSidebar(SessionHelper.Role);
            }
        }

        /// <summary>Builds the role-based sidebar navigation as Bootstrap list links.</summary>
        private void BuildSidebar(string role)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='list-group list-group-flush'>");

            if (role == "Admin")
            {
                lblSidebarTitle.Text = "Admin Panel";
                AddLink(sb, "~/Admin/Dashboard.aspx", "speedometer2", "Dashboard");
                AddLink(sb, "~/Admin/ManageStudents.aspx", "people", "Manage Students");
                AddLink(sb, "~/Admin/ManageFaculty.aspx", "person-badge", "Manage Faculty");
                AddLink(sb, "~/Admin/ManageDepartments.aspx", "building", "Departments");
                AddLink(sb, "~/Admin/ManageCourses.aspx", "journal-bookmark", "Courses");
                AddLink(sb, "~/Admin/ManageSubjects.aspx", "book", "Subjects");
                AddLink(sb, "~/Admin/ManageNotices.aspx", "megaphone", "Notices");
                AddLink(sb, "~/Admin/ViewAttendance.aspx", "calendar-check", "View Attendance");
                AddLink(sb, "~/Admin/ViewMarks.aspx", "clipboard-data", "View Marks");
            }
            else if (role == "Faculty")
            {
                lblSidebarTitle.Text = "Faculty Panel";
                AddLink(sb, "~/Faculty/Dashboard.aspx", "speedometer2", "Dashboard");
                AddLink(sb, "~/Faculty/ManageAttendance.aspx", "calendar-check", "Manage Attendance");
                AddLink(sb, "~/Faculty/UploadMarks.aspx", "upload", "Upload Marks");
                AddLink(sb, "~/Faculty/SearchStudent.aspx", "search", "Search Student");
                AddLink(sb, "~/Faculty/ViewNotices.aspx", "megaphone", "Notices");
            }
            else if (role == "Student")
            {
                lblSidebarTitle.Text = "Student Panel";
                AddLink(sb, "~/Student/Dashboard.aspx", "speedometer2", "Dashboard");
                AddLink(sb, "~/Student/Profile.aspx", "person-vcard", "My Profile");
                AddLink(sb, "~/Student/ViewAttendance.aspx", "calendar-check", "My Attendance");
                AddLink(sb, "~/Student/ViewMarks.aspx", "clipboard-data", "My Marks");
                AddLink(sb, "~/Student/ViewNotices.aspx", "megaphone", "Notices");
                AddLink(sb, "~/Student/FeeStatus.aspx", "cash-coin", "Fee Status");
                AddLink(sb, "~/Student/ChangePassword.aspx", "key", "Change Password");
            }

            sb.Append("</div>");
            litSidebar.Text = sb.ToString();
        }

        private void AddLink(StringBuilder sb, string url, string icon, string text)
        {
            sb.AppendFormat(
                "<a class='list-group-item list-group-item-action' href='{0}'>" +
                "<i class='bi bi-{1} me-2'></i>{2}</a>",
                ResolveUrl(url), icon, text);
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Destroy the session and the forms-auth cookie, then go to Logout page.
            SessionHelper.SignOut();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Logout.aspx");
        }
    }
}
