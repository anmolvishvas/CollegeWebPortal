using System;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal.FacultyArea
{
    public partial class UploadMarks : BasePage
    {
        public UploadMarks() { RequiredRole = "Faculty"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSubject.DataSource = SubjectManager.GetForDropDown();
                ddlSubject.DataBind();

                ddlStudent.Items.Clear();
                foreach (Student s in StudentManager.GetAll())
                    ddlStudent.Items.Add(new ListItem(s.RollNo + " - " + s.FullName, s.StudentID.ToString()));

                BindGrid();
            }
        }

        private void BindGrid()
        {
            try { gv.DataSource = MarksManager.GetAll(); gv.DataBind(); }
            catch (Exception) { gv.DataSource = null; gv.DataBind(); }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("mk");
            if (!Page.IsValid) return;

            if (ddlStudent.SelectedItem == null || ddlSubject.SelectedItem == null)
            {
                ShowMessage("Please select a student and a subject.", false);
                return;
            }

            try
            {
                MarksManager.Upload(
                    int.Parse(ddlStudent.SelectedValue),
                    int.Parse(ddlSubject.SelectedValue),
                    int.Parse(txtInternal.Text),
                    int.Parse(txtExternal.Text));

                ShowMessage("Marks saved successfully.", true);
                BindGrid();
            }
            catch (Exception)
            {
                ShowMessage("Could not save the marks.", false);
            }
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
