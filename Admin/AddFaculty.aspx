<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="AddFaculty.aspx.cs" Inherits="OtherPages_AddFaculty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <%--<link rel="Stylesheet" href="../Includes/Stylesheets/MainStyle.css" />
    <link rel="stylesheet" href="../Includes/Stylesheets/jquery-ui-1.10.1.custom.css" />
  <script type="text/javascript" src="../Includes/JS/jquery-1.9.1.js"></script>
  <script type="text/javascript" src="../Includes/JS/jquery-ui.js"></script>--%>
  
  <script type="text/javascript">
  function getusername() {

        var username = $(".username").val();

        if (username != "") {
            $.ajax({
                type: "POST",
                url: "AddFaculty.aspx?a=1&user=" + username,
                success: function(a) {
                    $(".display").text(a);
                }
            });
        }
        else
            $(".display").text("");
    }

    function uniquemail() {

        var mail = $(".mail").val();

        if (mail != "") {
            $.ajax({
                type: "POST",
                url: "AddFaculty.aspx?b=1&mail=" + mail,
                success: function(a) {
                    $(".lblmail").text(a);
                }
            });
        }
        else
            $(".lblmail").text("");

    }
   </script>

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div>
    <div class="profile">
    <asp:Label ID="lblHeader" class="header" runat="server" Text="Add Faculty"></asp:Label>
    </div>
    
    <div class="centrealign">
    <div class="lbl">
            <table class="style1">
            <tr>
                <td class="adminlbl">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Full Name"></asp:Label>
                    <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtName" ErrorMessage="Name is required">&nbsp;</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="Name must only have alphabets" 
                    ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtName">Only alphabets are allowed</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDept" runat="server" Text="Select Department"></asp:Label>
                    <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDept" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                    <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                        <asp:TextBox ID="txtUsername" runat="server" 
                    CssClass="username"
    onblur="getusername();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtUsername" 
    ErrorMessage="Username is required">&nbsp;</asp:RequiredFieldValidator>
                        <asp:Label ID="lblUsernameValid" CssClass="display" runat="server" 
                            ForeColor="Red"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                    <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtPassword" ErrorMessage="Password is required">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRePassword" runat="server" Text="Re-enter Password"></asp:Label>
                    <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtRePassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtPassword" ControlToValidate="txtRePassword" 
                    ErrorMessage="Both Password should match">Passwords do not match</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                    <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtEmail" runat="server" onblur="uniquemail()" CssClass="mail"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Email is required">&nbsp;</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Email is invalid" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid email format</asp:RegularExpressionValidator>
                <asp:Label ID="lblUniqueMail" runat="server" ForeColor="Red" CssClass="lblmail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblContact" runat="server" Text="Mobile Number "></asp:Label>
                    <asp:Label ID="Label20" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtContact" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtContact" ErrorMessage="Phone Number is required">&nbsp;</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ControlToValidate="txtContact" 
                    ErrorMessage="Phone number must only contain numbers without blank space" 
                    ValidationExpression="^[0-9]+$">Only numbers are allowed without blank space</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInterest" runat="server" Text="Interest Area"></asp:Label>
                    </td>
                <td>
                    <asp:TextBox ID="txtInterest" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblValid" runat="server" Text="Valid User"></asp:Label>
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
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnEventRegister" 
                        onclick="btnSubmit_Click" />
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

