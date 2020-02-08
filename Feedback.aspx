<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="OtherPages_FeedbackF" %>

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
            Font-Strikeout="False" Text="FEEDBACK" ForeColor="#CC0000"></asp:Label>
        </div>
        <br />
        <br />
            
            <div class="centrealign">
            <div class="lbl">
            
                <table class="style1">
                    <tr>
                        <td class="adminlbl">
                <asp:Label ID="lblOption" runat="server" Text="Select Option"></asp:Label>
                            <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td>
                <asp:DropDownList ID="ddlOption" runat="server">
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                            <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtName" ErrorMessage="Name is required">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                            <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Email is required">&nbsp;</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Incorrect Email Format" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Incorrect Email Format</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label>
                            <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtSubject" ErrorMessage="Subject is required">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label ID="lblContent" runat="server" Text="Content"></asp:Label>
                            <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtContent" ErrorMessage="Content is required">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btnEventRegister" 
                        onclick="btnSubmit_Click" /><asp:Label ID="lblMsg"
                        runat="server" Text=""></asp:Label>
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

