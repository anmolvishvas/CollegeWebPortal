<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageSubjects.aspx.cs" Inherits="CollegeWebPortal.Admin.ManageSubjects" Title="Manage Subjects" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Manage Subjects</h2>
    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block alert-auto-dismiss" Visible="false" />

    <div class="row g-3">
        <div class="col-lg-4">
            <div class="content-card p-3">
                <h5 class="mb-3"><asp:Label ID="lblFormTitle" runat="server" Text="Add Subject" /></h5>
                <div class="mb-2">
                    <label class="form-label">Subject Name</label>
                    <asp:TextBox ID="txtName" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
                        ErrorMessage="Name is required." Text="*" CssClass="field-error"
                        ValidationGroup="sub" Display="Dynamic" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Course</label>
                    <asp:DropDownList ID="ddlCourse" runat="server" DataTextField="CourseName"
                        DataValueField="CourseID" />
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                    ValidationGroup="sub" OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary"
                    CausesValidation="false" OnClick="btnClear_Click" />
            </div>
        </div>
        <div class="col-lg-8">
            <div class="content-card p-3">
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
                    OnRowCommand="gv_RowCommand" DataKeyNames="SubjectID"
                    CssClass="table table-striped table-hover table-bordered align-middle">
                    <Columns>
                        <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                        <asp:BoundField DataField="CourseName" HeaderText="Course" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-primary"
                                    CommandName="EditRow" CommandArgument='<%# Eval("SubjectID") %>'>
                                    <i class="bi bi-pencil"></i></asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="DeleteRow" CommandArgument='<%# Eval("SubjectID") %>'
                                    OnClientClick="return confirmDelete('Delete this subject?');">
                                    <i class="bi bi-trash"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No subjects found.</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
