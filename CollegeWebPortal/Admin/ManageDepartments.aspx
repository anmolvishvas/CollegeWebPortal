<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageDepartments.aspx.cs" Inherits="CollegeWebPortal.Admin.ManageDepartments" Title="Manage Departments" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Manage Departments</h2>
    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block alert-auto-dismiss" Visible="false" />

    <div class="row g-3">
        <div class="col-lg-4">
            <div class="content-card p-3">
                <h5 class="mb-3"><asp:Label ID="lblFormTitle" runat="server" Text="Add Department" /></h5>
                <div class="mb-3">
                    <label class="form-label">Department Name</label>
                    <asp:TextBox ID="txtName" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
                        ErrorMessage="Name is required." Text="*" CssClass="field-error"
                        ValidationGroup="dept" Display="Dynamic" />
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                    ValidationGroup="dept" OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary"
                    CausesValidation="false" OnClick="btnClear_Click" />
            </div>
        </div>
        <div class="col-lg-8">
            <div class="content-card p-3">
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
                    OnRowCommand="gv_RowCommand" DataKeyNames="DepartmentID"
                    CssClass="table table-striped table-hover table-bordered align-middle">
                    <Columns>
                        <asp:BoundField DataField="DepartmentID" HeaderText="ID" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-primary"
                                    CommandName="EditRow" CommandArgument='<%# Eval("DepartmentID") %>'>
                                    <i class="bi bi-pencil"></i></asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="DeleteRow" CommandArgument='<%# Eval("DepartmentID") %>'
                                    OnClientClick="return confirmDelete('Delete this department?');">
                                    <i class="bi bi-trash"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No departments found.</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
