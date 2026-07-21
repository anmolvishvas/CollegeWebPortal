using System;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;

namespace CollegeWebPortal.StudentArea
{
    public partial class ChangePassword : BasePage
    {
        public ChangePassword() { RequiredRole = "Student"; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("pwd");
            if (!Page.IsValid) return;

            try
            {
                bool ok = UserManager.ChangePassword(
                    SessionHelper.UserId, txtCurrent.Text, txtNew.Text);

                if (ok)
                {
                    ShowMessage("Password updated successfully.", true);
                    txtCurrent.Text = txtNew.Text = txtConfirm.Text = string.Empty;
                }
                else
                {
                    ShowMessage("Current password is incorrect.", false);
                }
            }
            catch (Exception)
            {
                ShowMessage("Could not update the password. Please try again.", false);
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
