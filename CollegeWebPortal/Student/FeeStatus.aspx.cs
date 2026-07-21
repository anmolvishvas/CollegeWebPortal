using System;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;

namespace CollegeWebPortal.StudentArea
{
    public partial class FeeStatus : BasePage
    {
        public FeeStatus() { RequiredRole = "Student"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    gv.DataSource = FeeManager.GetByStudent(SessionHelper.RefId);
                    gv.DataBind();
                }
                catch (Exception) { gv.DataSource = null; gv.DataBind(); }
            }
        }
    }
}
