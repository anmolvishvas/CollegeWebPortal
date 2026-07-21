using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using CollegeWebPortal.BLL;
using CollegeWebPortal.Helpers;
using CollegeWebPortal.Models;

namespace CollegeWebPortal
{
    public partial class Login : System.Web.UI.Page
    {
        private const string CaptchaKey = "LoginCaptcha";
        private const string RememberCookie = "RememberUser";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Already signed in? Go straight to the role dashboard.
            if (!IsPostBack && SessionHelper.IsLoggedIn)
            {
                RedirectByRole(SessionHelper.Role);
                return;
            }

            if (!IsPostBack)
            {
                GenerateCaptcha();
                LoadRememberedUser();   // read the "Remember me" cookie
            }
        }

        /// <summary>Creates a fresh captcha and stores it in Session.</summary>
        private void GenerateCaptcha()
        {
            string code = CaptchaHelper.Generate(5);
            Session[CaptchaKey] = code;
            lblCaptcha.Text = code;
        }

        /// <summary>Cookies demo: prefill the username if it was remembered.</summary>
        private void LoadRememberedUser()
        {
            HttpCookie cookie = Request.Cookies[RememberCookie];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                txtUsername.Text = Server.HtmlEncode(cookie.Value);
                chkRemember.Checked = true;
            }
        }

        protected void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
            upCaptcha.Update();
        }

        /// <summary>Server-side captcha check.</summary>
        protected void cvCaptcha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string expected = Session[CaptchaKey] as string ?? string.Empty;
            args.IsValid = !string.IsNullOrEmpty(expected) &&
                           string.Equals(args.Value.Trim(), expected, StringComparison.OrdinalIgnoreCase);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Page.Validate("login");
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                UserAccount user = UserManager.ValidateLogin(txtUsername.Text.Trim(), txtPassword.Text);
                if (user == null)
                {
                    ShowError("Invalid username or password.");
                    GenerateCaptcha();
                    upCaptcha.Update();
                    return;
                }

                // ----- Session login -----
                SessionHelper.SignIn(user.UserID, user.Username, user.Role, user.RefID);

                // ----- Forms-auth cookie (role kept in UserData for SiteMap trimming) -----
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(30), false, user.Role);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encrypted));

                // ----- Remember Me cookie -----
                if (chkRemember.Checked)
                {
                    HttpCookie remember = new HttpCookie(RememberCookie, user.Username)
                    {
                        Expires = DateTime.Now.AddDays(30)
                    };
                    Response.Cookies.Add(remember);
                }
                else
                {
                    HttpCookie remember = new HttpCookie(RememberCookie) { Expires = DateTime.Now.AddDays(-1) };
                    Response.Cookies.Add(remember);
                }

                RedirectByRole(user.Role);
            }
            catch (Exception)
            {
                // Never expose raw SQL/system exceptions to the user.
                ShowError("Something went wrong while signing in. Please try again.");
                GenerateCaptcha();
                upCaptcha.Update();
            }
        }

        private void RedirectByRole(string role)
        {
            switch (role)
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
                    Response.Redirect("~/Default.aspx");
                    break;
            }
        }

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }
    }
}
