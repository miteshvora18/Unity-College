<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="EditAlumni.aspx.cs" Inherits="OtherPages_EditAlumni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div>
     <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Edit Alumni Details"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    
<br />
        <div class="centrealign">
        <div class="lbl">
        <table class="style1">
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                    <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtName" ErrorMessage="Name is required">&nbsp;</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ErrorMessage="Name must only have alphabets" 
                    ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtName">Only alphabets are allowed</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="lblCourse" runat="server" Text="Course"></asp:Label>
                    <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" onselectedindexchanged="ddlCourse_SelectedIndexChanged" 
                    >
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="lblPassYear" runat="server" Text="Passout Year"></asp:Label>
                    <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlPassYear" runat="server" 
                            >
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
                <td>
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                    <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Email is required">&nbsp;</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtEmail" ErrorMessage="Email is incorrect" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid email format</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="lblWork" runat="server" Text="Work Profile"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtWorkProfile" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" CssClass="btnEventRegister" 
                     />
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

