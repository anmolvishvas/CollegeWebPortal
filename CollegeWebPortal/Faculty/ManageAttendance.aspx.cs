using System;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.FacultyArea
{
    public partial class ManageAttendance : BasePage
    {
        public ManageAttendance() { RequiredRole = "Faculty"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropDowns();
                calDate.SelectedDate = DateTime.Today;
                calDate.VisibleDate = DateTime.Today;
                BindGrid();
            }
            lblSelectedDate.Text = calDate.SelectedDate == DateTime.MinValue
                ? "(none)"
                : calDate.SelectedDate.ToString("dd MMM yyyy");
        }

        private void LoadDropDowns()
        {
            ddlSubject.DataSource = SubjectManager.GetForDropDown();
            ddlSubject.DataBind();

            ddlStudent.Items.Clear();
            foreach (Student s in StudentManager.GetAll())
                ddlStudent.Items.Add(new ListItem(s.RollNo + " - " + s.FullName, s.StudentID.ToString()));
        }

        private void BindGrid()
        {
            try { gv.DataSource = AttendanceManager.GetAll(); gv.DataBind(); }
            catch (Exception) { gv.DataSource = null; gv.DataBind(); }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (calDate.SelectedDate == DateTime.MinValue)
            {
                ShowMessage("Please select a date on the calendar.", false);
                return;
            }
            if (ddlStudent.SelectedItem == null || ddlSubject.SelectedItem == null)
            {
                ShowMessage("Please select a subject and a student.", false);
                return;
            }

            try
            {
                AttendanceManager.Mark(
                    int.Parse(ddlStudent.SelectedValue),
                    int.Parse(ddlSubject.SelectedValue),
                    SessionHelper.RefId,
                    calDate.SelectedDate,
                    rblStatus.SelectedValue);

                ShowMessage("Attendance saved successfully.", true);
                BindGrid();
            }
            catch (Exception)
            {
                ShowMessage("Could not save attendance.", false);
            }
        }

        private void ShowMessage(string text, bool success)
        {
            lblMsg.Text = text;
            lblMsg.CssClass = success
                ? "alert alert-success d-block alert-auto-dismiss"
                : "alert alert-danger d-block";
            lblMsg.Visible = true;
        }
    }
}
