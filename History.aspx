<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
    <div style="vertical-align:top;position:relative;top:0">
        <div style="text-align:center">
            <asp:Label ID="lblHisHead" runat="server" BackColor="#CCCC00" 
                BorderColor="#CCCC00" BorderStyle="Solid" Font-Bold="True" Font-Italic="True" 
                Font-Names="Palatino Linotype" Font-Overline="False" Font-Size="X-Large" 
                Font-Strikeout="False" Text="HISTORY" ForeColor="#CC0000"></asp:Label>
        </div>
        <div>
        
            <p class="principaltext">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Unity College is one of the prominent colleges in Mumbai producing quality 
            engineers from 1989 that help make earth a better place to live in for all living beings. In 
                its 24th year of existence, it has won several accreditation and awards from various 
                recognised bodies. One of the core values of the institution is unity while 
                maintaining highest standard of moral and ethical integrity. Under leadersip of 
                Neeraj Sawant, Principal, we aim to achieve even more success. Along with 
                academics, we place emphasis on sports that provides required physical and 
                mental balance essential for success in any field.</p>
            <div class="eventheader">Established</div>
            <div class="text">1989</div><br />
            <br />
            <div class="eventheader">Faculty</div>
            <div class="text">More than150 full time faculty members and 50 visting faculty from various 
            colleges.</div><br />
            <br />
            <div class="eventheader">Students</div>
            <div class="text">About 600 from all branches</div><br />
            <br />
            
            <div class="eventheader">Living Alumni</div>
            <div class="text">More than 10,000 from all over India</div><br />
            <br />
            <div class="eventheader">Library Collection</div>
            <div class="text">About 1 million books and magazines from all over the world on diverse subjects</div><br />
            <br />
        
        </div>
    </div>
   
</asp:Content>

