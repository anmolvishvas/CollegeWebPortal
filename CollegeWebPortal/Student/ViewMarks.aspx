<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewMarks.aspx.cs" Inherits="CollegeWebPortal.StudentArea.ViewMarks" Title="My Marks" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">My Marks</h2>

    <div class="content-card p-3">
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
            CssClass="table table-striped table-hover table-bordered align-middle">
            <Columns>
                <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                <asp:BoundField DataField="InternalMarks" HeaderText="Internal (30)" />
                <asp:BoundField DataField="ExternalMarks" HeaderText="External (70)" />
                <asp:BoundField DataField="Total" HeaderText="Total (100)" />
            </Columns>
            <EmptyDataTemplate>No marks published yet.</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
