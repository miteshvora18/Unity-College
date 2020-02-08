<%@ Page Title="" Language="C#" MasterPageFile="Student.master" AutoEventWireup="true" CodeFile="CommunicateFaculty.aspx.cs" Inherits="OtherPages_CommunicateFaculty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudentHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentContent" Runat="Server">
    <div>
     <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Communicate with Faculty"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    <br />
    <div style="text-align:center" class="lbl">
        
        <asp:Label ID="lblSelect" runat="server" Text="Select Course"></asp:Label>
        <asp:DropDownList ID="ddlCourse" runat="server">
        </asp:DropDownList>
        
    &nbsp;<asp:Button ID="btnSubmit" class="btnEventRegister" Width="60px" 
            runat="server" Text="Submit" onclick="btnSubmit_Click" />
        <br />
        <asp:Label ID="lblMsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
        
    </div>
    <br />
    <div style="text-align:center;font-size:large">
        <asp:Label ID="lblFacultyHeader" runat="server" Text="Select Faculty to Communicate"></asp:Label>
    </div>
    <div style="padding-left:30%">
        
        
        
        <table class="style1">
            <tr>
                <td class="adminlbl">
                    &nbsp;</td>
                <td>
                <asp:Label ID="lblFaculty" runat="server" Font-Underline="False"></asp:Label>
                    &nbsp;</td>
            </tr>
        </table>
        
    </div>
</div>
</asp:Content>

