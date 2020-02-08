<%@ Page Title="" Language="C#" MasterPageFile="Faculty.master" AutoEventWireup="true" CodeFile="CommunicateStudent.aspx.cs" Inherits="OtherPages_CommunicateFaculty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FacultyHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FacultyContent" Runat="Server">
    <div>
    <div class="profile">
        <asp:Label ID="lblHeader" class="header" runat="server" Text="Communicate with Student"></asp:Label>
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
        &nbsp;<asp:Label ID="lblStudentHeader" runat="server" 
            Text="Select Student to Communicate"></asp:Label>
    </div>
        <div style="padding-left:30%">
        
        <table class="style1">
            <tr>
                <td class="adminlbl">
                    &nbsp;</td>
                <td>
                <asp:Label ID="lblStudent" runat="server" Font-Underline="False"></asp:Label>
                    &nbsp;</td>
            </tr>
        </table>
        
    </div>
</div>
</asp:Content>

