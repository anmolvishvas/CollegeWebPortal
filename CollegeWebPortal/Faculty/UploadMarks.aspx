<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UploadMarks.aspx.cs" Inherits="CollegeWebPortal.FacultyArea.UploadMarks" Title="Upload Marks" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Upload Marks</h2>
    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block alert-auto-dismiss" Visible="false" />

    <div class="row g-3">
        <div class="col-lg-5">
            <div class="content-card p-3">
                <asp:ValidationSummary ID="vs" runat="server" CssClass="validation-summary mb-2"
                    ValidationGroup="mk" DisplayMode="BulletList" />

                <div class="mb-2">
                    <label class="form-label">Student</label>
                    <asp:DropDownList ID="ddlStudent" runat="server" />
                </div>
                <div class="mb-2">
                    <label class="form-label">Subject</label>
                    <asp:DropDownList ID="ddlSubject" runat="server" DataTextField="SubjectName"
                        DataValueField="SubjectID" />
                </div>
                <div class="mb-2">
                    <label class="form-label">Internal Marks (0-30)</label>
                    <asp:TextBox ID="txtInternal" runat="server" TextMode="Number" Text="0" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInternal"
                        ErrorMessage="Internal marks required." Text="*" CssClass="field-error"
                        ValidationGroup="mk" Display="Dynamic" />
                    <asp:RangeValidator runat="server" ControlToValidate="txtInternal"
                        MinimumValue="0" MaximumValue="30" Type="Integer"
                        ErrorMessage="Internal marks must be 0-30." Text="*" CssClass="field-error"
                        ValidationGroup="mk" Display="Dynamic" />
                </div>
                <div class="mb-3">
                    <label class="form-label">External Marks (0-70)</label>
                    <asp:TextBox ID="txtExternal" runat="server" TextMode="Number" Text="0" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtExternal"
                        ErrorMessage="External marks required." Text="*" CssClass="field-error"
                        ValidationGroup="mk" Display="Dynamic" />
                    <asp:RangeValidator runat="server" ControlToValidate="txtExternal"
                        MinimumValue="0" MaximumValue="70" Type="Integer"
                        ErrorMessage="External marks must be 0-70." Text="*" CssClass="field-error"
                        ValidationGroup="mk" Display="Dynamic" />
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save Marks" CssClass="btn btn-primary"
                    ValidationGroup="mk" OnClick="btnSave_Click" />
            </div>
        </div>

        <div class="col-lg-7">
            <div class="content-card p-3">
                <h5 class="mb-3">All Marks</h5>
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="8" OnPageIndexChanging="gv_PageIndexChanging"
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
                    <EmptyDataTemplate>No marks uploaded yet.</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
