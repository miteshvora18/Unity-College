﻿<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="EditStudent.aspx.cs" Inherits="AdminPages_EditStudentA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
    <%--<link rel="Stylesheet" href="../Includes/Stylesheets/MainStyle.css" />
    <link rel="stylesheet" href="../Includes/Stylesheets/jquery-ui-1.10.1.custom.css" />
  <script type="text/javascript" src="../Includes/JS/jquery-1.9.1.js"></script>
  <script type="text/javascript" src="../Includes/JS/jquery-ui.js"></script>--%>
    
    <script type="text/javascript">
        function func() {
            var el = document.getElementById('ddlUsername');
            this.setAttribute('readonly');
        }

        $(function() {
           
            $(".txtdatepicker").datepicker({
                showOn: "both",
                buttonImage: "../Includes/Images/calendar.gif",
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true
            });
        });

    </script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
      
<div>
            <div class="profile">
            
        <asp:Label ID="lblHeader" class="header" runat="server" Text="Edit Student Details" 
            ></asp:Label>
            
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            
            </div >
    
    <div class="centrealign">
    <table style="width:100%;" class="lbl">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                 &nbsp;</td>
        </tr>
        <tr>
            <td class="adminlbl">
                <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td>
                
                <asp:TextBox ID="txtUsername" ReadOnly="true" runat="server"></asp:TextBox>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtUsername" ErrorMessage="Username is required">&nbsp;</asp:RequiredFieldValidator>
                
            </td>
        </tr>
        <tr>
            <td class="adminlbl">
                <asp:Label ID="lblName" runat="server" Text="Full Name"></asp:Label>
                <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtName" ErrorMessage="Name is required">&nbsp;</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="Name must only have alphabets" 
                    ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtName">Only alphabets are allowed</asp:RegularExpressionValidator>
            </td>
        </tr >
        <tr>
            <td class="adminlbl">
                <asp:Label ID="lblCourse" runat="server" Text="Course"></asp:Label>
                <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddlCourse_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="adminlbl">
                     <asp:Label ID="lblCurrentSem" runat="server" Text="Current Semester"></asp:Label>
                     <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
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
                <asp:Label ID="lblEmail" runat="server" Text="Email "></asp:Label>
                <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" onblur="uniquemail()" CssClass="mail"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Email is required">&nbsp;</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Email is invalid" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid email format</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="adminlbl">
                <asp:Label ID="lblContact" runat="server" Text="Mobile Number"></asp:Label>
                <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContact" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
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
                <asp:Label ID="Label20" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtStartdate" CssClass="txtdatepicker"  runat="server" onkeypress="return false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="adminlbl">
                <asp:Label ID="lblEndDate" runat="server" Text="Course End Date"></asp:Label>
                <asp:Label ID="Label21" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEnddate" CssClass="txtdatepicker"  runat="server" onkeypress="return false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="adminlbl">
                <asp:Label ID="lblGs" runat="server" Text="General Secretary"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlGs" runat="server">
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="adminlbl">
                <asp:Label ID="lblValid" runat="server" Text="Valid User"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlValidUser" runat="server">
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
      
        <tr>
            <td class="adminlbl">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnEventRegister"
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
        
    
</asp:Content>

