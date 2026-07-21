using System;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.Admin
{
    public partial class ManageStudents : BasePage
    {
        public ManageStudents()
        {
            RequiredRole = "Admin";
        }

        // ----- state kept in ViewState (state management demo) -----
        private int EditingId
        {
            get { return ViewState["EditId"] == null ? 0 : (int)ViewState["EditId"]; }
            set { ViewState["EditId"] = value; }
        }
        private string Keyword
        {
            get { return ViewState["Kw"] as string ?? string.Empty; }
            set { ViewState["Kw"] = value; }
        }
        private string SortCol
        {
            get { return ViewState["SortCol"] as string ?? "RollNo"; }
            set { ViewState["SortCol"] = value; }
        }
        private bool SortAsc
        {
            get { return ViewState["SortAsc"] == null || (bool)ViewState["SortAsc"]; }
            set { ViewState["SortAsc"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDepartments();
                BindGrid();
            }
        }

        private void LoadDepartments()
        {
            ddlDepartment.DataSource = DepartmentManager.GetAll();
            ddlDepartment.DataBind();
        }

        private void BindGrid()
        {
            try
            {
                gvStudents.DataSource = StudentManager.Search(Keyword, SortCol, SortAsc);
                gvStudents.DataBind();
            }
            catch (Exception)
            {
                ShowMessage("Unable to load students right now.", false);
            }
        }

        // ----- Search / sort / paging -----
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Keyword = txtSearch.Text.Trim();
            gvStudents.PageIndex = 0;
            BindGrid();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            Keyword = string.Empty;
            txtSearch.Text = string.Empty;
            gvStudents.PageIndex = 0;
            BindGrid();
        }

        protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudents.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvStudents_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (SortCol == e.SortExpression)
                SortAsc = !SortAsc;      // toggle direction
            else
            {
                SortCol = e.SortExpression;
                SortAsc = true;
            }
            BindGrid();
        }

        // ----- Row actions (edit / delete) -----
        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id)) return;

            if (e.CommandName == "EditRow")
            {
                LoadForEdit(id);
            }
            else if (e.CommandName == "DeleteRow")
            {
                try
                {
                    StudentManager.Delete(id);
                    ShowMessage("Student deleted successfully.", true);
                    ClearForm();
                    BindGrid();
                }
                catch (Exception)
                {
                    ShowMessage("Unable to delete this student.", false);
                }
            }
        }

        private void LoadForEdit(int id)
        {
            Student s = StudentManager.GetById(id);
            if (s == null) return;

            EditingId = s.StudentID;
            txtRollNo.Text = s.RollNo;
            txtFirst.Text = s.FirstName;
            txtLast.Text = s.LastName;
            txtEmail.Text = s.Email;
            txtPhone.Text = s.Phone;
            txtSemester.Text = s.Semester.ToString();
            txtAddress.Text = s.Address;
            if (ddlDepartment.Items.FindByValue(s.DepartmentID.ToString()) != null)
                ddlDepartment.SelectedValue = s.DepartmentID.ToString();

            lblFormTitle.Text = "Edit Student";
        }

        // ----- Save (insert or update via stored procedures) -----
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("student");
            if (!Page.IsValid) return;

            try
            {
                Student s = new Student
                {
                    StudentID = EditingId,
                    RollNo = txtRollNo.Text.Trim(),
                    FirstName = txtFirst.Text.Trim(),
                    LastName = txtLast.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    DepartmentID = int.Parse(ddlDepartment.SelectedValue),
                    Semester = int.Parse(txtSemester.Text),
                    Address = txtAddress.Text.Trim()
                };

                if (EditingId == 0)
                {
                    int newId = StudentManager.Insert(s);
                    // Auto-create a student login (username = Roll No, default password)
                    UserManager.CreateUser(s.RollNo, "Student@123", "Student", newId);
                    ShowMessage("Student added. Login created with default password 'Student@123'.", true);
                }
                else
                {
                    StudentManager.Update(s);
                    ShowMessage("Student updated successfully.", true);
                }

                ClearForm();
                BindGrid();
            }
            catch (Exception)
            {
                ShowMessage("Could not save the student. The Roll No may already exist.", false);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            EditingId = 0;
            txtRollNo.Text = txtFirst.Text = txtLast.Text = txtEmail.Text =
                txtPhone.Text = txtAddress.Text = string.Empty;
            txtSemester.Text = "1";
            if (ddlDepartment.Items.Count > 0) ddlDepartment.SelectedIndex = 0;
            lblFormTitle.Text = "Add Student";
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
