<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="OtherPages_Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div>
     <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Feedback List"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    
<br />

        <div style="text-align:center">
            <asp:DropDownList ID="ddlFeedback" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnEventRegister" 
                onclick="btnSubmit_Click" />
            <br />
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <br />
        <div style="padding-left:10%">
            
            <asp:GridView ID="gvFeedback" runat="server" BorderColor="#003300" 
                BorderStyle="Solid" BorderWidth="5px" ForeColor="#003300">
                <HeaderStyle ForeColor="Maroon" />
                <AlternatingRowStyle ForeColor="#000066" />
            </asp:GridView>
            
        </div>
</div>
</asp:Content>

