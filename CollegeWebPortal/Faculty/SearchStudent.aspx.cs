using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.FacultyArea
{
    public partial class SearchStudent : BasePage
    {
        public SearchStudent() { RequiredRole = "Faculty"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid(string.Empty);
                BindDetails(null);
            }
        }

        private void BindGrid(string keyword)
        {
            try
            {
                gv.DataSource = StudentManager.Search(keyword);
                gv.DataBind();
            }
            catch (Exception)
            {
                gv.DataSource = null;
                gv.DataBind();
            }
        }

        private void BindDetails(Student s)
        {
            fv.DataSource = s == null ? new List<Student>() : new List<Student> { s };
            fv.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid(txtSearch.Text.Trim());
        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "ViewRow") return;
            int id;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id)) return;

            Student s = StudentManager.GetById(id);
            BindDetails(s);
        }
    }
}
