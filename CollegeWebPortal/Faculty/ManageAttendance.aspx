<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageAttendance.aspx.cs" Inherits="CollegeWebPortal.FacultyArea.ManageAttendance" Title="Manage Attendance" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Mark Attendance</h2>
    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block alert-auto-dismiss" Visible="false" />

    <div class="row g-3">
        <div class="col-lg-5">
            <div class="content-card p-3">
                <asp:ValidationSummary ID="vs" runat="server" CssClass="validation-summary mb-2"
                    ValidationGroup="att" DisplayMode="BulletList" />

                <div class="mb-2">
                    <label class="form-label">Subject</label>
                    <asp:DropDownList ID="ddlSubject" runat="server" DataTextField="SubjectName"
                        DataValueField="SubjectID" />
                </div>
                <div class="mb-2">
                    <label class="form-label">Student</label>
                    <asp:DropDownList ID="ddlStudent" runat="server" />
                </div>

                <div class="mb-2">
                    <label class="form-label d-block">Date</label>
                    <%-- Calendar server control --%>
                    <asp:Calendar ID="calDate" runat="server" CssClass="border rounded p-2"
                        SelectionMode="Day" />
                    <div class="small text-muted mt-1">Selected:
                        <asp:Label ID="lblSelectedDate" runat="server" /></div>
                </div>

                <div class="mb-3">
                    <label class="form-label d-block">Status</label>
                    <%-- RadioButtonList (RadioButton) control --%>
                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal"
                        CssClass="d-inline">
                        <asp:ListItem Text="Present" Value="Present" Selected="True" />
                        <asp:ListItem Text="Absent" Value="Absent" />
                    </asp:RadioButtonList>
                </div>

                <asp:Button ID="btnSave" runat="server" Text="Save Attendance" CssClass="btn btn-primary"
                    ValidationGroup="att" OnClick="btnSave_Click" />
            </div>
        </div>

        <div class="col-lg-7">
            <div class="content-card p-3">
                <h5 class="mb-3">Recent Attendance</h5>
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="8" OnPageIndexChanging="gv_PageIndexChanging"
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
                                    <%# Eval("Status") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No attendance records yet.</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
