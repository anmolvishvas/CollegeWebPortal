using System;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;

namespace CollegeWebPortal.Admin
{
    public partial class ManageDepartments : BasePage
    {
        public ManageDepartments() { RequiredRole = "Admin"; }

        private int EditingId
        {
            get { return ViewState["EditId"] == null ? 0 : (int)ViewState["EditId"]; }
            set { ViewState["EditId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                gv.DataSource = DepartmentManager.GetAll();
                gv.DataBind();
            }
            catch (Exception) { ShowMessage("Unable to load departments.", false); }
        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id)) return;

            if (e.CommandName == "EditRow")
            {
                foreach (var d in DepartmentManager.GetAllList())
                {
                    if (d.DepartmentID == id)
                    {
                        EditingId = id;
                        txtName.Text = d.DepartmentName;
                        lblFormTitle.Text = "Edit Department";
                        break;
                    }
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                try
                {
                    DepartmentManager.Delete(id);
                    ShowMessage("Department deleted.", true);
                    ClearForm();
                    BindGrid();
                }
                catch (Exception)
                {
                    ShowMessage("Cannot delete - department is in use.", false);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("dept");
            if (!Page.IsValid) return;
            try
            {
                if (EditingId == 0) DepartmentManager.Insert(txtName.Text.Trim());
                else DepartmentManager.Update(EditingId, txtName.Text.Trim());
                ShowMessage("Saved successfully.", true);
                ClearForm();
                BindGrid();
            }
            catch (Exception) { ShowMessage("Could not save department.", false); }
        }

        protected void btnClear_Click(object sender, EventArgs e) { ClearForm(); }

        private void ClearForm()
        {
            EditingId = 0;
            txtName.Text = string.Empty;
            lblFormTitle.Text = "Add Department";
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
