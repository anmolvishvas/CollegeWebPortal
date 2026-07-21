using System;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;

namespace CollegeWebPortal.StudentArea
{
    public partial class ViewMarks : BasePage
    {
        public ViewMarks() { RequiredRole = "Student"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    gv.DataSource = MarksManager.GetByStudent(SessionHelper.RefId);
                    gv.DataBind();
                }
                catch (Exception) { gv.DataSource = null; gv.DataBind(); }
            }
        }
    }
}
