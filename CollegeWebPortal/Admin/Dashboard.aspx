<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="CollegeWebPortal.Admin.Dashboard" Title="Admin Dashboard" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="page-title mb-0">Admin Dashboard</h2>

        <%-- AJAX Timer + UpdatePanel: live clock that refreshes without a full page reload --%>
        <asp:UpdatePanel ID="upClock" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <span class="badge bg-primary p-2">
                    <i class="bi bi-clock"></i>
                    <asp:Label ID="lblClock" runat="server" />
                </span>
                <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <p class="text-muted">Welcome back, <strong><asp:Label ID="lblWelcome" runat="server" /></strong>!</p>

    <div class="row g-3 mb-4">
        <div class="col-md-4 col-lg-2">
            <div class="card stat-card bg-grad-blue p-3">
                <i class="bi bi-people stat-icon"></i>
                <div class="stat-value"><asp:Label ID="lblStudents" runat="server" /></div>
                <div class="stat-label">Students</div>
            </div>
        </div>
        <div class="col-md-4 col-lg-2">
            <div class="card stat-card bg-grad-green p-3">
                <i class="bi bi-person-badge stat-icon"></i>
                <div class="stat-value"><asp:Label ID="lblFaculty" runat="server" /></div>
                <div class="stat-label">Faculty</div>
            </div>
        </div>
        <div class="col-md-4 col-lg-2">
            <div class="card stat-card bg-grad-orange p-3">
                <i class="bi bi-building stat-icon"></i>
                <div class="stat-value"><asp:Label ID="lblDepartments" runat="server" /></div>
                <div class="stat-label">Departments</div>
            </div>
        </div>
        <div class="col-md-4 col-lg-2">
            <div class="card stat-card bg-grad-purple p-3">
                <i class="bi bi-journal-bookmark stat-icon"></i>
                <div class="stat-value"><asp:Label ID="lblCourses" runat="server" /></div>
                <div class="stat-label">Courses</div>
            </div>
        </div>
        <div class="col-md-4 col-lg-2">
            <div class="card stat-card bg-grad-teal p-3">
                <i class="bi bi-book stat-icon"></i>
                <div class="stat-value"><asp:Label ID="lblSubjects" runat="server" /></div>
                <div class="stat-label">Subjects</div>
            </div>
        </div>
        <div class="col-md-4 col-lg-2">
            <div class="card stat-card bg-grad-red p-3">
                <i class="bi bi-megaphone stat-icon"></i>
                <div class="stat-value"><asp:Label ID="lblNotices" runat="server" /></div>
                <div class="stat-label">Notices</div>
            </div>
        </div>
    </div>

    <div class="content-card p-3">
        <h5 class="mb-3"><i class="bi bi-megaphone me-2"></i>Latest Notices</h5>
        <asp:GridView ID="gvNotices" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="PostedDate" HeaderText="Posted" DataFormatString="{0:dd MMM yyyy}" />
            </Columns>
            <EmptyDataTemplate>No notices posted yet.</EmptyDataTemplate>
        </asp:GridView>
    </div>

</asp:Content>
