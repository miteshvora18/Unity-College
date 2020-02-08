<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="OtherPages_ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" class="passchange">
<head runat="server">
    <title>Change Password</title>
      <link rel="Stylesheet" href="Includes/Stylesheets/MainStyle.css" />
</head>
<body>
    <form id="form1" runat="server" >
    <div >
            <div class="profile">
                <asp:Label ID="lblChangePass" class="header" runat="server" Text="Change Password"></asp:Label>
            </div>
            <br />
            <div style="padding-left:30%"  class="lbl">
                
                <table style="width:100%;height:100%">
                    <tr>
                        <td style="width:200px;text-align:left;">
                            <asp:Label ID="lblOldPass" runat="server" Text="Old Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtOldPass" ErrorMessage="Old Password is required">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNewPass" runat="server" Text="New Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtNewPass" ErrorMessage="New Password is required">&nbsp;</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblConfirmPass" runat="server" Text="Confirm New Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToCompare="txtNewPass" ControlToValidate="txtConfirmPass" 
                                ErrorMessage="Passwords do not match">&nbsp;</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btnChange" runat="server" Font-Size="Small" Text="Submit" 
                                onclick="btnChange_Click"  CssClass="btnEventRegister"/>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False" CssClass="btnEventRegister"
                                onclick="btnBack_Click" />
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
    </form>
</body>
</html>
