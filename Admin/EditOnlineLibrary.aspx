<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="EditOnlineLibrary.aspx.cs" Inherits="OtherPages_EditOnlineLibrary" %>

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
        <asp:Label ID="lblheader" class="header" runat="server" Text="Edit Data in Online Library"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    
<br />
        <div class="centrealign">
        <div class="lbl">
        <table class="style1">
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblCourse" runat="server" Text="Course"></asp:Label>
                    <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblSem" runat="server" Text="Select Subject"></asp:Label>
                    <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="True" onselectedindexchanged="ddlSubject_SelectedIndexChanged" 
                       >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblSem0" runat="server" Text="Semester"></asp:Label>
                    <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSem" runat="server" onfocus="this.blur()"
                        >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="File Header"></asp:Label>
                    <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlHeader" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlHeader_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUpload" runat="server" Text="Upload file"></asp:Label>
                    <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblValid" runat="server" Text="Valid"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlValid" runat="server">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" CssClass="btnEventRegister" 
                        Text="Submit" />
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </td>
            </tr>
            </table>
            </div>
    
    </div>
</div>
</asp:Content>

