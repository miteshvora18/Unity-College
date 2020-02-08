using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class MasterPage : System.Web.UI.MasterPage
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            //Display latest news in marquee
            int count = 0;
            var latestNews = (from n in ue.News
                              orderby n.ndatetime descending
                              select n).Take(4).ToList();
            foreach (var data in latestNews)
            {
                if (count == 0)
                {
                    lblNews1.Text = "<a href='News.aspx?x=1'>" + data.nheader + "</a><br/><br/>";
                    count++;
                    continue;
                }
                else if (count == 1)
                {
                    lblNews2.Text = "<a href='News.aspx?x=2'>" + data.nheader + "</a><br/><br/>";
                    count++;
                    continue;
                }
                else if (count == 2)
                {
                    lblNews3.Text = "<a href='News.aspx?x=3'>" + data.nheader + "</a><br/><br/>";
                    count++;
                    continue;
                }
                else if (count == 3)
                {
                    lblNews4.Text = "<a href='News.aspx?x=4'>" + data.nheader + "</a><br/><br/>";
                    count++;
                    continue;
                }
                else
                    break;
            }

            if (Session["selecteddate"] != null)
            {
                DateTime selectedDate = Convert.ToDateTime(Session["selecteddate"]).Date;
                clEventDisplay.VisibleDate = selectedDate;
                clEventDisplay.SelectedDate = selectedDate;
                clEventDisplay.SelectedDayStyle.BackColor = System.Drawing.Color.Gold;
                clEventDisplay.SelectedDayStyle.ForeColor = System.Drawing.Color.Maroon;
                clEventDisplay.SelectedDayStyle.Font.Bold = true;
                Session.RemoveAt(0);
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    protected void clEventDisplay_SelectionChanged(object sender, EventArgs e)
    {
        Session["selecteddate"] = clEventDisplay.SelectedDate;
        Response.Redirect("AllEvents.aspx");
    }
    protected void clEventDisplay_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            // Display event dates in yellow boxes
            Style eventStyle = new Style();
            //eventStyle.BackColor = System.Drawing.Color.Yellow;
            eventStyle.ForeColor = System.Drawing.Color.BlueViolet;

            //eventStyle.BorderWidth = 1;

            //Display today's date with green boxes
            Style todayStyle = new Style();
            //todayStyle.BackColor = System.Drawing.Color.Green;
            todayStyle.ForeColor = System.Drawing.Color.White;

            var nextDay = e.Day.Date.AddDays(1);

            var getDayEvent = (from e1 in ue.Event
                               where e1.estartdatetime <= nextDay && e1.eendatetime >= e.Day.Date && e1.evalid == true
                               select e1).ToList();

            if (getDayEvent.Count != 0)
            {
                e.Cell.ApplyStyle(eventStyle);
                //Label aLabel = new Label();
                //aLabel.Text = " <br>" + "E";
                //e.Cell.Controls.Add(aLabel);
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }/*
    protected void txtdatepicker_TextChanged(object sender, EventArgs e)
    {
        
        if (txtdatepicker.Text != "")
        {
            Session["selecteddate"] = txtdatepicker.Text;
            Response.Redirect("AllEvents.aspx");
        }
    }*/
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var getUser = (from u in ue.Users
                           join r in ue.Roles
                           on u.Roles.rid equals r.rid
                           where u.username == txtUsername.Text && u.upassword == txtPassword.Text
                           select new { u, r }).FirstOrDefault();

            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                if (getUser != null)
                {
                    if (getUser.u.uvalid == true)
                    {

                        if (getUser.r.rname == "Admin")
                        {
                            Session["username"] = txtUsername.Text;
                            txtUsername.Text = "";
                            Response.Redirect("Admin/AdminHome.aspx");
                        }
                        if (getUser.r.rname == "Student")
                        {
                            Session["username"] = txtUsername.Text;
                            txtUsername.Text = "";
                            Response.Redirect("Student/StudentHome.aspx");
                        }
                        if (getUser.r.rname == "Faculty")
                        {
                            Session["username"] = txtUsername.Text;
                            txtUsername.Text = "";
                            Response.Redirect("Faculty/FacultyHome.aspx");
                        }
                    }
                    else
                        lblMsg.Text = "You are not authorized to login!!";
                }
                else
                    lblMsg.Text = "Invalid Username and/or Password";
            }
            else
                lblMsg.Text = "Username and Password are required!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }

}