using System;
using System.Web.UI;
using CollegeWebPortal.Helpers;

namespace CollegeWebPortal
{
    /// <summary>
    /// Base class for secured content pages. Set <see cref="RequiredRole"/> in a
    /// page's constructor (or leave null to allow any logged-in user). Pages that
    /// fail the check are redirected to the Login page.
    /// </summary>
    public class BasePage : Page
    {
        /// <summary>Role required to view the page (Admin/Faculty/Student), or null for any.</summary>
        protected string RequiredRole { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!SessionHelper.IsLoggedIn)
            {
                Response.Redirect("~/Login.aspx", true);
                return;
            }

            if (!string.IsNullOrEmpty(RequiredRole) &&
                !string.Equals(SessionHelper.Role, RequiredRole, StringComparison.OrdinalIgnoreCase))
            {
                // Logged in but wrong role -> send to their own dashboard
                Response.Redirect("~/Default.aspx", true);
            }
        }
    }
}
