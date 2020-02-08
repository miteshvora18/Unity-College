using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_Feedback : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gvFeedback.DataSource = "";
            gvFeedback.DataBind();

            //Check Valid Login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            //Add feedback options to select..
            if (!IsPostBack)
            {
                ddlFeedback.Items.Clear();
                ddlFeedback.Items.Add("--Select--");
                ddlFeedback.Items.Add("Suggestions");
                ddlFeedback.Items.Add("Complaints");
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Gets list of Feedback for Complaints/Suggestions
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlFeedback.SelectedIndex != 0)
            {
                var feedback = (from f in ue.Feedback
                                join ft in ue.FeedbackType
                                on f.FeedbackType.ftid equals ft.ftid
                                where ft.ftname == ddlFeedback.Text
                                orderby f.fdate descending
                                select new { Name = f.fname, Email = f.femail, Subject = f.fsubject, Content = f.fcontent, Date = f.fdate }).ToList();

                if (feedback.Count != 0)
                {
                    gvFeedback.DataSource = feedback;
                    gvFeedback.DataBind();

                    //Make unread feedback as read..
                    var feedbackRead = (from f in ue.Feedback
                                        join ft in ue.FeedbackType
                                        on f.FeedbackType.ftid equals ft.ftid
                                        where ft.ftname == ddlFeedback.Text
                                        select f).ToList();
                    if (feedbackRead.Count != 0)
                    {
                        foreach (var data in feedbackRead)
                            data.funread = false;
                        ue.SaveChanges();
                    }
                }
                else
                    lblMsg.Text = "No feedback available!";
            }
            else
                lblMsg.Text = "Select any one of the options to view feedback!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
