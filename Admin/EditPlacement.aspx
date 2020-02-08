<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="EditPlacement.aspx.cs" Inherits="OtherPages_EditPlacement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    
    <%--<link rel="stylesheet" href="../Includes/Stylesheets/jquery-ui-1.10.1.custom.css" />
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

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function(evt, args) {
                $(".txtdatepicker").datetimepicker({
                showOn: "both",
                buttonImage: "../Includes/Images/calendar.gif",
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                minDate: "+0D"
            });
        });
    });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div>
     <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Edit Job/Internship"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    
<br />
        <div class="centrealign">
        <div class="lbl">
         <table class="style1">
             <tr>
                 <td class="largelbl">
                    <asp:Label ID="lblType" runat="server" Text="Select Type"></asp:Label>
                     <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" 
                         onselectedindexchanged="ddlType_SelectedIndexChanged">
                        <asp:ListItem Selected="True">Job</asp:ListItem>
                        <asp:ListItem>Internship</asp:ListItem>
                    </asp:DropDownList>
                 </td>
             </tr>
             <tr>
                 <td>
                    <asp:Label ID="lblCourse" runat="server" Text="Placement for Course"></asp:Label>
                     <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
    onselectedindexchanged="ddlCourse_SelectedIndexChanged">
                             </asp:DropDownList>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlType" 
                                 EventName="SelectedIndexChanged" />
                         </Triggers>
                     </asp:UpdatePanel>
                 </td>
             </tr>
             <tr>
                 <td>
                    <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                     <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <asp:DropDownList ID="ddlCompanyName" runat="server" AutoPostBack="True" 
                                 onselectedindexchanged="ddlCompanyName_SelectedIndexChanged">
                             </asp:DropDownList>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlType" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlType" 
                                 EventName="SelectedIndexChanged" />
                         </Triggers>
                     </asp:UpdatePanel>
                 </td>
             </tr>
             <tr>
                 <td>
                    <asp:Label ID="lblJobProfile" runat="server" Text="Work Profile"></asp:Label>
                     <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <asp:DropDownList ID="ddlWorkProfile" runat="server" AutoPostBack="True" 
                                 onselectedindexchanged="ddlWorkProfile_SelectedIndexChanged">
                             </asp:DropDownList>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCompanyName" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlType" 
                                 EventName="SelectedIndexChanged" />
                         </Triggers>
                     </asp:UpdatePanel>
                 </td>
             </tr>
             <tr>
                 <td>
                    <asp:Label ID="lblLocation" runat="server" Text="Interview Location"></asp:Label>
                     <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtLocation" 
    ErrorMessage="Interview Location is required">&nbsp;</asp:RequiredFieldValidator>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlWorkProfile" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCompanyName" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlType" 
                                 EventName="SelectedIndexChanged" />
                         </Triggers>
                     </asp:UpdatePanel>
                 </td>
             </tr>
             <tr>
                 <td>
                    <asp:Label ID="lblDateTime" runat="server" Text="Placement Date and Time"></asp:Label>
                     <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <asp:TextBox Width="140px" ID="txtDateTime" runat="server" 
    class="txtdatepicker" onkeypress="return false"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtDateTime" 
    ErrorMessage="DateTime is required">&nbsp;</asp:RequiredFieldValidator>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlType" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCompanyName" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlWorkProfile" 
                                 EventName="SelectedIndexChanged" />
                         </Triggers>
                     </asp:UpdatePanel>
                 </td>
             </tr>
             <tr>
                 <td>
                    <asp:Label ID="lblEligibility" runat="server" Text="Eligibility"></asp:Label>
                     <asp:Label ID="Label20" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 </td>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <asp:TextBox ID="txtEligibility" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtEligibility" 
    ErrorMessage="Eligibility is required">&nbsp;</asp:RequiredFieldValidator>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlType" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCompanyName" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlWorkProfile" 
                                 EventName="SelectedIndexChanged" />
                         </Triggers>
                     </asp:UpdatePanel>
                 </td>
             </tr>
             <tr>
                 <td>
                    <asp:Label ID="lblWebsite" runat="server" Text="Company Website"></asp:Label>
                 </td>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <asp:TextBox ID="txtWebsite" runat="server"></asp:TextBox>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlType" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCompanyName" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlWorkProfile" 
                                 EventName="SelectedIndexChanged" />
                         </Triggers>
                     </asp:UpdatePanel>
                 </td>
             </tr>
             <tr>
                 <td>
                    <asp:Label ID="lblValid" runat="server" Text="Valid Placement"></asp:Label>
                 </td>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <asp:DropDownList ID="ddlValid" runat="server">
                                 <asp:ListItem Selected="True">Yes</asp:ListItem>
                                 <asp:ListItem>No</asp:ListItem>
                             </asp:DropDownList>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlType" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlCompanyName" 
                                 EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="ddlWorkProfile" 
                                 EventName="SelectedIndexChanged" />
                         </Triggers>
                     </asp:UpdatePanel>
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

