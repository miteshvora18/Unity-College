<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeFile="Home.aspx.cs" Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="ChildPages" runat="server">
    <table class="homestyle">
        <tr>
            <td colspan="6">
                <img alt="Unity Home" src="Includes/Images/UnityHome2.jpg" style="width:100%; height: 400px;" />
             </td>
        </tr>
        <tr class="homeevent">
            <td style="width:20%;background-color:#A52A2A;color:White;">
                <asp:Label ID="lblEvents" runat="server" Text="Featured Events" 
                    Font-Size="X-Large"></asp:Label>
                <br />
                <a href="Event.aspx">More Events</a> ==> </td>
            <td style="width:20%;">
                <asp:Label ID="lblEvent1" runat="server"></asp:Label>
                </td>
            <td style="width:20%;" colspan="2">
                <asp:Label ID="lblEvent2" runat="server"></asp:Label>
                </td>
            <td style="width:20%;">
                <asp:Label ID="lblEvent3" runat="server"></asp:Label>
                </td>
            <td style="width:20%;">
                <asp:Label ID="lblEvent4" runat="server"></asp:Label>
                </td>
        </tr>
        <tr class="lblNews">
            <td colspan="6">
                <asp:Label ID="lblNews" runat="server" Text="Latest News"></asp:Label>
            </td>
        </tr>
        <tr class="homenews">
            <td colspan="3">
                <asp:Label ID="lblNews1" runat="server"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblNews2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="homenews">
            <td colspan="3">
                <asp:Label ID="lblNews3" runat="server"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblNews4" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">

</asp:Content>

