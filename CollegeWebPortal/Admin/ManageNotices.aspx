<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageNotices.aspx.cs" Inherits="CollegeWebPortal.Admin.ManageNotices" Title="Manage Notices" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title mb-3">Manage Notices</h2>
    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block alert-auto-dismiss" Visible="false" />

    <div class="row g-3">
        <div class="col-lg-4">
            <div class="content-card p-3">
                <h5 class="mb-3">Post a Notice</h5>
                <asp:ValidationSummary ID="vs" runat="server" CssClass="validation-summary mb-2"
                    ValidationGroup="notice" DisplayMode="BulletList" />

                <div class="mb-2">
                    <label class="form-label">Title</label>
                    <asp:TextBox ID="txtTitle" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle"
                        ErrorMessage="Title is required." Text="*" CssClass="field-error"
                        ValidationGroup="notice" Display="Dynamic" />
                </div>
                <div class="mb-2">
                    <label class="form-label">Description</label>
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Rows="4" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDesc"
                        ErrorMessage="Description is required." Text="*" CssClass="field-error"
                        ValidationGroup="notice" Display="Dynamic" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Attachment (optional)</label>
                    <asp:FileUpload ID="fuAttachment" runat="server" CssClass="form-control" />
                    <small class="text-muted">Images or PDF up to 2 MB.</small>
                </div>
                <asp:Button ID="btnPost" runat="server" Text="Post Notice" CssClass="btn btn-primary"
                    ValidationGroup="notice" OnClick="btnPost_Click" />
            </div>
        </div>
        <div class="col-lg-8">
            <div class="content-card p-3">
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="6"
                    OnPageIndexChanging="gv_PageIndexChanging"
                    OnRowCommand="gv_RowCommand" DataKeyNames="NoticeID"
                    CssClass="table table-striped table-hover table-bordered align-middle">
                    <PagerStyle CssClass="gv-pager" />
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="PostedDate" HeaderText="Posted" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <%-- HyperLink control + QueryString demo --%>
                                <asp:HyperLink runat="server" CssClass="btn btn-sm btn-outline-secondary"
                                    NavigateUrl='<%# "~/NoticeDetail.aspx?id=" + Eval("NoticeID") %>'>
                                    <i class="bi bi-eye"></i>
                                </asp:HyperLink>
                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-danger"
                                    CommandName="DeleteRow" CommandArgument='<%# Eval("NoticeID") %>'
                                    OnClientClick="return confirmDelete('Delete this notice?');">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No notices yet.</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
