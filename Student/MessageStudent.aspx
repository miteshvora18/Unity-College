<%@ Page Title="" Language="C#" MasterPageFile="Student.master" AutoEventWireup="true" CodeFile="MessageStudent.aspx.cs" Inherits="OtherPages_Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudentHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentContent" Runat="Server">
<div>
     <div class="profile">
        <asp:Label ID="lblFaculty" class="header" runat="server" Text=""></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    <br />
    <div style="text-align:center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
             <div style="text-align:center">
             <div style="font-size:large">
                Enter Message(Upto 150 characters)
             </div>
        <br />
        <asp:TextBox ID="txtSend" runat="server" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="btnSend" runat="server" CssClass="btnEventRegister"
            Text="Send" onclick="btnSend_Click" />
                 <br />
                 <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <asp:RegularExpressionValidator ID="txtConclusionValidator2" ControlToValidate="txtSend" 
                     Text="Exceeding 150 characters" ValidationExpression="^[\s\S]{0,150}$" 
                     runat="server" />
    </div>
    <br />
                    <asp:Label ID="lblPrevMsg" runat="server" Text=""></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSend" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
</asp:Content>

