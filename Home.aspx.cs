using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.Globalization;
public partial class _Default : System.Web.UI.Page 
{
    unitycollegeEntities1 ue = new unitycollegeEntities1(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                #region Get News
                int count = 0;
                var latestNews = (from n in ue.News
                                  where n.nvalid == true
                                  orderby n.ndatetime descending
                                  select n).Take(4).ToList();
                foreach (var data in latestNews)
                {
                    if (count == 0)
                    {
                        lblNews1.Text = "<a href='News.aspx?x=1'>" + data.nheader + "</a><br/>" + data.nshortdesc;
                        count++;
                        continue;
                    }
                    else if (count == 1)
                    {
                        lblNews2.Text = "<a href='News.aspx?x=2'>" + data.nheader + "</a><br/>" + data.nshortdesc;
                        count++;
                        continue;
                    }
                    else if (count == 2)
                    {
                        lblNews3.Text = "<a href='News.aspx?x=3'>" + data.nheader + "</a><br/>" + data.nshortdesc;
                        count++;
                        continue;
                    }
                    else if (count == 3)
                    {
                        lblNews4.Text = "<a href='News.aspx?x=4'>" + data.nheader + "</a><br/>" + data.nshortdesc;
                        count++;
                        continue;
                    }
                    else
                        break;
                }
                #endregion

                #region Get Events
                int cnt = 0;
                var getDate = DateTime.Now;
                //var nextmonth = DateTime.Now.Date.AddDays(1);
                //var gettenthday=DateTime.Now.AddDays(10);

                var eventsToday = (from e1 in ue.Event
                                   where e1.eendatetime > getDate && e1.evalid == true
                                   orderby e1.estartdatetime
                                   select e1).Take(4);

                if (eventsToday.Count() != 0)
                {
                    foreach (var data in eventsToday)
                    {
                        if (cnt == 0)
                        {
                            lblEvent1.Text = "<a href='Event.aspx?x=1'>" + data.ename + "</a><br/><b>Start:</b> " + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/><b>End:</b>  " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString();//+"," + data.eendatetime.ToShortTimeString();
                            cnt++;
                            continue;
                            //DateTime.ParseExact(data.eendatetime.ToString(), "yy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("MMM. dd, yyyy HH:mm:ss");
                        }
                        else if (cnt == 1)
                        {
                            lblEvent2.Text = "<a href='Event.aspx?x=2'>" + data.ename + "</a><br/><b>Start:</b>  " + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/><b>End:</b>  " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString();
                            cnt++;
                            continue;
                        }
                        else if (cnt == 2)
                        {
                            lblEvent3.Text = "<a href='Event.aspx?x=3'>" + data.ename + "</a><br/><b>Start:</b>  " + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/><b>End:</b>  " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString();
                            cnt++;
                            continue;
                        }
                        else if (cnt == 3)
                        {
                            lblEvent4.Text = "<a href='Event.aspx?x=4'>" + data.ename + "</a><br/><b>Start:</b> " + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/><b>End:</b> " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString();
                            cnt++;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                    lblEvent1.Text = "<div style='color:red;font-size:large;text-align:center'>No events available now!</div>";
                #endregion
            }
        }
        catch (Exception e1)
        {
            ClientScript.RegisterStartupScript(GetType(),"alert","alert('Error:" + e1.Message+"');",true);
        }
    }
}
