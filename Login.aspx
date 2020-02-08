<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	
	<script type = "text/javascript" >
//	function preventBack(){window.history.forward();}
//	setTimeout("preventBack()", 0);
	    //	window.onunload=function(){null};

	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
    <div style="text-align:center">
       <asp:Label ID="lblExCourseHead" runat="server" BackColor="#CCCC00" 
            BorderColor="#CCCC00" BorderStyle="Solid" Font-Bold="True" Font-Italic="True" 
            Font-Names="Palatino Linotype" Font-Overline="False" Font-Size="X-Large" 
            Font-Strikeout="False" ForeColor="#CC0000" Text="Login Page"></asp:Label>
</div>
<div style="text-align:center;font-size:large;color:Red">
    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
</div>
<div>
    <br />
    <table class="login" style="margin: 5px auto;">
        <tr style="color: rgb(81, 83, 72); font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.453125px; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px;">
            <td>
                <p
                    style="margin-top: 0px !important; margin-bottom: 0.25em !important;">
                    <strong>
                    <label for="email" class="text" style="color: #800000">
                    Username<span style="font-size:large; color: #FF0000;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtUsername" ErrorMessage="Username is required" 
                        Font-Size="Medium">&nbsp;</asp:RequiredFieldValidator>
                    </label></strong></p>
                <asp:TextBox ID="txtUsername" runat="server" style="padding: 3px; width: 240px; color: rgb(81, 83, 72);"></asp:TextBox>
            </td>
        </tr>
        <tr style="color: rgb(81, 83, 72); font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.453125px; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px;">
            <td>
                <p
                    style="margin-top: 0.5em !important; margin-bottom: 0.25em !important;">
                    <strong>
                    <label for="password" class="text" style="color: #800000">
                    Password<span style="font-size:large; color: #FF0000;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtPassword" ErrorMessage="Password is required" 
                        Font-Size="Medium">&nbsp;</asp:RequiredFieldValidator>
                    </label>
                    </strong></p>
                <asp:TextBox ID="txtPassword" runat="server" 
                    style="padding: 3px; width: 240px; color: rgb(81, 83, 72);" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr style="color: rgb(81, 83, 72); font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.453125px; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px;">
            <td style="text-align:center">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btnEventRegister" 
                    onclick="btnSubmit_Click" Font-Underline="False" />
                <br />
            <asp:Label ID="lblMsg" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
            
            </td>
        </tr>
        <tr style="color: rgb(81, 83, 72); font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.453125px; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px;">
            <td class="ol">
                <asp:HyperLink ID="hyForgotPass" runat="server" Font-Size="Large" 
                    NavigateUrl="ForgotPassword.aspx"  
                    >Forgot Password?</asp:HyperLink>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                
            </td>
        </tr>
        
    </table>

</div>
</asp:Content>

