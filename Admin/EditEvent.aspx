<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="EditEvent.aspx.cs" Inherits="OtherPages_EditEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
    
    
   <%-- <link rel="stylesheet" href="../Includes/Stylesheets/jquery-ui-1.10.1.custom.css" />
    <script type="text/javascript" src="../Includes/JS/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Includes/JS/jquery-ui.js"></script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div>
     <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Edit Event"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    
<br />
        <div class="centrealign">
     
         <table style="width:100%"  class="lbl">
             <tr>
                 <td class="adminlbl">
                     <asp:Label ID="lblName" runat="server" Text="Select Event"></asp:Label>
                     <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
                     <asp:DropDownList ID="ddlName" runat="server" AutoPostBack="True" 
                         onselectedindexchanged="ddlName_SelectedIndexChanged">
                     </asp:DropDownList>
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
                     <asp:Label ID="lblShortDesc0" runat="server" Text="Long Description"></asp:Label>
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
                        <asp:Label ID="lblEndDate0" runat="server" Text="Start DateTime"></asp:Label>
                        <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtdatepicker" Width="150px" onkeypress="return false"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                         ControlToValidate="txtStartDate" ErrorMessage="Start DateTime is required">&nbsp;</asp:RequiredFieldValidator>
                 </td>
             </tr>
             <tr>
                 <td>
                        <asp:Label ID="lblEndDate" runat="server" Text="End DateTime"></asp:Label>
                        <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
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
</asp:Content>

