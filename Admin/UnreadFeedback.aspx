<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="UnreadFeedback.aspx.cs" Inherits="OtherPages_UnreadFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
<div>
      <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Unread "></asp:Label>
             <asp:Label ID="lblFeedback" class="header" runat="server" Text=""></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    <br />
    <div  style="padding-left:10%">
        <asp:GridView ID="gvFeedback" runat="server" BorderColor="#003300" Width="100%"
                BorderStyle="Solid" BorderWidth="5px" ForeColor="#003300">
                <HeaderStyle ForeColor="Maroon" />
                <AlternatingRowStyle ForeColor="#000066" />
            </asp:GridView>
    </div>
    <div style="text-align:center;font-size:large;color:Purple">
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <div style="text-align:center;">
        <asp:Button ID="btnBack" class="btnEventRegister" runat="server" Text="Back" 
            onclick="btnBack_Click" />
    </div>
</div>
</asp:Content>

