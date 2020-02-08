using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using unitycollegeModel;
using System.Data;

public partial class EventRegister : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    DateTime nextMonth = DateTime.Now.AddDays(30);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                ddlEventName.Items.Add("--Select--");
                //Registration opens before one month..
                var getEvents = (from e1 in ue.Event
                                 where e1.eendatetime > DateTime.Now && e1.estartdatetime <= nextMonth && e1.evalid == true
                                 select e1).ToList();
                if (getEvents.Count != 0)
                {
                    foreach (var data in getEvents)
                        ddlEventName.Items.Add(data.ename);
                }
                else
                    lblMsg.Text = "No event available now";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }

    //public bool ValEmail(string email)
    //{
    //    Regex reg = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
    //    if (reg.IsMatch(email))
    //    {
            
    //        return true;
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Invalid EmailId";
    //        return false;
    //    }
    //}


    /// <summary>
    /// Register for event on Submit..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEventName.SelectedIndex != 0)
            {
                var nextDay = DateTime.Now.AddDays(1);

                var getEvents = (from e1 in ue.Event
                                 where e1.eendatetime > DateTime.Now && e1.estartdatetime <= nextMonth && e1.evalid == true
                                 select e1).ToList();

                var getEvent = (from e2 in ue.Event
                                where e2.ename == ddlEventName.Text
                                select e2).FirstOrDefault();

                var checkduplicateuser = (from re in ue.RegisterEvent
                                          where re.Event.eid == getEvent.eid && re.reemail == txtEmail.Text
                                          select re).ToList();

                //If no event
                if (getEvents.Count != 0 && getEvent != null)
                {
                    //To check if new user for event
                    if (checkduplicateuser.Count == 0)
                    {
                        //If finite seats
                        if (getEvent.eseatsavailable != null)
                        {
                            if (getEvent.eseatsavailable > 0)
                            {
                                if (getEvent.eendatetime > DateTime.Now.AddMinutes(30))
                                {
                                    RegisterEvent EventRegistration = new RegisterEvent();
                                    EventRegistration.EventReference.EntityKey = new EntityKey("unitycollegeEntities1.Event", "eid", getEvent.eid);
                                    EventRegistration.refullname = txtName.Text;
                                    EventRegistration.reemail = txtEmail.Text;
                                    EventRegistration.recollege = txtCollege.Text;
                                    EventRegistration.recourse = txtCourse.Text;
                                    ue.AddToRegisterEvent(EventRegistration);
                                    ue.SaveChanges();

                                    getEvent.eseatsavailable--;
                                    ue.SaveChanges();
                                    lblMsg.Text = "Success,you have registered for event!!!";
                                }
                                else
                                    lblMsg.Text = "Seat cannot be booked now as the time is less than half hour before event ends!";
                            }
                            else
                                lblMsg.Text = "No seats available";
                        }
                        else //If no limit on seats
                        {
                            if (getEvent.eendatetime > DateTime.Now.AddMinutes(30))
                            {
                                RegisterEvent EventRegistration = new RegisterEvent();
                                EventRegistration.EventReference.EntityKey = new EntityKey("unitycollegeEntities1.Event", "eid", getEvent.eid);
                                EventRegistration.refullname = txtName.Text;
                                EventRegistration.reemail = txtEmail.Text;
                                EventRegistration.recollege = txtCollege.Text;
                                EventRegistration.recourse = txtCourse.Text;
                                ue.AddToRegisterEvent(EventRegistration);
                                ue.SaveChanges();
                                lblMsg.Text = "Success,you have registered for event!!!";
                            }
                            else
                                lblMsg.Text = "Seat cannot be booked now as the time is less than half hour before event ends!";
                        }
                    }
                    else
                        lblMsg.Text = "You are already registered for this event!";
                }
                else
                    lblMsg.Text = "You cannot register as no event is available now!!";
                txtCollege.Text = txtCourse.Text = txtEmail.Text = txtName.Text = "";
                ddlEventName.SelectedIndex = 0;
            }
            else
                lblMsg.Text = "No event selected";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
