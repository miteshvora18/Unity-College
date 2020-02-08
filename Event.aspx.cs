using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class Event : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Gets details of next 4 events..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gvEvents.DataSource = "";
            gvEvents.DataBind();

            if (!IsPostBack)
            {
                int eventNumber = Convert.ToInt32(Request.QueryString["x"]);
                int count = 0;
                var getDate = DateTime.Now;
                //var getdateincmonth = DateTime.Now.Date.AddDays(30);
                var nextMonth = DateTime.Now.AddDays(30);
                var eventsToday = (from e1 in ue.Event
                                   where e1.eendatetime > getDate && e1.evalid == true
                                   orderby e1.estartdatetime
                                   select e1).Take(4).ToList();

                var nextEvents = (from e1 in ue.Event
                                  where e1.eendatetime > getDate && e1.estartdatetime < nextMonth && e1.evalid == true
                                  orderby e1.estartdatetime
                                  select new { Name = e1.ename, Start = e1.estartdatetime, End = e1.eendatetime, Location = e1.elocation, Description = e1.elongdesc }).ToList();

                if (eventsToday.Count != 0)
                {
                    foreach (var data in nextEvents)
                    {
                        if (eventNumber == 0)
                        {
                            var eventList = nextEvents.Select(u => new { Name = u.Name, Start = u.Start.ToString("MMM. dd yyyy HH:mm"), End = u.End.ToString("MMM. dd yyyy HH:mm"), Location = u.Location, Description = u.Description }).ToList();
                            gvEvents.DataSource = eventList;
                            gvEvents.DataBind();
                            //lblEvent.Text += "<div class='eventheader'>" + data.ename + "</div><br/><div class='eventcontent'>Start:\t" + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/>End:  " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString() + "<br/>Where:" + data.elocation + "<br/>Description:" + data.elongdesc + "</div><br/><br/>";
                        }
                    }
                    foreach (var data in eventsToday)
                    {
                        if (eventNumber == 0)
                        {
                            //lblEvent.Text += "<div class='eventheader'>" + data.ename + "</div><br/><div class='eventcontent'>When:\t" + data.estartdatetime.Date.ToLongDateString() + "," + data.estartdatetime.ToShortTimeString() + "-" + data.eendatetime.ToShortTimeString() + "<br/>Where:" + data.elocation + "<br/>Description:" + data.elongdesc + "</div><br/><br/>";
                        }
                        else if (eventNumber == 1)
                        {
                            lblEvent.Text = "<div class='eventheader'>" + data.ename + "</div><br/><div class='eventcontent'>Start:\t" + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/>End:  " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString() + "<br/>Where:" + data.elocation + "<br/>Description:" + data.elongdesc + "</div>";
                            break;
                        }
                        else if (eventNumber == 2)
                        {
                            if (count == 1)
                            {
                                lblEvent.Text = "<div class='eventheader'>" + data.ename + "</div><br/><div class='eventcontent'>Start:\t" + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/>End:  " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString() + "<br/>Where:" + data.elocation + "<br/>Description:" + data.elongdesc + "</div>";
                                break;
                            }
                            count++;
                            continue;
                        }
                        else if (eventNumber == 3)
                        {
                            if (count == 2)
                            {
                                lblEvent.Text = "<div class='eventheader'>" + data.ename + "</div><br/><div class='eventcontent'>Start:\t" + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/>End:  " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString() + "<br/>Where:" + data.elocation + "<br/>Description:" + data.elongdesc + "</div>";
                                break;
                            }
                            count++;
                            continue;
                        }
                        else if (eventNumber == 4)
                        {
                            if (count == 3)
                            {
                                lblEvent.Text = "<div class='eventheader'>" + data.ename + "</div><br/><div class='eventcontent'>Start:\t" + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/>End:  " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString() + "<br/>Where:" + data.elocation + "<br/>Description:" + data.elongdesc + "</div>";
                                break;
                            }
                            count++;
                            continue;
                        }
                        else
                            break;
                    }
                }
                else
                {
                    btnRegister.Visible = false;
                    lblEvent.Text = "<div style='text-align:center;color:red;font-size:x-large'>No events available!</div>";
                }
            }
        }
        catch (Exception e1)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Error:"+e1.Message+"');", true);
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("EventRegister.aspx");
    }
}
