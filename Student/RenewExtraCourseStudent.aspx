<%@ Page Title="" Language="C#" MasterPageFile="Student.master" AutoEventWireup="true" CodeFile="RenewExtraCourseStudent.aspx.cs" Inherits="OtherPages_RenewExtraCourseS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudentHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    
  <link rel="Stylesheet" href="../Includes/Stylesheets/MainStyle.css" />
  <link rel="stylesheet" href="../Includes/Stylesheets/jquery-ui-1.10.1.custom.css" />
  <script type="text/javascript" src="../Includes/JS/jquery-1.9.1.js"></script>
  <script type="text/javascript" src="../Includes/JS/jquery-ui.js"></script>
  
  <script type="text/javascript">
    $(function() {
        var getdate = $(".txtdatepicker").val();
        $(".txtdatepicker").datepicker({
            showOn: "both",
            buttonImage: "../Includes/Images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            minDate: "+0D", 
            maxDate: "+1Y"
        });
    });
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentContent" Runat="Server">
    <div>
     <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Renew Extra Course"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    
<br />
        <div class="centrealign">
        <div class="lbl">
        <table class="style1">
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblECourse" runat="server" Text="Select Course"></asp:Label>
                    <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlExtraCourse" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlECourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>
                    (in Months)</td>
                <td>
                    <asp:TextBox ID="txtDuration" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label>
                    <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" CssClass="txtdatepicker"  runat="server" onkeypress="return false"
                        ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtStartDate" ErrorMessage="Start Date is required">&nbsp;</asp:RequiredFieldValidator>
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

