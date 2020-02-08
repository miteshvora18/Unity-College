<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="StudentDirectory.aspx.cs" Inherits="OtherPages_StudentDirectory" %>

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
        <asp:Label ID="lblheader" class="header" runat="server" Text="Student Directory"></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    <br />
    <div style="padding-left:40%;" class="lbl">
        <table class="style1">
            <tr>
                <td class="adminlbl">
                    <asp:Label ID="lblCourse" runat="server" Text="Select Course"></asp:Label>
                </td>
                <td>
        <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlCourse_SelectedIndexChanged">
        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                                                <asp:Label ID="lblSem" runat="server" 
                        Text="Select Semester"></asp:Label>

     
</td>
                <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
        </table>


        <p style="margin-left: 120px">
        <asp:Button ID="btnSubmit" Width="60px" class="btnEventRegister" runat="server" 
            Text="Submit" onclick="btnSubmit_Click" />
        <br />
        <asp:Label ID="lblMsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
        </p>
    </div>
<br />
    
    <div>
        <asp:GridView ID="gvStudentDirectory" runat="server" 
            onrowcommand="gvStudentDirectory_RowCommand" CellSpacing="10" 
             Width="100%" BorderColor="#003300" BorderStyle="Solid" BorderWidth="5px" 
            Font-Size="Medium" ForeColor="#003300"
            >
        <Columns>
        
        <asp:TemplateField ShowHeader="False" >
            <ItemTemplate> 
                <asp:Button ID="btnEdit" class="btnEventRegister" Width="70px" runat="server" Text="Edit" CommandName="Seen" CommandArgument='<%#Eval("username") %>'/>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
            <HeaderStyle Font-Size="Medium" ForeColor="Maroon" />
            <AlternatingRowStyle ForeColor="#000066" />
        </asp:GridView>
    </div>

</div>

</asp:Content>

