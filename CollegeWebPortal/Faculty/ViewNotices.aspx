<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewNotices.aspx.cs" Inherits="CollegeWebPortal.FacultyArea.ViewNotices" Title="Notices" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Notices</h2>

    <div class="content-card p-3 mb-3">
        <div class="d-flex gap-2">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                placeholder="Search notices..." />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                OnClick="btnSearch_Click" />
            <asp:Button ID="btnAll" runat="server" Text="All" CssClass="btn btn-outline-secondary"
                OnClick="btnAll_Click" />
        </div>
    </div>

    <div class="content-card p-3">
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="8" OnPageIndexChanging="gv_PageIndexChanging"
            CssClass="table table-striped table-hover table-bordered align-middle">
            <PagerStyle CssClass="gv-pager" />
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="PostedDate" HeaderText="Posted" DataFormatString="{0:dd MMM yyyy}" />
                <asp:TemplateField HeaderText="View">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" CssClass="btn btn-sm btn-outline-secondary"
                            NavigateUrl='<%# "~/NoticeDetail.aspx?id=" + Eval("NoticeID") %>'>Open</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No notices found.</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
