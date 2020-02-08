<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EventRegister.aspx.cs" Inherits="EventRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
        
        <div class="profile">
          
                    <asp:Label ID="lblHeader" runat="server" CssClass="header"
                        Text="Register for Event"></asp:Label>
          
          </div>
          
          <div style="padding-left:30%;" class="lbl">
            <table style="width:100%;">
            <tr>
                <td style="width:150px">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td >
                    <asp:Label ID="lblEventName" runat="server" Text="Event Name"></asp:Label>
        
                    <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:DropDownList ID="ddlEventName" 
            runat="server">
        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="ddlEventName" ErrorMessage="Event is required">&nbsp;</asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lblName" runat="server" 
            Text="Full Name"></asp:Label>
                    <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtName" 
            runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtName" ErrorMessage="Name is required">&nbsp;</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtName" ErrorMessage="Full name must have only alphabets" 
                        ValidationExpression="^[a-zA-Z\s]+$">Only Alphabets are valid</asp:RegularExpressionValidator>
                    </td>
            </tr>
            <tr>
                <td >
                    Email<asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="Email is required">&nbsp;</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="Invalid Email" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td >
                    College<asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCollege" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtCollege" ErrorMessage="College is required">&nbsp;</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td >
                    Course</td>
                <td>
                    <asp:TextBox ID="txtCourse" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btnEventRegister"
                        onclick="btnSubmit_Click"  />
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            
                </td>
            </tr>
            </table>
            </div>
                                            
</asp:Content>

