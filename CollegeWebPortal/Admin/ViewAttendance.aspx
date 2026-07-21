<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewAttendance.aspx.cs" Inherits="CollegeWebPortal.Admin.ViewAttendance" Title="View Attendance" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Attendance Report</h2>

    <div class="content-card p-3 mb-3">
        <div class="row g-2 align-items-end">
            <div class="col-md-4">
                <label class="form-label">Student</label>
                <asp:DropDownList ID="ddlStudent" runat="server" />
            </div>
            <div class="col-md-3">
                <label class="form-label">Subject</label>
                <asp:DropDownList ID="ddlSubject" runat="server" />
            </div>
            <div class="col-md-3">
                <label class="form-label">Status</label>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Text="All" Value="" />
                    <asp:ListItem Text="Present" Value="Present" />
                    <asp:ListItem Text="Absent" Value="Absent" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn btn-primary w-100"
                    OnClick="btnFilter_Click" />
            </div>
        </div>
    </div>

    <div class="content-card p-3">
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="gv_PageIndexChanging"
            CssClass="table table-striped table-hover table-bordered align-middle">
            <PagerStyle CssClass="gv-pager" />
            <Columns>
                <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
                <asp:BoundField DataField="StudentName" HeaderText="Student" />
                <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class='badge <%# (string)Eval("Status") == "Present" ? "bg-success" : "bg-danger" %>'>
                            <%# Eval("Status") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No attendance records found.</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
