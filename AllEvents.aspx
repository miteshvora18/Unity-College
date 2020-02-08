<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AllEvents.aspx.cs" Inherits="AllEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">


    <asp:Label ID="lblSelectedDayEvent" runat="server"></asp:Label>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
<div>
    
        <asp:GridView ID="gvEvents" runat="server" CellSpacing="10" 
             Width="100%" BorderColor="#003300" BorderStyle="Solid" BorderWidth="5px" 
            Font-Size="Medium" ForeColor="#003300" BackColor="White"
            >
            <HeaderStyle Font-Size="Medium" ForeColor="Maroon" BackColor="White" />
            <AlternatingRowStyle ForeColor="#000066" />
        </asp:GridView>
                
</div>
        <asp:Button ID="btnRegister" runat="server" Text="Register for event" class="btnEventRegister" Width="200px"
         onclick="btnRegister_Click" />

</asp:Content>

