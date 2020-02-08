<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Courses.aspx.cs" Inherits="Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
            <div style="text-align:center">
            <asp:Label ID="lblRegCourseHead" runat="server" BackColor="#CCCC00" 
                BorderColor="#CCCC00" BorderStyle="Solid" Font-Bold="True" Font-Italic="True" 
                Font-Names="Palatino Linotype" Font-Overline="False" Font-Size="X-Large" 
                Font-Strikeout="False" Text="REGULAR COURSES" ForeColor="#CC0000"></asp:Label>
            </div>
        <br />
    <asp:Label ID="lblCourseData" runat="server"></asp:Label>
            <br />
            <div>
                
        <asp:GridView ID="gvCourse" runat="server" CellSpacing="10" 
             Width="100%" BorderColor="#003300" BorderStyle="Solid" BorderWidth="5px" 
            Font-Size="Medium" ForeColor="#003300" BackColor="White"
            >
            <HeaderStyle Font-Size="Medium" ForeColor="Maroon" BackColor="White" />
            <AlternatingRowStyle ForeColor="#000066" />
        </asp:GridView>
                
            </div>
</asp:Content>

