<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="CollegeWebPortal.FacultyArea.Dashboard" Title="Faculty Dashboard" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Faculty Dashboard</h2>
    <p class="text-muted">Welcome, <strong><asp:Label ID="lblName" runat="server" /></strong>
        <asp:Label ID="lblDept" runat="server" CssClass="badge bg-secondary" /></p>

    <div class="row g-3 mb-4">
        <div class="col-md-4">
            <a href="ManageAttendance.aspx" class="text-decoration-none">
                <div class="card stat-card bg-grad-blue p-3">
                    <i class="bi bi-calendar-check stat-icon"></i>
                    <div class="stat-label mt-2">Mark Attendance</div>
                </div>
            </a>
        </div>
        <div class="col-md-4">
            <a href="UploadMarks.aspx" class="text-decoration-none">
                <div class="card stat-card bg-grad-green p-3">
                    <i class="bi bi-upload stat-icon"></i>
                    <div class="stat-label mt-2">Upload Marks</div>
                </div>
            </a>
        </div>
        <div class="col-md-4">
            <a href="SearchStudent.aspx" class="text-decoration-none">
                <div class="card stat-card bg-grad-purple p-3">
                    <i class="bi bi-search stat-icon"></i>
                    <div class="stat-label mt-2">Search Student</div>
                </div>
            </a>
        </div>
    </div>

    <div class="content-card p-3">
        <h5 class="mb-3"><i class="bi bi-megaphone me-2"></i>Latest Notices</h5>
        <asp:GridView ID="gvNotices" runat="server" AutoGenerateColumns="false"
            CssClass="table table-striped table-hover">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="PostedDate" HeaderText="Posted" DataFormatString="{0:dd MMM yyyy}" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink runat="server" CssClass="btn btn-sm btn-outline-secondary"
                            NavigateUrl='<%# "~/NoticeDetail.aspx?id=" + Eval("NoticeID") %>'>Open</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No notices.</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
