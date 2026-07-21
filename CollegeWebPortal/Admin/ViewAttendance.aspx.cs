using System;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.Admin
{
    public partial class ViewAttendance : BasePage
    {
        public ViewAttendance() { RequiredRole = "Admin"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
                BindGrid();
            }
        }

        private void LoadFilters()
        {
            ddlStudent.Items.Clear();
            ddlStudent.Items.Add(new ListItem("All Students", "0"));
            foreach (Student s in StudentManager.GetAll())
                ddlStudent.Items.Add(new ListItem(s.RollNo + " - " + s.FullName, s.StudentID.ToString()));

            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("All Subjects", "0"));
            ddlSubject.DataTextField = "SubjectName";
            ddlSubject.DataValueField = "SubjectID";
            ddlSubject.AppendDataBoundItems = true;
            ddlSubject.DataSource = SubjectManager.GetForDropDown();
            ddlSubject.DataBind();
        }

        private void BindGrid()
        {
            try
            {
                int studentId = int.Parse(ddlStudent.SelectedValue);
                int subjectId = int.Parse(ddlSubject.SelectedValue);
                string status = ddlStatus.SelectedValue;
                gv.DataSource = AttendanceManager.Filter(studentId, subjectId, status);
                gv.DataBind();
            }
            catch (Exception)
            {
                // graceful empty grid
                gv.DataSource = null;
                gv.DataBind();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            gv.PageIndex = 0;
            BindGrid();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}
