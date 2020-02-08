<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="OtherPages_AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div>

    <div class="loginhead">
        Welcome to Admin Home Page...
    </div>
    <div>
        <asp:HyperLink ID="hyChangePass" NavigateUrl="../ChangePassword.aspx" 
            runat="server" Font-Size="Large" Font-Underline="True" ForeColor="#669999">Change Password</asp:HyperLink>
    </div>
    <br />
    <div>
        <asp:Label ID="lblMsg" runat="server" Font-Size="Large"></asp:Label>
    </div>
</div>
</asp:Content>

