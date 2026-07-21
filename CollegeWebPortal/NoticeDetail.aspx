<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="NoticeDetail.aspx.cs" Inherits="CollegeWebPortal.NoticeDetail" Title="Notice" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Notice</h2>

    <div class="content-card p-4">
        <%-- FormView bound to a single notice selected via QueryString (?id=) --%>
        <asp:FormView ID="fvNotice" runat="server" RenderOuterTable="false">
            <ItemTemplate>
                <h3><%# Eval("Title") %></h3>
                <p class="text-muted"><i class="bi bi-calendar"></i>
                    Posted on <%# Eval("PostedDate", "{0:dd MMMM yyyy, hh:mm tt}") %></p>
                <hr />
                <p style="white-space:pre-wrap;"><%# Eval("Description") %></p>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div class="alert alert-warning mb-0">Notice not found.</div>
            </EmptyDataTemplate>
        </asp:FormView>

        <a href="javascript:history.back()" class="btn btn-outline-secondary mt-3">
            <i class="bi bi-arrow-left"></i> Back
        </a>
    </div>
</asp:Content>
