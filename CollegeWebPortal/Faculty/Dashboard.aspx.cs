using System;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.FacultyArea
{
    public partial class Dashboard : BasePage
    {
        public Dashboard() { RequiredRole = "Faculty"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Faculty f = FacultyManager.GetById(SessionHelper.RefId);
                    lblName.Text = f != null ? f.FullName : SessionHelper.Username;
                    lblDept.Text = f != null ? f.DepartmentName : string.Empty;

                    gvNotices.DataSource = NoticeManager.GetAll();
                    gvNotices.DataBind();
                }
                catch (Exception) { }
            }
        }
    }
}
