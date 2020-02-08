<%@ Page Title="" Language="C#" MasterPageFile="Faculty.master" AutoEventWireup="true" CodeFile="MessageFaculty.aspx.cs" Inherits="OtherPages_Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FacultyHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FacultyContent" Runat="Server">
<div>
      <div class="profile">
        <asp:Label ID="lblStudent" class="header" runat="server" Text=""></asp:Label>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    </div>
    <br />
     <div style="text-align:center">

        <br />
        
    <div style="text-align:center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                         <div style="font-size:large">
                Enter Message(Upto 150 characters)
             </div>
                  <asp:TextBox ID="txtSend" runat="server" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="btnSend" runat="server" CssClass="btnEventRegister"
            Text="Send" onclick="btnSend_Click" />
                 <br />
                 <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <asp:RegularExpressionValidator ID="txtConclusionValidator2" ControlToValidate="txtSend" 
                     Text="Exceeding 150 characters" ValidationExpression="^[\s\S]{0,150}$" 
                     runat="server" />
    
                         <br />
    <br />
                    <asp:Label ID="lblPrevMsg" runat="server" Text=""></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSend" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </div>
</div>
</asp:Content>

