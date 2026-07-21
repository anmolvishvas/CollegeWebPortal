using System;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.Admin
{
    public partial class ManageFaculty : BasePage
    {
        public ManageFaculty() { RequiredRole = "Admin"; }

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
            try
            {
                gvFaculty.DataSource = FacultyManager.GetAll();
                gvFaculty.DataBind();
            }
            catch (Exception)
            {
                ShowMessage("Unable to load faculty.", false);
            }
        }

        protected void gvFaculty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFaculty.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvFaculty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id)) return;

            if (e.CommandName == "EditRow")
            {
                Faculty f = FacultyManager.GetById(id);
                if (f == null) return;
                EditingId = f.FacultyID;
                txtFirst.Text = f.FirstName;
                txtLast.Text = f.LastName;
                txtEmail.Text = f.Email;
                txtPhone.Text = f.Phone;
                if (ddlDepartment.Items.FindByValue(f.DepartmentID.ToString()) != null)
                    ddlDepartment.SelectedValue = f.DepartmentID.ToString();
                lblFormTitle.Text = "Edit Faculty";
            }
            else if (e.CommandName == "DeleteRow")
            {
                try
                {
                    FacultyManager.Delete(id);
                    ShowMessage("Faculty deleted.", true);
                    ClearForm();
                    BindGrid();
                }
                catch (Exception)
                {
                    ShowMessage("Unable to delete (faculty may have linked records).", false);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("fac");
            if (!Page.IsValid) return;

            try
            {
                Faculty f = new Faculty
                {
                    FacultyID = EditingId,
                    FirstName = txtFirst.Text.Trim(),
                    LastName = txtLast.Text.Trim(),
                    DepartmentID = int.Parse(ddlDepartment.SelectedValue),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim()
                };

                if (EditingId == 0)
                {
                    int newId = FacultyManager.Insert(f);
                    string username = (f.FirstName.Substring(0, 1) + f.LastName).ToLower();
                    UserManager.CreateUser(username, "Faculty@123", "Faculty", newId);
                    ShowMessage("Faculty added. Login '" + username + "' created (password 'Faculty@123').", true);
                }
                else
                {
                    FacultyManager.Update(f);
                    ShowMessage("Faculty updated.", true);
                }
                ClearForm();
                BindGrid();
            }
            catch (Exception)
            {
                ShowMessage("Could not save the faculty member.", false);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e) { ClearForm(); }

        private void ClearForm()
        {
            EditingId = 0;
            txtFirst.Text = txtLast.Text = txtEmail.Text = txtPhone.Text = string.Empty;
            if (ddlDepartment.Items.Count > 0) ddlDepartment.SelectedIndex = 0;
            lblFormTitle.Text = "Add Faculty";
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
