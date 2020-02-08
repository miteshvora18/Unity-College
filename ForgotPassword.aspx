<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
<div>
    <div style="text-align:center">
           <asp:Label ID="lblExCourseHead" runat="server" BackColor="#CCCC00" 
                BorderColor="#CCCC00" BorderStyle="Solid" Font-Bold="True" Font-Italic="True" 
                Font-Names="Palatino Linotype" Font-Overline="False" Font-Size="X-Large" 
                Font-Strikeout="False" ForeColor="#CC0000" Text="Forgot Password"></asp:Label>
    </div>
    <br />
    <div>

        <table class="login" style="margin: 5px auto;">
            <tr style="color: rgb(81, 83, 72); font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.453125px; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px;">
                <td>
                    <p
                        style="margin-top: 0px !important; margin-bottom: 0.25em !important;">
                        <strong>
                        <label for="email" class="text" style="color: #800000">
                        Email</strong><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ErrorMessage="Invalid Email Format" ControlToValidate="txtEmail" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;</asp:RegularExpressionValidator>
                        </label></p>
                    <asp:TextBox ID="txtEmail" runat="server" 
                        style="padding: 3px; width: 240px; color: rgb(81, 83, 72);"></asp:TextBox>
                </td>
            </tr>
            <tr style="color: rgb(81, 83, 72); font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.453125px; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px;">
                <td style="text-align:center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btnEventRegister"
                        onclick="btnSubmit_Click" />
                
                <asp:Label ID="lblMsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                
                </td>
            </tr>
            <tr>
            <td>
            
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            
            </td>
            </tr>
        </table>

    </div>

</div>
</asp:Content>

