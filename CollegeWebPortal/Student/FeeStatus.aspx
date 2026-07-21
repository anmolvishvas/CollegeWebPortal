<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FeeStatus.aspx.cs" Inherits="CollegeWebPortal.StudentArea.FeeStatus" Title="Fee Status" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Fee Status</h2>

    <div class="content-card p-3">
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
            CssClass="table table-striped table-hover table-bordered align-middle">
            <Columns>
                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C0}" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class='badge <%# (string)Eval("Status") == "Paid" ? "bg-success" : "bg-warning text-dark" %>'>
                            <%# Eval("Status") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date"
                    DataFormatString="{0:dd MMM yyyy}" NullDisplayText="-" />
            </Columns>
            <EmptyDataTemplate>No fee records found.</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
