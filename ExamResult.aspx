<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ExamResult.aspx.cs" Inherits="ExamSchedule" %>

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
        <asp:Label ID="lblExCourseHead" runat="server" BackColor="#CCCC00" 
            BorderColor="#CCCC00" BorderStyle="Solid" Font-Bold="True" Font-Italic="True" 
            Font-Names="Palatino Linotype" Font-Overline="False" Font-Size="X-Large" 
            Font-Strikeout="False" ForeColor="#CC0000" Text="Exam Result"></asp:Label>
    </div>
    
    <div class="centrealign">
    <div class="lbl">
                <br />
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
                <asp:DropDownList ID="ddlSem" runat="server" 
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
    </div>
</div>
</asp:Content>

