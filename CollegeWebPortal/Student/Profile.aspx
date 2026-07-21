<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Profile.aspx.cs" Inherits="CollegeWebPortal.StudentArea.ProfilePage" Title="My Profile" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">My Profile</h2>

    <div class="row">
        <div class="col-lg-3 text-center mb-3">
            <div class="content-card p-4">
                <img src="~/Images/avatar.svg" runat="server" width="120" class="mb-2" />
                <h5 class="mb-0"><asp:Label ID="lblName" runat="server" /></h5>
                <span class="text-muted"><asp:Label ID="lblRoll" runat="server" /></span>
            </div>
        </div>
        <div class="col-lg-9">
            <div class="content-card p-3">
                <%-- DetailsView server control showing the student's record --%>
                <asp:DetailsView ID="dv" runat="server" AutoGenerateRows="false"
                    CssClass="table table-bordered dv-responsive" GridLines="Horizontal">
                    <Fields>
                        <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
                        <asp:BoundField DataField="FullName" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                        <asp:BoundField DataField="Semester" HeaderText="Semester" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                    </Fields>
                </asp:DetailsView>
            </div>
        </div>
    </div>
</asp:Content>
