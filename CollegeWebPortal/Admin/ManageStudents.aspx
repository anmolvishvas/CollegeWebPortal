<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageStudents.aspx.cs" Inherits="CollegeWebPortal.Admin.ManageStudents" Title="Manage Students" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="page-title mb-3">Manage Students</h2>

    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block alert-auto-dismiss" Visible="false" />

    <div class="row g-3">
        <!-- ===================== ADD / EDIT FORM ===================== -->
        <div class="col-lg-4">
            <div class="content-card p-3">
                <h5 class="mb-3"><asp:Label ID="lblFormTitle" runat="server" Text="Add Student" /></h5>

                <asp:ValidationSummary ID="vs" runat="server" CssClass="validation-summary mb-2"
                    ValidationGroup="student" DisplayMode="BulletList" />

                <div class="mb-2">
                    <label class="form-label">Roll No</label>
                    <asp:TextBox ID="txtRollNo" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRollNo"
                        ErrorMessage="Roll No is required." Text="*" CssClass="field-error"
                        ValidationGroup="student" Display="Dynamic" />
                </div>

                <div class="row">
                    <div class="col-6 mb-2">
                        <label class="form-label">First Name</label>
                        <asp:TextBox ID="txtFirst" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirst"
                            ErrorMessage="First name is required." Text="*" CssClass="field-error"
                            ValidationGroup="student" Display="Dynamic" />
                    </div>
                    <div class="col-6 mb-2">
                        <label class="form-label">Last Name</label>
                        <asp:TextBox ID="txtLast" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLast"
                            ErrorMessage="Last name is required." Text="*" CssClass="field-error"
                            ValidationGroup="student" Display="Dynamic" />
                    </div>
                </div>

                <div class="mb-2">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
                        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                        ErrorMessage="Enter a valid email address." Text="*" CssClass="field-error"
                        ValidationGroup="student" Display="Dynamic" />
                </div>

                <div class="mb-2">
                    <label class="form-label">Phone</label>
                    <asp:TextBox ID="txtPhone" runat="server" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPhone"
                        ValidationExpression="^[0-9]{10}$"
                        ErrorMessage="Phone must be 10 digits." Text="*" CssClass="field-error"
                        ValidationGroup="student" Display="Dynamic" />
                </div>

                <div class="mb-2">
                    <label class="form-label">Department</label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" DataTextField="DepartmentName"
                        DataValueField="DepartmentID" />
                </div>

                <div class="mb-2">
                    <label class="form-label">Semester (1-8)</label>
                    <asp:TextBox ID="txtSemester" runat="server" TextMode="Number" Text="1" />
                    <asp:RangeValidator runat="server" ControlToValidate="txtSemester"
                        MinimumValue="1" MaximumValue="8" Type="Integer"
                        ErrorMessage="Semester must be between 1 and 8." Text="*" CssClass="field-error"
                        ValidationGroup="student" Display="Dynamic" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="2" />
                </div>

                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                    ValidationGroup="student" OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary"
                    CausesValidation="false" OnClick="btnClear_Click" />
            </div>
        </div>

        <!-- ===================== LIST / SEARCH ===================== -->
        <div class="col-lg-8">
            <div class="content-card p-3">
                <div class="d-flex gap-2 mb-3">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                        placeholder="Search by roll no, name or email..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                        CausesValidation="false" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnShowAll" runat="server" Text="All" CssClass="btn btn-outline-secondary"
                        CausesValidation="false" OnClick="btnShowAll_Click" />
                </div>

                <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5" AllowSorting="true"
                    OnPageIndexChanging="gvStudents_PageIndexChanging"
                    OnSorting="gvStudents_Sorting"
                    OnRowCommand="gvStudents_RowCommand"
                    DataKeyNames="StudentID"
                    CssClass="table table-striped table-hover table-bordered align-middle">
                    <PagerStyle CssClass="gv-pager" />
                    <Columns>
                        <asp:BoundField DataField="RollNo" HeaderText="Roll No" SortExpression="RollNo" />
                        <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                        <asp:BoundField DataField="Semester" HeaderText="Sem" SortExpression="Semester" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-primary"
                                    CommandName="EditRow" CommandArgument='<%# Eval("StudentID") %>'>
                                    <i class="bi bi-pencil"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="DeleteRow" CommandArgument='<%# Eval("StudentID") %>'
                                    OnClientClick="return confirmDelete('Delete this student and related records?');">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No students found.</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
