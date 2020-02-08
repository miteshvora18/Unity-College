<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="AddSubject.aspx.cs" Inherits="OtherPages_AddSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">

    <div>
    <div class="profile">
        <asp:Label ID="lblheader" class="header" runat="server" Text="Add Subject"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <br />
    <div class="centrealign">
        <table style="width:100%" class="lbl">
            <tr>
                <td class="adminlbl">
        
                    <asp:Label ID="lblCourse" runat="server" Text="Select Course"></asp:Label>
        
                    <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                
                </td>
            </tr>
            <tr>
                <td>
                
                    <asp:Label ID="lblSem" runat="server" Text="Select Semester"></asp:Label>
                
                    <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlSem" runat="server">
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
                
                    <asp:Label ID="lblSubName" runat="server" Text="Subject Name"></asp:Label>
                
                    <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                
                    <asp:TextBox ID="txtSubName" runat="server"></asp:TextBox>
                
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtSubName" ErrorMessage="Name is required">&nbsp;</asp:RequiredFieldValidator>
                
                </td>
            </tr>
            <tr>
                <td>
                
                    <asp:Label ID="lblFaculty" runat="server" Text="Select Faculty"></asp:Label>
                
                    <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                
                    <asp:DropDownList ID="ddlFaculty" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFaculty_SelectedIndexChanged">
                    </asp:DropDownList>
                
                </td>
            </tr>
            <tr>
                <td>
                
                    <asp:Label ID="lblDept" runat="server" Text="Faculty Department"></asp:Label>
                
                </td>
                <td>
                
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtDept" runat="server" ReadOnly="True"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlFaculty" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                
                </td>
            </tr>
            <tr>
                <td>
                
                    <asp:Label ID="lblValid" runat="server" Text="Valid Subject"></asp:Label>
                
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
                
                    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                        Text="Submit" CssClass="btnEventRegister" />
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
            <tr>
                <td>
                
                    &nbsp;</td>
                <td>
                
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</div>

</asp:Content>

