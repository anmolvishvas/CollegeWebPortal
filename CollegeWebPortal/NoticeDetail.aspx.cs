using System;
using System.Collections.Generic;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Models;

namespace CollegeWebPortal
{
    /// <summary>Shows a single notice. The NoticeID is read from the QueryString.</summary>
    public partial class NoticeDetail : BasePage
    {
        // RequiredRole left null: any authenticated user may view a notice.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id;
                if (int.TryParse(Request.QueryString["id"], out id))
                {
                    Notice notice = NoticeManager.GetById(id);
                    fvNotice.DataSource = notice == null
                        ? new List<Notice>()
                        : new List<Notice> { notice };
                }
                else
                {
                    fvNotice.DataSource = new List<Notice>();
                }
                fvNotice.DataBind();
            }
        }
    }
}
