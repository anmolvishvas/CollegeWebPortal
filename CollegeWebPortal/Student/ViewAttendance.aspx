<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewAttendance.aspx.cs" Inherits="CollegeWebPortal.StudentArea.ViewAttendance" Title="My Attendance" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="page-title mb-0">My Attendance</h2>
        <span class="badge bg-primary p-2 fs-6">Overall: <asp:Label ID="lblPct" runat="server" />%</span>
    </div>

    <div class="content-card p-3">
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="gv_PageIndexChanging"
            CssClass="table table-striped table-hover table-bordered align-middle">
            <PagerStyle CssClass="gv-pager" />
            <Columns>
                <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class='badge <%# (string)Eval("Status") == "Present" ? "bg-success" : "bg-danger" %>'>
                            <%# Eval("Status") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No attendance records yet.</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
