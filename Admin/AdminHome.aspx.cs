using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_AdminHome : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Checks valid login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            //List of unread complaints..
            var unreadComplaint = (from f in ue.Feedback
                                   join ft in ue.FeedbackType
                                   on f.FeedbackType.ftid equals ft.ftid
                                   where f.funread == true && ft.ftname == "Complaints"
                                   select f).Count();

            if (unreadComplaint != 0)
            {
                lblMsg.Text += "<span class='notify'><a href='UnreadFeedback.aspx?type=Complaints'>You have " + unreadComplaint.ToString() + " unread Complaint(s)</a></span><br/>";
            }

            //List of unread suggestions..
            var unreadSuggestion = (from f in ue.Feedback
                                    join ft in ue.FeedbackType
                                    on f.FeedbackType.ftid equals ft.ftid
                                    where f.funread == true && ft.ftname == "Suggestions"
                                    select f).Count();

            if (unreadSuggestion != 0)
            {
                lblMsg.Text += "<span class='notify'><a href='UnreadFeedback.aspx?type=Suggestions'>You have " + unreadSuggestion.ToString() + " unread Suggestion(s)</a></span><br/>";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
