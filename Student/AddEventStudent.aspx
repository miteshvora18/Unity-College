<%@ Page Title="" Language="C#" MasterPageFile="Student.master" AutoEventWireup="true" CodeFile="AddEventStudent.aspx.cs" Inherits="OtherPages_AddEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudentHead" Runat="Server">

    <link rel="stylesheet" href="../Includes/Stylesheets/jquery-ui-1.10.1.custom.css" />
    <script type="text/javascript" src="../Includes/JS/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Includes/JS/jquery-ui.js"></script>
    <script type="text/javascript" src="../Includes/JS/DateTimePicker.js"></script>
    
    <script type="text/javascript">

//        $(function() {
//            var getdate = $(".txtdatepicker").val();
//            $(".txtdatepicker").datepicker({
//                showOn: "both",
//                buttonImage: "../Images/calendar.gif",
//                buttonImageOnly: true,
//                changeMonth: true,
//                changeYear: true,
//                minDate: "+0D"
//            });
//        });


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

    $(function() {
        $(".txtdatepicker2").datetimepicker({
            showOn: "both",
            buttonImage: "../Includes/Images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            minDate: "+0D"
        });
    }); 
  
    </script>
<style type="text/css">
    .style1
    {
        height: 8px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentContent" Runat="Server">
    <div>
    <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Add Event"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     </div>
     <br />
     <div class="centrealign">  
     <div class="lbl"> 
            <table style="width:100%">
                <tr>
                    <td class="adminlbl">
                        <asp:Label ID="lblName" runat="server" Text="Event Name"></asp:Label>
                        <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtName" ErrorMessage="Event Name is required">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblShortDesc" runat="server" Text="Short Description"></asp:Label>
                        <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtShortDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtShortDesc" ErrorMessage="Short Description is required">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLongDesc" runat="server" Text="Long Description"></asp:Label>
                        <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLongDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtLongDesc" ErrorMessage="Long Description is required">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStartEvent" runat="server" Text="Start DateTime"></asp:Label>
                        <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtdatepicker" Width="150px" onkeypress="return false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="txtStartDate" ErrorMessage="Start DateTime is required">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblEndDate" runat="server" Text="End DateTime"></asp:Label>
                        <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtdatepicker2" Width="150px"
                            onkeypress="return false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="txtEndDate" ErrorMessage="End DateTime is required">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                        <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLocation" runat="server" 
                            ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtLocation" ErrorMessage="Location is required">&nbsp;</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSeats" runat="server" Text="Seats(if Limited)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSeats" runat="server" MaxLength="5"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ControlToValidate="txtSeats" 
                    ErrorMessage="Seats must only contain numbers" 
                    ValidationExpression="^[0-9]+$">Only numbers are allowed without blank space</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblValid" runat="server" Text="Valid"></asp:Label>
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
                        <asp:Button ID="btnSubmit" class="btnEventRegister" runat="server" Text="Submit" 
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

