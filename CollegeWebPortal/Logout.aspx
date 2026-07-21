<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs"
    Inherits="CollegeWebPortal.Logout" EnableTheming="false" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Logged out - College Web Portal</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
    <link href="CSS/site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-wrapper">
            <div class="card login-card text-center">
                <div class="card-body p-5">
                    <i class="bi bi-check-circle-fill text-success" style="font-size:3rem;"></i>
                    <h4 class="mt-3 fw-bold" style="color:#1e3a8a;">You have been logged out</h4>
                    <p class="text-muted">Your session has ended securely.</p>
                    <a href="Login.aspx" class="btn btn-primary mt-2">
                        <i class="bi bi-box-arrow-in-right"></i> Sign in again
                    </a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
