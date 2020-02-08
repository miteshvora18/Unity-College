using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class AllEvents : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Gets events on selected day..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gvEvents.DataSource = "";
            gvEvents.DataBind();
            btnRegister.Visible = false;

            if (!IsPostBack)
            {
                DateTime selectedDate = Convert.ToDateTime(Session["selecteddate"]);
                DateTime nextDay = selectedDate.AddDays(1);
                //DateTime selecteddate=cl
                var getEvent = (from e1 in ue.Event
                                where e1.estartdatetime <= nextDay && e1.eendatetime >= selectedDate && e1.evalid == true
                                select new { Name = e1.ename, Start = e1.estartdatetime, End = e1.eendatetime, Location = e1.elocation, Description = e1.elongdesc }).ToList();

                //if (getselecteddayevent.Count != 0)
                //{
                //    foreach (var data in getselecteddayevent)
                //        lblSelectedDayEvent.Text += "<div class='eventheader'>" + data.ename + "</div><br/><div class='eventcontent'>Start:\t" + Convert.ToDateTime(data.estartdatetime).ToString("MMM.dd,yyyy ") + data.estartdatetime.ToShortTimeString() + "<br/>End:  " + Convert.ToDateTime(data.eendatetime).ToString("MMM.dd,yyyy ") + data.eendatetime.ToShortTimeString() + "<br/>Where:" + data.elocation + "<br/>Description:" + data.eshortdesc + "</div><br/><br/>";
                //}
                //else
                //{
                //    lblSelectedDayEvent.Text = "<div style='color:red;font-size:x-large;padding-left:40%;vertical-align:middle'>No events on this day!</div>";
                //    btnRegister.Visible = false;
                //}

                //Bind data in gridview..
                if (getEvent.Count != 0)
                {
                    var eventList = getEvent.Select(u => new { Name = u.Name, Start = u.Start.ToString("MMM. dd yyyy HH:mm"), End = u.End.ToString("MMM. dd yyyy HH:mm"), Location = u.Location, Description = u.Description }).ToList();
                    gvEvents.DataSource = eventList;
                    gvEvents.DataBind();
                    btnRegister.Visible = true;
                }
                else
                {
                    lblSelectedDayEvent.Text = "<div style='color:red;font-size:x-large;padding-left:40%;vertical-align:middle'>No events on this day!</div>";
                    btnRegister.Visible = false;
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("EventRegister.aspx");
    }
}
