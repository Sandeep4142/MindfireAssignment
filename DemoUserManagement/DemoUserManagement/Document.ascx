<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Document.ascx.cs" Inherits="DemoUserManagement.Document" %>
<h3>Document</h3>


<select id="Document" class="form-control" style="width: 250px;">
    <option value="">Select File --</option>
    <option value="1">Aadhaar Card</option>
    <option value="2">Pan Card</option>
</select>

<input type="file" id="DocumentUpload" style="width: 250px;">
 <button id="addDocument" class="btn btn-primary ">Add Document</button>


<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <asp:GridView ID="documentGrid" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
            BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">

            <Columns>
                <asp:TemplateField HeaderText="DocumentId" SortExpression="DocumentId">
                    <ItemTemplate>
                        <asp:Label ID="Label0" runat="server" Text='<%# Bind("DocumentId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DocumentType" SortExpression="DocumentType">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetDocumentTypeName((int)Eval("DocumentType")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DocumentName" SortExpression="DocumentName">
                    <ItemTemplate>
                        <asp:HyperLink ID="DocumentLink" Target="_blank" runat="server" Text='<%# Eval("DocumentName") %>' NavigateUrl='<%# $"~/DocumentHandler.ashx?guidDocumentName={Eval("GuidDocumentName")}" %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TimeStamp" SortExpression="TimeStamp">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("TimeStamp") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>

    </ContentTemplate>

</asp:UpdatePanel>


<script src="Scripts/DocumentHandler.js"></script>
