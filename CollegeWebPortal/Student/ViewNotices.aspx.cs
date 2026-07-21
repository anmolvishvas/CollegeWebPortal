using System;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;

namespace CollegeWebPortal.StudentArea
{
    public partial class ViewNotices : BasePage
    {
        public ViewNotices() { RequiredRole = "Student"; }

        private string Keyword
        {
            get { return ViewState["Kw"] as string ?? string.Empty; }
            set { ViewState["Kw"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid()
        {
            try { gv.DataSource = NoticeManager.Search(Keyword); gv.DataBind(); }
            catch (Exception) { gv.DataSource = null; gv.DataBind(); }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Keyword = txtSearch.Text.Trim();
            gv.PageIndex = 0;
            BindGrid();
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Keyword = string.Empty;
            txtSearch.Text = string.Empty;
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
