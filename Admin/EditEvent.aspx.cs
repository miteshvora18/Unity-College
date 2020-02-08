using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_EditEvent : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            lblMsg.Text = "";

            if (!IsPostBack)
            {
                ddlName.Items.Clear();
                ddlName.Items.Add("--Select--");

                var date = DateTime.Now;
                var events = (from e1 in ue.Event
                              orderby e1.estartdatetime descending
                              select e1).ToList();
                if (events.Count != 0)
                {
                    foreach (var data in events)
                        ddlName.Items.Add(data.ename);
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Gets details of selected event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtEndDate.Text = txtLocation.Text = txtLongDesc.Text = txtSeats.Text = txtShortDesc.Text = txtStartDate.Text = "";
            ddlValid.SelectedIndex = 0;

            var selectedEvent = (from e1 in ue.Event
                                 where e1.ename == ddlName.Text
                                 select e1).FirstOrDefault();

            if (selectedEvent != null)
            {
                txtStartDate.Text = selectedEvent.estartdatetime.ToString("MM/dd/yyyy HH:mm");
                txtEndDate.Text = selectedEvent.eendatetime.ToString("MM/dd/yyyy HH:mm");
                txtLocation.Text = selectedEvent.elocation;
                txtLongDesc.Text = selectedEvent.elongdesc;
                txtShortDesc.Text = selectedEvent.eshortdesc;
                //if(selevent.eseats!=null)
                txtSeats.Text = selectedEvent.eseats.ToString();
                if (selectedEvent.evalid == true)
                    ddlValid.SelectedIndex = 0;
                else
                    ddlValid.SelectedIndex = 1;
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Saves changes for that Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var events = (from e1 in ue.Event
                          where e1.ename == ddlName.Text
                          select e1).FirstOrDefault();

            int seatDiff = Convert.ToInt32(events.eseats) - Convert.ToInt32(events.eseatsavailable);

            var eventRegistered = (from re in ue.RegisterEvent
                                   join e1 in ue.Event
                                   on re.Event.eid equals e1.eid
                                   where e1.ename == ddlName.Text
                                   select re).ToList();


            if (ddlName.SelectedIndex != 0)
            {
                //Event end datetime must be greater than start datetime..
                if (Convert.ToDateTime(txtEndDate.Text) > Convert.ToDateTime(txtStartDate.Text))
                {
                    if (events.eseats != null)
                    {
                        if (Convert.ToInt32(txtSeats.Text) - seatDiff >= 0)
                        {
                            events.eshortdesc = txtShortDesc.Text;
                            events.elongdesc = txtLongDesc.Text;
                            events.estartdatetime = Convert.ToDateTime(txtStartDate.Text);
                            events.eendatetime = Convert.ToDateTime(txtEndDate.Text);
                            events.elocation = txtLocation.Text;
                            if (txtSeats.Text != "")
                                events.eseats = Convert.ToInt32(txtSeats.Text);
                            else
                                events.eseats = null;
                            if (ddlValid.SelectedIndex == 0)
                                events.evalid = true;
                            else
                                events.evalid = false;
                            ue.SaveChanges();

                            lblMsg.Text = "Success!!!Record Updated!";

                            txtEndDate.Text = txtLocation.Text = txtLongDesc.Text = txtSeats.Text = txtShortDesc.Text = txtStartDate.Text = "";
                            ddlValid.SelectedIndex = 0;

                        }
                        else
                            lblMsg.Text = "Please enter seats greater than " + seatDiff + " as they have already been booked!";
                    }
                    else
                    {
                        events.eshortdesc = txtShortDesc.Text;
                        events.elongdesc = txtLongDesc.Text;
                        events.estartdatetime = Convert.ToDateTime(txtStartDate.Text);
                        events.eendatetime = Convert.ToDateTime(txtEndDate.Text);
                        events.elocation = txtLocation.Text;

                        if (txtSeats.Text != "")
                        {
                            if (Convert.ToInt32(txtSeats.Text) - eventRegistered.Count() >= 0)
                            {
                                events.eshortdesc = txtShortDesc.Text;
                                events.elongdesc = txtLongDesc.Text;
                                events.estartdatetime = Convert.ToDateTime(txtStartDate.Text);
                                events.eendatetime = Convert.ToDateTime(txtEndDate.Text);
                                events.elocation = txtLocation.Text;
                                events.eseats = Convert.ToInt32(txtSeats.Text);
                                if (ddlValid.SelectedIndex == 0)
                                    events.evalid = true;
                                else
                                    events.evalid = false;
                                ue.SaveChanges();

                                lblMsg.Text = "Success!!!Record Updated!";

                                txtEndDate.Text = txtLocation.Text = txtLongDesc.Text = txtSeats.Text = txtShortDesc.Text = txtStartDate.Text = "";
                                ddlValid.SelectedIndex = 0;
                            }
                            else
                                lblMsg.Text = "Seats must be greater than " + eventRegistered.Count() + " as they are already registered!";
                        }
                        else
                        {
                            events.eshortdesc = txtShortDesc.Text;
                            events.elongdesc = txtLongDesc.Text;
                            events.estartdatetime = Convert.ToDateTime(txtStartDate.Text);
                            events.eendatetime = Convert.ToDateTime(txtEndDate.Text);
                            events.elocation = txtLocation.Text;
                            if (ddlValid.SelectedIndex == 0)
                                events.evalid = true;
                            else
                                events.evalid = false;
                            ue.SaveChanges();

                            lblMsg.Text = "Success!!!Record Updated!";

                            txtEndDate.Text = txtLocation.Text = txtLongDesc.Text = txtSeats.Text = txtShortDesc.Text = txtStartDate.Text = "";
                            ddlValid.SelectedIndex = 0;
                        }
                    }
                }
                else
                    lblMsg.Text = "Error!Event End DateTime must be greater than Start DateTime!";
            }
            else
                lblMsg.Text = "No event selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }
}
