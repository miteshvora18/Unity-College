using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_FeedbackF : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Populates Feeedback Type i.e. Complaints/Suggestions 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ddlOption.Items.Clear();
                ddlOption.Items.Add("--Select--");

                var feedback = (from ft in ue.FeedbackType
                                select ft.ftname).ToList();
                if (feedback.Count != 0)
                {
                    foreach (var data in feedback)
                        ddlOption.Items.Add(data);
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Saves feedback and sends to admin..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var feedbackType = (from ft in ue.FeedbackType
                                where ft.ftname == ddlOption.Text
                                select ft).FirstOrDefault();

            var adminUser = (from u in ue.Users
                            join r in ue.Roles
                            on u.Roles.rid equals r.rid
                            where r.rname == "Admin"
                            select u).FirstOrDefault();

            if (ddlOption.SelectedIndex != 0)
            {
                Feedback newFeedback = new Feedback();
                newFeedback.FeedbackTypeReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.FeedbackType", "ftid", feedbackType.ftid);
                newFeedback.fname = txtName.Text;
                newFeedback.femail = txtEmail.Text;
                newFeedback.fsubject = txtSubject.Text;
                newFeedback.fcontent = txtContent.Text;
                newFeedback.UsersReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Users", "uid", adminUser.uid);
                newFeedback.fdate = DateTime.Now;
                newFeedback.funread = true;
                ue.AddToFeedback(newFeedback);
                ue.SaveChanges();

                lblMsg.Text = "Your feedback is received! Thank you!!!";

                txtContent.Text = txtEmail.Text = txtName.Text = txtSubject.Text = "";
                ddlOption.SelectedIndex = 0;
            }
            else
                lblMsg.Text = "Feedback Option must be selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }
}
