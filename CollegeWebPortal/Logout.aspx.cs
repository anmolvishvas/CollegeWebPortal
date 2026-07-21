using System;
using System.Web.Security;
using CollegeWebPortal.Helpers;

namespace CollegeWebPortal
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure the session and forms-auth cookie are fully cleared.
            SessionHelper.SignOut();
            FormsAuthentication.SignOut();
        }
    }
}
