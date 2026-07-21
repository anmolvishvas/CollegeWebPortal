using System;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;

namespace CollegeWebPortal.Admin
{
    public partial class ViewMarks : BasePage
    {
        public ViewMarks() { RequiredRole = "Admin"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid()
        {
            try { gv.DataSource = MarksManager.GetAll(); gv.DataBind(); }
            catch (Exception) { gv.DataSource = null; gv.DataBind(); }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}
