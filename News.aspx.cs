using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class News : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Details of specific news selected from home page..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                int newsNumber = Convert.ToInt32(Request.QueryString["x"]);
                int count = 0;
                var latestNews = (from n in ue.News
                                  orderby n.ndatetime descending
                                  select n).Take(4).ToList();

                foreach (var data in latestNews)
                {
                    if (newsNumber == 1)
                    {
                        lblNews.Text = "<div class='dynnewsheader'>" + data.nheader + "</div><br/><div class='dynnewscontent'>" + data.nlongdesc + "</div>";
                        break;
                    }
                    else if (newsNumber == 2)
                    {
                        if (count == 1)
                        {
                            lblNews.Text = "<div class='dynnewsheader'>" + data.nheader + "</div><br/><div class='dynnewscontent'>" + data.nlongdesc + "</div>";
                            break;
                        }
                        count++;
                        continue;
                    }
                    else if (newsNumber == 3)
                    {
                        if (count == 2)
                        {
                            lblNews.Text = "<div class='dynnewsheader'>" + data.nheader + "</div><br/><div class='dynnewscontent'>" + data.nlongdesc + "</div>";
                            break;
                        }
                        count++;
                        continue;
                    }
                    else if (newsNumber == 4)
                    {
                        if (count == 3)
                        {
                            lblNews.Text = "<div class='dynnewsheader'>" + data.nheader + "</div><br/><div class='dynnewscontent'>" + data.nlongdesc + "</div>";
                            break;
                        }
                        count++;
                        continue;
                    }
                    else
                        break;
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
