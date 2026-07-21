using System;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;

namespace CollegeWebPortal.StudentArea
{
    public partial class ViewAttendance : BasePage
    {
        public ViewAttendance() { RequiredRole = "Student"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblPct.Text = AttendanceManager.GetPercentage(SessionHelper.RefId).ToString();
                BindGrid();
            }
        }

        private void BindGrid()
        {
            try
            {
                gv.DataSource = AttendanceManager.GetByStudent(SessionHelper.RefId);
                gv.DataBind();
            }
            catch (Exception) { gv.DataSource = null; gv.DataBind(); }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}
