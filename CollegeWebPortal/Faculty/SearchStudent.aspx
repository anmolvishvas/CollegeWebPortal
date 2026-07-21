<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SearchStudent.aspx.cs" Inherits="CollegeWebPortal.FacultyArea.SearchStudent" Title="Search Student" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Search Student</h2>

    <%-- AJAX: results update without a full page refresh (UpdatePanel) --%>
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="content-card p-3 mb-3">
                <div class="d-flex gap-2">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                        placeholder="Search by roll no, name or email..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                        OnClick="btnSearch_Click" />
                </div>
            </div>

            <div class="row g-3">
                <div class="col-lg-7">
                    <div class="content-card p-3">
                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="StudentID" OnRowCommand="gv_RowCommand"
                            CssClass="table table-striped table-hover table-bordered align-middle">
                            <Columns>
                                <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
                                <asp:BoundField DataField="FullName" HeaderText="Name" />
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-primary"
                                            CommandName="ViewRow" CommandArgument='<%# Eval("StudentID") %>'>
                                            <i class="bi bi-eye"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No students found.</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>

                <div class="col-lg-5">
                    <div class="content-card p-3">
                        <h5 class="mb-3">Student Details</h5>
                        <%-- FormView shows the selected student --%>
                        <asp:FormView ID="fv" runat="server" RenderOuterTable="false">
                            <ItemTemplate>
                                <img src='<%# ResolveUrl("~/Images/avatar.svg") %>' width="90" class="mb-2" />
                                <table class="table table-sm">
                                    <tr><th>Roll No</th><td><%# Eval("RollNo") %></td></tr>
                                    <tr><th>Name</th><td><%# Eval("FullName") %></td></tr>
                                    <tr><th>Email</th><td><%# Eval("Email") %></td></tr>
                                    <tr><th>Phone</th><td><%# Eval("Phone") %></td></tr>
                                    <tr><th>Department</th><td><%# Eval("DepartmentName") %></td></tr>
                                    <tr><th>Semester</th><td><%# Eval("Semester") %></td></tr>
                                    <tr><th>Address</th><td><%# Eval("Address") %></td></tr>
                                </table>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <p class="text-muted mb-0">Select a student to view details.</p>
                            </EmptyDataTemplate>
                        </asp:FormView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
