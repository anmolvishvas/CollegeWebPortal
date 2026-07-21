using System;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;

namespace CollegeWebPortal.Admin
{
    public partial class Dashboard : BasePage
    {
        public Dashboard()
        {
            RequiredRole = "Admin";   // only admins may view this page
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblWelcome.Text = SessionHelper.Username;
                    LoadStats();
                    gvNotices.DataSource = NoticeManager.GetAll();
                    gvNotices.DataBind();
                }
                catch (Exception)
                {
                    // Fail gracefully - dashboard should never crash the app.
                }
            }
        }

        private void LoadStats()
        {
            lblStudents.Text = DashboardManager.Students.ToString();
            lblFaculty.Text = DashboardManager.Faculty.ToString();
            lblDepartments.Text = DashboardManager.Departments.ToString();
            lblCourses.Text = DashboardManager.Courses.ToString();
            lblSubjects.Text = DashboardManager.Subjects.ToString();
            lblNotices.Text = DashboardManager.Notices.ToString();
        }

        /// <summary>AJAX Timer tick - updates the live clock only (partial render).</summary>
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("dddd, dd MMM yyyy  HH:mm:ss");
        }
    }
}
