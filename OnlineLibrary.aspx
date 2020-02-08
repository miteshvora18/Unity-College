<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OnlineLibrary.aspx.cs" Inherits="OnlineLibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
    <div>
    <div style="text-align:center">
            <asp:Label ID="lblRegCourseHead" runat="server" BackColor="#CCCC00" 
                BorderColor="#CCCC00" BorderStyle="Solid" Font-Bold="True" Font-Italic="True" 
                Font-Names="Palatino Linotype" Font-Overline="False" Font-Size="X-Large" 
                Font-Strikeout="False" Text="ONLINE LIBRARY" ForeColor="#CC0000"></asp:Label>
            </div>
            <br />
            <br />
            
            <div style="padding-left:30%;" class="lbl">
                <table class="style1">
                    <tr>
                        <td class="adminlbl">
                <asp:Label ID="lblCourse" runat="server" Text="Select Course"></asp:Label>
                        <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td>
                <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlCourse_SelectedIndexChanged">
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label ID="lblSem" runat="server" Text="Select Semester"></asp:Label>
                        <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td>
                <asp:DropDownList ID="ddlSem" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlSem_SelectedIndexChanged">
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label ID="lblSubject" runat="server" Text="Select Subject"></asp:Label>
                        <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td>
                <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="True" 
                   >
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btnEventRegister" 
                    onclick="btnSubmit_Click" />
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div style="padding-left:40%;font-size:x-large">
                <asp:Label ID="lblHeader" runat="server" Text="Select Link to view files.."></asp:Label>
            </div>
            
            <div style="padding-left:40%;color:white;font-size:large">
                <asp:Label class="ol" ID="lblData" runat="server" Text=""></asp:Label>
            </div>
</div>
</asp:Content>

