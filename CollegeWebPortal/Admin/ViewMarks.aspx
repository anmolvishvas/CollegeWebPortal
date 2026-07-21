<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewMarks.aspx.cs" Inherits="CollegeWebPortal.Admin.ViewMarks" Title="View Marks" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Marks Report</h2>

    <div class="content-card p-3">
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="gv_PageIndexChanging"
            CssClass="table table-striped table-hover table-bordered align-middle">
            <PagerStyle CssClass="gv-pager" />
            <Columns>
                <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
                <asp:BoundField DataField="StudentName" HeaderText="Student" />
                <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                <asp:BoundField DataField="InternalMarks" HeaderText="Internal" />
                <asp:BoundField DataField="ExternalMarks" HeaderText="External" />
                <asp:BoundField DataField="Total" HeaderText="Total" />
            </Columns>
            <EmptyDataTemplate>No marks found.</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
