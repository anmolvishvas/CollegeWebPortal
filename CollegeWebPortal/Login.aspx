<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs"
    Inherits="CollegeWebPortal.Login" EnableTheming="false" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Login - College Web Portal</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
    <link href="CSS/site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="login-wrapper">
            <div class="card login-card">
                <div class="card-body p-4 p-md-5">

                    <div class="text-center mb-4">
                        <img src="Images/logo.svg" alt="Logo" height="56" />
                        <h4 class="mt-3 mb-0 fw-bold" style="color:#1e3a8a;">College Web Portal</h4>
                        <p class="text-muted small">Sign in to continue</p>
                    </div>

                    <asp:ValidationSummary ID="vsLogin" runat="server" CssClass="validation-summary mb-3"
                        DisplayMode="BulletList" ValidationGroup="login" HeaderText="Please fix the following:" />

                    <!-- Server-side login error message -->
                    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block"
                        Visible="false" />

                    <!-- Username -->
                    <div class="mb-3">
                        <label class="form-label">Username</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="bi bi-person"></i></span>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"
                                placeholder="admin / jsmith / CS2101" />
                        </div>
                        <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="txtUsername"
                            ErrorMessage="Username is required." Text="*" CssClass="field-error"
                            ValidationGroup="login" Display="Dynamic" />
                    </div>

                    <!-- Password -->
                    <div class="mb-3">
                        <label class="form-label">Password</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="bi bi-lock"></i></span>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"
                                TextMode="Password" placeholder="Password" />
                        </div>
                        <asp:RequiredFieldValidator ID="rfvPass" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Password is required." Text="*" CssClass="field-error"
                            ValidationGroup="login" Display="Dynamic" />
                    </div>

                    <!-- Captcha (partial update via AJAX UpdatePanel) -->
                    <div class="mb-3">
                        <label class="form-label">Captcha</label>
                        <asp:UpdatePanel ID="upCaptcha" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="d-flex align-items-center gap-2 mb-2">
                                    <span class="captcha-box"><asp:Label ID="lblCaptcha" runat="server" /></span>
                                    <asp:LinkButton ID="btnRefreshCaptcha" runat="server" CssClass="btn btn-outline-secondary btn-sm"
                                        CausesValidation="false" OnClick="btnRefreshCaptcha_Click" ToolTip="Refresh captcha">
                                        <i class="bi bi-arrow-clockwise"></i>
                                    </asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control"
                            placeholder="Enter the code above" autocomplete="off" />
                        <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ControlToValidate="txtCaptcha"
                            ErrorMessage="Captcha is required." Text="*" CssClass="field-error"
                            ValidationGroup="login" Display="Dynamic" />
                        <asp:CustomValidator ID="cvCaptcha" runat="server" ControlToValidate="txtCaptcha"
                            ErrorMessage="Captcha does not match." Text="*" CssClass="field-error"
                            ValidationGroup="login" Display="Dynamic" OnServerValidate="cvCaptcha_ServerValidate" />
                    </div>

                    <!-- Remember Me (stored in a cookie) -->
                    <div class="form-check mb-3">
                        <asp:CheckBox ID="chkRemember" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label">Remember my username</label>
                    </div>

                    <div class="d-grid">
                        <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="btn btn-primary btn-lg"
                            ValidationGroup="login" OnClick="btnLogin_Click" />
                    </div>

                    <hr class="my-4" />
                    <div class="small text-muted">
                        <strong>Demo logins</strong> (password shown):<br />
                        Admin: <code>admin / Admin@123</code><br />
                        Faculty: <code>jsmith / Faculty@123</code><br />
                        Student: <code>CS2101 / Student@123</code>
                    </div>
                </div>
            </div>
        </div>

        <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>
