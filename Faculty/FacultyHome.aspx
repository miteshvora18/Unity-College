<%@ Page Title="" Language="C#" MasterPageFile="Faculty.master" AutoEventWireup="true" CodeFile="FacultyHome.aspx.cs" Inherits="OtherPages_FacultyHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FacultyHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FacultyContent" Runat="Server">
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
            Department
        </div>
        <div>
            <asp:Label ID="lblDept" runat="server" Text=""></asp:Label>
        </div><br />
        <div>
            Your Subjects
        </div>
        <div>
            <asp:Label ID="lblSubjects" runat="server" Text=""></asp:Label>
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
    Welcome to Faculty Home...
    </div>
    </div>
    <br />
    <div>
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
</div>
</asp:Content>

