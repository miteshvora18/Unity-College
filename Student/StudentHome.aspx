<%@ Page Title="" Language="C#" MasterPageFile="Student.master" AutoEventWireup="true" CodeFile="StudentHome.aspx.cs" Inherits="OtherPages_StudentHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudentHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentContent" Runat="Server">
    <div>
    <div>
    <div class="editprofile">
        <div class="profile">
            <asp:Label ID="lblHead" class="header" runat="server" Text="Your Profile"></asp:Label>
        </div><br />
        <div class="color">
        <div>
            Name
        </div>
        <div>
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
        </div><br />
        <div>
            Email
        </div>
        <div>
            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
        </div><br />
        <div>
            Contact Number
        </div>
        <div>
            <asp:Label ID="lblContact" runat="server" Text=""></asp:Label>
        </div><br />
        <div>
            Course Name
        </div>
        <div>
            <asp:Label ID="lblCourse" runat="server" Text=""></asp:Label>
        </div><br />
        <div>
            Course Start Date
        </div>
        <div>
            <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
        </div><br />
        <div>
            Course End Date
        </div>
        <div>
            <asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>
        </div><br />
        <div>
            Current Sem
        </div>
        <div>
            <asp:Label ID="lblSem" runat="server" Text=""></asp:Label>
        </div><br />
        <div>
            Your Extra Courses
        </div>
        <div>
            <asp:Label ID="lblExtraCourse" runat="server" Text=""></asp:Label>
        </div>
        </div>
        <br />
        <div>
            <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" CssClass="btnEventRegister" 
                onclick="btnEditProfile_Click" />
            <br />
            <br />
            <asp:HyperLink ID="hyChangePass" NavigateUrl="../ChangePassword.aspx" 
                runat="server" Font-Size="Large" ForeColor="#669999">Change Password</asp:HyperLink>
        </div>
    </div>
    <div class="loginhead">
    Welcome to Student Home...
    </div>
    </div>
    <br />
    <div>
        <asp:HyperLink ID="hlkAddEvent" runat="server" Font-Size="Large" 
            Font-Underline="True" ForeColor="#669999" NavigateUrl="AddEventStudent.aspx" 
            Visible="False">Add Event</asp:HyperLink><br />
        <asp:HyperLink ID="hlkEditEvent" runat="server" Font-Size="Large" 
            Font-Underline="True" ForeColor="#669999" NavigateUrl="EditEventStudent.aspx" 
            Visible="False">Edit Event</asp:HyperLink><br />
            <br />
            <div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </div>
        
    </div>
</div>
</asp:Content>

