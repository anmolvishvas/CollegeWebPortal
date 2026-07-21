<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageCourses.aspx.cs" Inherits="CollegeWebPortal.Admin.ManageCourses" Title="Manage Courses" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Manage Courses</h2>
    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block alert-auto-dismiss" Visible="false" />

    <div class="row g-3">
        <div class="col-lg-4">
            <div class="content-card p-3">
                <h5 class="mb-3"><asp:Label ID="lblFormTitle" runat="server" Text="Add Course" /></h5>
                <div class="mb-2">
                    <label class="form-label">Course Name</label>
                    <asp:TextBox ID="txtName" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
                        ErrorMessage="Name is required." Text="*" CssClass="field-error"
                        ValidationGroup="course" Display="Dynamic" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Department</label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" DataTextField="DepartmentName"
                        DataValueField="DepartmentID" />
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                    ValidationGroup="course" OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary"
                    CausesValidation="false" OnClick="btnClear_Click" />
            </div>
        </div>
        <div class="col-lg-8">
            <div class="content-card p-3">
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
                    OnRowCommand="gv_RowCommand" DataKeyNames="CourseID"
                    CssClass="table table-striped table-hover table-bordered align-middle">
                    <Columns>
                        <asp:BoundField DataField="CourseName" HeaderText="Course" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-primary"
                                    CommandName="EditRow" CommandArgument='<%# Eval("CourseID") %>'>
                                    <i class="bi bi-pencil"></i></asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="DeleteRow" CommandArgument='<%# Eval("CourseID") %>'
                                    OnClientClick="return confirmDelete('Delete this course?');">
                                    <i class="bi bi-trash"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No courses found.</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
