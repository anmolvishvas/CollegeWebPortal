<%@ Application Language="C#" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Web.Security" %>

<script runat="server">
    // Application-level events.

    void Application_Start(object sender, EventArgs e)
    {
    }

    void Session_Start(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Rebuilds the user principal from the Forms-auth cookie so that the role
    /// (stored in ticket UserData) is available for SiteMap security trimming
    /// and role-based folder authorization.
    /// </summary>
    void Application_PostAuthenticateRequest(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        if (context.User != null &&
            context.User.Identity.IsAuthenticated &&
            context.User.Identity is FormsIdentity)
        {
            FormsIdentity identity = (FormsIdentity)context.User.Identity;
            string role = identity.Ticket.UserData;
            string[] roles = string.IsNullOrEmpty(role) ? new string[0] : new string[] { role };
            context.User = new GenericPrincipal(identity, roles);
        }
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Last line of defence: keep raw exceptions away from the user.
        Exception ex = Server.GetLastError();
        if (ex != null)
        {
            Application["LastError"] = ex.Message;
        }
    }

    void Session_End(object sender, EventArgs e)
    {
    }

    void Application_End(object sender, EventArgs e)
    {
    }
</script>
