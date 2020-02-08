<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PrincipalView.aspx.cs" Inherits="PrincipalView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
    <div>

    <div style="text-align:center;">
    <asp:Label ID="lblHisHead" runat="server" BackColor="#CCCC00" 
                BorderColor="#CCCC00" BorderStyle="Solid" Font-Bold="True" Font-Italic="True" 
                Font-Names="Palatino Linotype" Font-Overline="False" Font-Size="X-Large" 
                Font-Strikeout="False" Text="PRINCIPAL'S VIEW" ForeColor="#CC0000"></asp:Label>
        <br />
        <br />
    </div>
    <div class="principaltext">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        In today’s era of globalization, qualified technocrats and managers filled with enthusiasm and spirit of hard work blended with self confidence are required in an organization for achieving its goals. 
        It gives me utmost pleasure and pride in associating myself with Unity College that has been providing sound technical education that is par excellence.
        We have appointed well trained, research oriented and challenge seeking faculty from all the corners of India.
        With their sound knowledge base and skills, the students are prepared for action and vision in the years to come. 
        We strive hard for forming character of our students which, according to Herbert Spencer, is the ultimate aim of education.
        I believe that the institute will continue to produce competent technocrats who will make significant contribution to the corporate world and industries all over the world which will enable them to serve as global citizens.
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Moreover, 
        we feel that education should be based on merit and other factors should not&nbsp; 
        affect student from entering in this college. So, a special fee waiver or 
        discount is given to the students based on his/her financial condition. This is responsibility of every college that 
        students get equal chance to move ahead in their career without any obstacle.
        <br />
        <br />
    </div>
    <div style="text-align:center">
        <img src="Images/Principal.jpeg" runat="server" title />
    </div>
    <div style="font-weight:bold;text-align:center;font-size:large">
        Principal, Unity College<br />
        Neeraj Sawant<br />
        B.E, M.E, Ph.D.
    </div>
</div>
</asp:Content>

