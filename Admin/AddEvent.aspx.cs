using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_AddEvent : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            //Checks valid login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            if (!IsPostBack)
            {
                /*
                ddlStartTime.Items.Clear();
                ddlStartTime.Items.Add("--Select--");

                ddlEndTime.Items.Clear();
                ddlEndTime.Items.Add("--Select--");

                for (double i = 0; i < 24; i++)
                {
                    ddlStartTime.Items.Add((i + ":00").ToString());
                    ddlStartTime.Items.Add((i + ":30").ToString());

                    ddlEndTime.Items.Add((i + ":00").ToString());
                    ddlEndTime.Items.Add((i + ":30").ToString());
                }
                 * */
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }

    /// <summary>
    /// Adds Event on Submit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var eventExists = (from e1 in ue.Event
                               where e1.ename == txtName.Text
                               select e1).FirstOrDefault();
            //Checks if event already exists..
            if (eventExists == null)
            {
                //Event End datetime must be greater than start datetime..
                if (Convert.ToDateTime(txtEndDate.Text) > Convert.ToDateTime(txtStartDate.Text))
                {
                    unitycollegeModel.Event newEvent = new unitycollegeModel.Event();
                    newEvent.ename = txtName.Text;
                    newEvent.eshortdesc = txtShortDesc.Text;
                    newEvent.elongdesc = txtLongDesc.Text;
                    //DateTime date = new DateTime(Convert.ToInt32(ddlStartYear.Text), 1, 1);
                    newEvent.estartdatetime = Convert.ToDateTime(txtStartDate.Text);
                    newEvent.eendatetime = Convert.ToDateTime(txtEndDate.Text);
                    newEvent.elocation = txtLocation.Text;
                    if (txtSeats.Text != "")
                    {
                        newEvent.eseats = Convert.ToInt32(txtSeats.Text);
                        newEvent.eseatsavailable = Convert.ToInt32(txtSeats.Text);
                    }
                    else
                    {
                        newEvent.eseats = null;
                        newEvent.eseatsavailable = null;
                    }
                    if (ddlValid.SelectedIndex == 0)
                        newEvent.evalid = true;
                    else
                        newEvent.evalid = false;
                    ue.AddToEvent(newEvent);
                    ue.SaveChanges();

                    lblMsg.Text = "Success!!!Event Saved!";
                    txtEndDate.Text = txtLocation.Text = txtLongDesc.Text = txtName.Text = txtSeats.Text = txtShortDesc.Text = txtStartDate.Text = "";
                    ddlValid.SelectedIndex = 0;

                }
                else
                    lblMsg.Text = "End DateTime must greater than Start DateTime";
            }
            else
                lblMsg.Text = "Event already exists!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }
}
