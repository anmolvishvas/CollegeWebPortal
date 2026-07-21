using System;
using System.Data;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;

namespace CollegeWebPortal.Admin
{
    public partial class ManageSubjects : BasePage
    {
        public ManageSubjects() { RequiredRole = "Admin"; }

        private int EditingId
        {
            get { return ViewState["EditId"] == null ? 0 : (int)ViewState["EditId"]; }
            set { ViewState["EditId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCourse.DataSource = CourseManager.GetAll();
                ddlCourse.DataBind();
                BindGrid();
            }
        }

        private void BindGrid()
        {
            try { gv.DataSource = SubjectManager.GetAll(); gv.DataBind(); }
            catch (Exception) { ShowMessage("Unable to load subjects.", false); }
        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id)) return;

            if (e.CommandName == "EditRow")
            {
                DataTable dt = SubjectManager.GetAll();
                foreach (DataRow r in dt.Rows)
                {
                    if ((int)r["SubjectID"] == id)
                    {
                        EditingId = id;
                        txtName.Text = r["SubjectName"].ToString();
                        string courseId = r["CourseID"] == DBNull.Value ? "" : r["CourseID"].ToString();
                        if (ddlCourse.Items.FindByValue(courseId) != null)
                            ddlCourse.SelectedValue = courseId;
                        lblFormTitle.Text = "Edit Subject";
                        break;
                    }
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                try { SubjectManager.Delete(id); ShowMessage("Subject deleted.", true); ClearForm(); BindGrid(); }
                catch (Exception) { ShowMessage("Cannot delete - subject is in use.", false); }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("sub");
            if (!Page.IsValid) return;
            try
            {
                int course = int.Parse(ddlCourse.SelectedValue);
                if (EditingId == 0) SubjectManager.Insert(txtName.Text.Trim(), course);
                else SubjectManager.Update(EditingId, txtName.Text.Trim(), course);
                ShowMessage("Saved successfully.", true);
                ClearForm();
                BindGrid();
            }
            catch (Exception) { ShowMessage("Could not save subject.", false); }
        }

        protected void btnClear_Click(object sender, EventArgs e) { ClearForm(); }

        private void ClearForm()
        {
            EditingId = 0;
            txtName.Text = string.Empty;
            if (ddlCourse.Items.Count > 0) ddlCourse.SelectedIndex = 0;
            lblFormTitle.Text = "Add Subject";
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
