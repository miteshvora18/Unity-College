<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="FacultyDirectory.aspx.cs" Inherits="OtherPages_FacultyDirectory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div>
     <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Faculty Directory"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    <br />
    <div style="padding-left:40%" class="lbl">
        
        <table class="style1">
            <tr>
                <td class="dirlbl">
                    <asp:Label ID="lblCourse" runat="server" Text="Select Course"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCourse" runat="server" 
                        >
                    </asp:DropDownList>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button Width="60px" class="btnEventRegister" ID="btnSubmit" runat="server" 
                        Text="Submit" onclick="btnSubmit_Click" />&nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        
    </div>
    <br />
    <div>
        <asp:GridView ID="gvFacultyDirectory" runat="server" 
            onrowcommand="gvFacultyDirectory_RowCommand" CellSpacing="10" 
             Width="100%" BorderColor="#003300" BorderStyle="Solid" BorderWidth="5px" ForeColor="#003300"
            >
        <Columns>
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:Button ID="btnEdit" Width="70px" class="btnEventRegister" runat="server" Text="Edit" CommandName="Seen" CommandArgument='<%#Eval("username") %>'/>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
            <HeaderStyle ForeColor="Maroon" />
            <AlternatingRowStyle ForeColor="#000066" />
        </asp:GridView>
    </div>
</div>
</asp:Content>

