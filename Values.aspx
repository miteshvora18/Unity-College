<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Values.aspx.cs" Inherits="Values" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildPages" Runat="Server">
    <div style="vertical-align:top;">
        <div style="text-align:center">
            <asp:Label ID="lblHisHead" runat="server" BackColor="#CCCC00" 
                BorderColor="#CCCC00" BorderStyle="Solid" Font-Bold="True" Font-Italic="True" 
                Font-Names="Palatino Linotype" Font-Overline="False" Font-Size="X-Large" 
                Font-Strikeout="False" Text="VALUES" ForeColor="#CC0000"></asp:Label>
        </div>
        <div class="ValueProverb">
        “Your beliefs become your thoughts, <br />
        Your thoughts become your words,<br />
        Your words become your actions,<br />
        Your actions become your habits, <br />
        Your habits become your values,<br /> 
        Your values become your destiny”<br />
        ―Mahatma Gandhi<br />
        </div>
        <br />
        <div class="l">
        <div class="valuehead">
            The following core values are fundamental to Unity College</div>
            <br />
            <div class="eventheader">Unity</div>
            <div class="text">“United we stand, divided we fall.”
            <br />
            It is important that people be united as the multitudes of united force together 
            can achieve the impossible and success is than a mere effect of that cause and 
            hence the name &#39;Unity College&#39;.</div> <br />
            <br />
            <div class="eventheader">Peace</div>
            <div class="text">It is said that best comes out when there is peace of mind, body and soul and we 
            shall strive to create an atmosphere that harnesses peace in college and campus.</div><br />
            <br />
            <div class="eventheader">Excellence</div>
            <div class="text">Excellence is merely a by-product of hardwork in the right direction with right knowledge.<br />
            We shall strive to be an epitome of excellence. </div>
            <br />
            <br />
            <div class="eventheader">High ethical and moral standard</div>
            <div class="text">It is imperative to do the right things in the right way at the right time.<br />
            To achieve this, we are committed to creating a community that acts with honesty and forthrightness.</div>
            <br />
        </div>
        
     </div>
<br />
        
</asp:Content>

