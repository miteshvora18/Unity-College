<%@ Page Title="" Language="C#" MasterPageFile="Faculty.master" AutoEventWireup="true" CodeFile="EditExamRelatedFaculty.aspx.cs" Inherits="OtherPages_EditExamRelated" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FacultyHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FacultyContent" Runat="Server">
<div>
    <div class="profile">
        <asp:Label ID="lblHeader" class="header" runat="server" Text="Edit Exam Schedule/Result"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <br />
    
    <div class="centrealign">
        
        <table style="width:100%" class="lbl">
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblOption" runat="server" Text="Select Option"></asp:Label>
                    <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOption" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlOption_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblCourse" runat="server" Text="Select Course"></asp:Label>
                    <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                        
    onselectedindexchanged="ddlCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlOption" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSem" runat="server" Text="Select Semester"></asp:Label>
                    <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlSem" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlSem_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlOption" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFile" runat="server" Text="Upload File"></asp:Label>
                    <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblValid" runat="server" Text="Valid"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlValid" runat="server">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlOption" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCourse" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlSem" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btnEventRegister" 
                        onclick="btnSubmit_Click" style="height: 26px" />
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

