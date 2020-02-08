<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
    <div>
    <asp:Label ID="lblNews" runat="server"></asp:Label>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
</div>
        
</asp:Content>

