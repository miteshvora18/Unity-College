<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ExtraCourses.aspx.cs" Inherits="ExtraCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
    <div style="text-align:center">
        <asp:Label ID="lblExCourseHead" runat="server" BackColor="#CCCC00" 
            BorderColor="#CCCC00" BorderStyle="Solid" Font-Bold="True" Font-Italic="True" 
            Font-Names="Palatino Linotype" Font-Overline="False" Font-Size="X-Large" 
            Font-Strikeout="False" ForeColor="#CC0000" Text="EXTRA COURSES"></asp:Label>
        <br />
        <br />
    </div>
    <div>
        <asp:Label ID="lblExCourse" runat="server"></asp:Label>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    
    <div>
        
        <asp:GridView ID="gvExtraCourse" runat="server" CellSpacing="10" 
             Width="100%" BorderColor="#003300" BorderStyle="Solid" BorderWidth="5px" 
            Font-Size="Medium" ForeColor="#003300" BackColor="White"
            >
            <HeaderStyle Font-Size="Medium" ForeColor="Maroon" BackColor="White" />
            <AlternatingRowStyle ForeColor="#000066" />
        </asp:GridView>
                
    </div>
    <br />
    </asp:Content>

