using System;
using System.Data;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;

namespace CollegeWebPortal.Admin
{
    public partial class ManageCourses : BasePage
    {
        public ManageCourses() { RequiredRole = "Admin"; }

        private int EditingId
        {
            get { return ViewState["EditId"] == null ? 0 : (int)ViewState["EditId"]; }
            set { ViewState["EditId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDepartment.DataSource = DepartmentManager.GetAll();
                ddlDepartment.DataBind();
                BindGrid();
            }
        }

        private void BindGrid()
        {
            try { gv.DataSource = CourseManager.GetAll(); gv.DataBind(); }
            catch (Exception) { ShowMessage("Unable to load courses.", false); }
        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id)) return;

            if (e.CommandName == "EditRow")
            {
                DataTable dt = CourseManager.GetAll();
                foreach (DataRow r in dt.Rows)
                {
                    if ((int)r["CourseID"] == id)
                    {
                        EditingId = id;
                        txtName.Text = r["CourseName"].ToString();
                        string deptId = r["DepartmentID"] == DBNull.Value ? "" : r["DepartmentID"].ToString();
                        if (ddlDepartment.Items.FindByValue(deptId) != null)
                            ddlDepartment.SelectedValue = deptId;
                        lblFormTitle.Text = "Edit Course";
                        break;
                    }
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                try { CourseManager.Delete(id); ShowMessage("Course deleted.", true); ClearForm(); BindGrid(); }
                catch (Exception) { ShowMessage("Cannot delete - course is in use.", false); }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("course");
            if (!Page.IsValid) return;
            try
            {
                int dept = int.Parse(ddlDepartment.SelectedValue);
                if (EditingId == 0) CourseManager.Insert(txtName.Text.Trim(), dept);
                else CourseManager.Update(EditingId, txtName.Text.Trim(), dept);
                ShowMessage("Saved successfully.", true);
                ClearForm();
                BindGrid();
            }
            catch (Exception) { ShowMessage("Could not save course.", false); }
        }

        protected void btnClear_Click(object sender, EventArgs e) { ClearForm(); }

        private void ClearForm()
        {
            EditingId = 0;
            txtName.Text = string.Empty;
            if (ddlDepartment.Items.Count > 0) ddlDepartment.SelectedIndex = 0;
            lblFormTitle.Text = "Add Course";
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
