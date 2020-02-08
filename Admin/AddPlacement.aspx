<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="AddPlacement.aspx.cs" Inherits="OtherPages_AddPlacement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    
   <%-- <link rel="stylesheet" href="../Includes/Stylesheets/jquery-ui-1.10.1.custom.css" />
    <script type="text/javascript" src="../Includes/JS/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Includes/Js/jquery-ui.js"></script>
    <script type="text/javascript" src="../Includes/JS/DateTimePicker.js"></script>--%>
    
    <script type="text/javascript">
        $(function() {
            $(".txtdatepicker").datetimepicker({
                showOn: "both",
                buttonImage: "../Includes/Images/calendar.gif",
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                minDate: "+0D"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div>
     <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Add Job/Internship"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    
<br />
        <div class="centrealign">
        <div class="lbl">
        <table class="style1">
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblType" runat="server" Text="Select Type"></asp:Label>
                    <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Selected="True">Job</asp:ListItem>
                        <asp:ListItem>Internship</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblCourse" runat="server" Text="Placement for Course"></asp:Label>
                    <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCourse" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                    <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtCompanyName" ErrorMessage="Company Name is required">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblJobProfile" runat="server" Text="Work Profile"></asp:Label>
                    <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWorkProfile" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtWorkProfile" ErrorMessage="Job Title is required">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLocation" runat="server" Text="Interview Location"></asp:Label>
                    <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtLocation" ErrorMessage="Interview Location is required">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDateTime" runat="server" Text="Placement Date and Time"></asp:Label>
                    <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox Width="140px" ID="txtDateTime" runat="server" CssClass="txtdatepicker" onkeypress="return false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtDateTime" ErrorMessage="DateTime is required">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEligibility" runat="server" Text="Eligibility"></asp:Label>
                    <asp:Label ID="Label20" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEligibility" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtEligibility" ErrorMessage="Eligibility is required">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblWebsite" runat="server" Text="Company Website"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWebsite" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblValid" runat="server" Text="Valid Placement"></asp:Label>
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

