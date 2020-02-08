<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="AdminPages_AddStudent" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" runat="server">
    <%--<link rel="Stylesheet" href="../Includes/Stylesheets/MainStyle.css" />
    <link rel="stylesheet" href="../Includes/Stylesheets/jquery-ui-1.10.1.custom.css" />
  <script type="text/javascript" src="../Includes/JS/jquery-1.9.1.js"></script>
  <script type="text/javascript" src="../Includes/JS/jquery-ui.js"></script>--%>
<script type="text/javascript">
    $(function() {
        var getdate = $(".txtdatepicker").val();
        $(".txtdatepicker").datepicker({
            showOn: "both",
            buttonImage: "../Includes/Images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            maxDate: "+0D"
        });
    });
    
    //Function to getusername and display message via ajax
    function getusername() {

        var username = $(".username").val();

        if (username != "") {
            $.ajax({
                type: "POST",
                url: "AddStudent.aspx?a=1&user=" + username,
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
                url: "AddStudent.aspx?b=1&mail=" + mail,
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
    <div style="height:20px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>

    <div>
        <div class="profile">
            <asp:Label ID="lblHeader" runat="server" class="header" Text="Register Student"></asp:Label>
        </div>
         
         <div  class="centrealign">
         <div class="lbl">
        <table style="width:100%;">
            <tr>
                <td>
                </td>
                <td>
                
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="adminlbl">
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
                <td class="adminlbl">
                    <asp:Label ID="lblCourse" runat="server" Text="Select Course"></asp:Label>
                    <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                      <asp:DropDownList ID="ddlCourse" runat="server" 
                        onselectedindexchanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    
                    
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                    <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtUsername" runat="server" 
                        CssClass="username"
        onblur="getusername();"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtUsername" 
        ErrorMessage="Username is required">&nbsp;</asp:RequiredFieldValidator>
                            <asp:Label ID="lblUsernameValid" CssClass="display" runat="server" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
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
                <td class="adminlbl">
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
                <td class="adminlbl">
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
                <td class="adminlbl">
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
                <td class="adminlbl">
                    <asp:Label ID="lblStartDate" runat="server" Text="Admission Date"></asp:Label>
                    <asp:Label ID="Label21" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" CssClass="txtdatepicker"  runat="server" onkeypress="return false"
                            ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtStartDate" ErrorMessage="Admission date is required">&nbsp;</asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblCurrentSem" runat="server" Text="Current Semester"></asp:Label>
                    <asp:Label ID="Label22" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCurrentSem" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblGs" runat="server" Text="General Secretary"></asp:Label>
                    </td>
                <td>
                
                    <asp:DropDownList ID="ddlGs" runat="server">
                        <asp:ListItem Selected="True">No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:DropDownList>
                
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    &nbsp;</td>
                <td>
                
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btnEventRegister"
                onclick="btnSubmit_Click" />
                
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    &nbsp;</td>
                <td>
                
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        
                </td>
            </tr>
        </table>
        </div>
        </div>
    </div>

</div>
</asp:Content>

