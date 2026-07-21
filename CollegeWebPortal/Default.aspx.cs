using System;
using CollegeWebPortal.Helpers;

namespace CollegeWebPortal
{
    /// <summary>
    /// Landing page. Routes the user to the correct dashboard based on the role
    /// stored in Session, or to Login if not authenticated.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionHelper.IsLoggedIn)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            switch (SessionHelper.Role)
            {
                case "Admin":
                    Response.Redirect("~/Admin/Dashboard.aspx");
                    break;
                case "Faculty":
                    Response.Redirect("~/Faculty/Dashboard.aspx");
                    break;
                case "Student":
                    Response.Redirect("~/Student/Dashboard.aspx");
                    break;
                default:
                    Response.Redirect("~/Login.aspx");
                    break;
            }
        }
    }
}
