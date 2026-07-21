using System;
using System.IO;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;

namespace CollegeWebPortal.Admin
{
    public partial class ManageNotices : BasePage
    {
        public ManageNotices() { RequiredRole = "Admin"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid()
        {
            try { gv.DataSource = NoticeManager.GetAll(); gv.DataBind(); }
            catch (Exception) { ShowMessage("Unable to load notices.", false); }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            Page.Validate("notice");
            if (!Page.IsValid) return;

            try
            {
                // Optional file upload demonstration (FileUpload control)
                if (fuAttachment.HasFile)
                {
                    if (fuAttachment.PostedFile.ContentLength > 2 * 1024 * 1024)
                    {
                        ShowMessage("Attachment is too large (max 2 MB).", false);
                        return;
                    }
                    string folder = Server.MapPath("~/Images/uploads/");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                    string safeName = Guid.NewGuid().ToString("N") + Path.GetExtension(fuAttachment.FileName);
                    fuAttachment.SaveAs(Path.Combine(folder, safeName));
                }

                NoticeManager.Insert(txtTitle.Text.Trim(), txtDesc.Text.Trim());
                ShowMessage("Notice posted successfully.", true);
                txtTitle.Text = txtDesc.Text = string.Empty;
                BindGrid();
            }
            catch (Exception)
            {
                ShowMessage("Could not post the notice.", false);
            }
        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "DeleteRow") return;
            int id;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id)) return;
            try
            {
                NoticeManager.Delete(id);
                ShowMessage("Notice deleted.", true);
                BindGrid();
            }
            catch (Exception) { ShowMessage("Could not delete notice.", false); }
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
