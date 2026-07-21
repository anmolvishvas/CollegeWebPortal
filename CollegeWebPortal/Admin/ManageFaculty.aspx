<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageFaculty.aspx.cs" Inherits="CollegeWebPortal.Admin.ManageFaculty" Title="Manage Faculty" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="page-title mb-3">Manage Faculty</h2>
    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block alert-auto-dismiss" Visible="false" />

    <div class="row g-3">
        <div class="col-lg-4">
            <div class="content-card p-3">
                <h5 class="mb-3"><asp:Label ID="lblFormTitle" runat="server" Text="Add Faculty" /></h5>
                <asp:ValidationSummary ID="vs" runat="server" CssClass="validation-summary mb-2"
                    ValidationGroup="fac" DisplayMode="BulletList" />

                <div class="row">
                    <div class="col-6 mb-2">
                        <label class="form-label">First Name</label>
                        <asp:TextBox ID="txtFirst" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirst"
                            ErrorMessage="First name is required." Text="*" CssClass="field-error"
                            ValidationGroup="fac" Display="Dynamic" />
                    </div>
                    <div class="col-6 mb-2">
                        <label class="form-label">Last Name</label>
                        <asp:TextBox ID="txtLast" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLast"
                            ErrorMessage="Last name is required." Text="*" CssClass="field-error"
                            ValidationGroup="fac" Display="Dynamic" />
                    </div>
                </div>

                <div class="mb-2">
                    <label class="form-label">Department</label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" DataTextField="DepartmentName"
                        DataValueField="DepartmentID" />
                </div>

                <div class="mb-2">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
                        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                        ErrorMessage="Enter a valid email." Text="*" CssClass="field-error"
                        ValidationGroup="fac" Display="Dynamic" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Phone</label>
                    <asp:TextBox ID="txtPhone" runat="server" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPhone"
                        ValidationExpression="^[0-9]{10}$"
                        ErrorMessage="Phone must be 10 digits." Text="*" CssClass="field-error"
                        ValidationGroup="fac" Display="Dynamic" />
                </div>

                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                    ValidationGroup="fac" OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary"
                    CausesValidation="false" OnClick="btnClear_Click" />
            </div>
        </div>

        <div class="col-lg-8">
            <div class="content-card p-3">
                <asp:GridView ID="gvFaculty" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="8"
                    OnPageIndexChanging="gvFaculty_PageIndexChanging"
                    OnRowCommand="gvFaculty_RowCommand" DataKeyNames="FacultyID"
                    CssClass="table table-striped table-hover table-bordered align-middle">
                    <PagerStyle CssClass="gv-pager" />
                    <Columns>
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-primary"
                                    CommandName="EditRow" CommandArgument='<%# Eval("FacultyID") %>'>
                                    <i class="bi bi-pencil"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="DeleteRow" CommandArgument='<%# Eval("FacultyID") %>'
                                    OnClientClick="return confirmDelete('Delete this faculty member?');">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No faculty found.</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
