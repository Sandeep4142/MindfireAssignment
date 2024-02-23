<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Notes.ascx.cs" Inherits="DemoUserManagement.Notes" %>

<div>
    <h4>NOTES</h4>

 <input type="text" id="note" style="margin-left: 0px; width: 70%;" />
<button runat="server" ClientIDMode="Static" type="button" id="addNote" class="btn btn-primary" style="width: 25%; margin-left: 0px;">Add</button>
</div>

<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div>
            <asp:GridView ID="notesGrid" CssClass="mt-2" runat="server" CellPadding="3" Width="100%" BackColor="White" BorderColor="black" BorderStyle="Solid" BorderWidth="1px">
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
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script src="Scripts/NotesHandler.js"></script>

