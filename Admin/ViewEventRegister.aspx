<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="ViewEventRegister.aspx.cs" Inherits="OtherPages_ViewEventRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div>
       <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="View Event Registrations"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <br />
    <div style="padding-left:30%;" class="lbl">
        <table style="width:100%">
            <tr>
                <td class="dirlbl">
                    <asp:Label ID="lblEventList" runat="server" Text="Select Event"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEventList" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                   
                    <asp:Button ID="btnSubmit" Width="70px" class="btnEventRegister" runat="server" 
                        Text="Submit" onclick="btnSubmit_Click" />
                   
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblMsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div>
       <asp:GridView ID="gvEventDirectory" runat="server" 
            CellSpacing="10" 
             Width="100%" BorderColor="#003300" BorderStyle="Solid" BorderWidth="5px" 
            Font-Size="Medium" ForeColor="#003300">
        
            <HeaderStyle Font-Size="Medium" ForeColor="Maroon" />
            <AlternatingRowStyle ForeColor="#000066" />
        </asp:GridView>
    </div>
</div>
</asp:Content>

