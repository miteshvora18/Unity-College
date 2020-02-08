using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_UnreadFeedback : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Gets list of unread feedback by admin
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check Valid Login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            lblMsg.Text = "";
            gvFeedback.DataSource = "";
            gvFeedback.DataBind();

            string feedbackType = Request.QueryString["type"];
            lblFeedback.Text = feedbackType;

            //Gets list of unread feedback from db..
            var unreadFeedback = (from f in ue.Feedback
                                  join ft in ue.FeedbackType
                                  on f.FeedbackType.ftid equals ft.ftid
                                  where ft.ftname == feedbackType && f.funread == true
                                  orderby f.fdate descending
                                  select new { Name = f.fname, Email = f.femail, Subject = f.fsubject, Content = f.fcontent, Date = f.fdate }).ToList();

            if (unreadFeedback.Count != 0)
            {
                gvFeedback.DataSource = unreadFeedback;
                gvFeedback.DataBind();

                //Make feedback unread=false, as soon as they are viewed..
                var feedsRead = (from f in ue.Feedback
                                 join ft in ue.FeedbackType
                                 on f.FeedbackType.ftid equals ft.ftid
                                 where ft.ftname == feedbackType
                                 select f).ToList();

                if (feedsRead.Count != 0)
                {
                    foreach (var data in feedsRead)
                        data.funread = false;
                    ue.SaveChanges();
                }
            }
            else
                lblMsg.Text = "No unread Feedback!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminHome.aspx");
    }
}
