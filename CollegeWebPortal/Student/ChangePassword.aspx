<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="CollegeWebPortal.StudentArea.ChangePassword" Title="Change Password" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Change Password</h2>
    <asp:Label ID="lblMsg" runat="server" Visible="false" />

    <div class="row">
        <div class="col-lg-5">
            <div class="content-card p-4">
                <asp:ValidationSummary ID="vs" runat="server" CssClass="validation-summary mb-3"
                    ValidationGroup="pwd" DisplayMode="BulletList" />

                <div class="mb-3">
                    <label class="form-label">Current Password</label>
                    <asp:TextBox ID="txtCurrent" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCurrent"
                        ErrorMessage="Current password is required." Text="*" CssClass="field-error"
                        ValidationGroup="pwd" Display="Dynamic" />
                </div>
                <div class="mb-3">
                    <label class="form-label">New Password</label>
                    <asp:TextBox ID="txtNew" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNew"
                        ErrorMessage="New password is required." Text="*" CssClass="field-error"
                        ValidationGroup="pwd" Display="Dynamic" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNew"
                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d).{6,}$"
                        ErrorMessage="Password must be at least 6 characters and include a letter and a number."
                        Text="*" CssClass="field-error" ValidationGroup="pwd" Display="Dynamic" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Confirm New Password</label>
                    <asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" />
                    <%-- CompareValidator: confirm must equal new password --%>
                    <asp:CompareValidator runat="server" ControlToValidate="txtConfirm"
                        ControlToCompare="txtNew" Operator="Equal" Type="String"
                        ErrorMessage="Passwords do not match." Text="*" CssClass="field-error"
                        ValidationGroup="pwd" Display="Dynamic" />
                </div>

                <asp:Button ID="btnSave" runat="server" Text="Update Password" CssClass="btn btn-primary"
                    ValidationGroup="pwd" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
