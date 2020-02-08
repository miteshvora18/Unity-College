using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_ViewEventRegister : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            //Check Valid Login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            gvEventDirectory.DataSource = "";
            gvEventDirectory.DataBind();

            if (!IsPostBack)
            {
                ddlEventList.Items.Clear();
                ddlEventList.Items.Add("--Select--");

                var eventList = (from e1 in ue.Event
                                 orderby e1.estartdatetime descending
                                 select e1).ToList();

                if (eventList.Count != 0)
                {
                    foreach (var data in eventList)
                        ddlEventList.Items.Add(data.ename);
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Displays regsitered people in gridview for selected event..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEventList.SelectedIndex != 0)
            {
                var registrationList = (from e1 in ue.Event
                                        join re in ue.RegisterEvent
                                        on e1.eid equals re.Event.eid
                                        where e1.ename == ddlEventList.Text
                                        select new { Name = re.refullname, Email = re.reemail, College = re.recollege, Course = re.recourse }).ToList();

                if (registrationList.Count != 0)
                {
                    gvEventDirectory.DataSource = registrationList;
                    gvEventDirectory.DataBind();
                }
                else
                    lblMsg.Text = "No registrations for this event!";
            }
            else
                lblMsg.Text = "No event selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
