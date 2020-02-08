using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_Message : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblPrevMsg.Text = "";

            //Check Valid Login..
            var currentUser = (string)Session["username"];
            string studentUsername = Request.QueryString["studentusername"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 3 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            #region Make unread messages as read..
            var readMsg = (from cf in ue.CommunicateFaculty
                           join u in ue.Users
                           on cf.Users2.uid equals u.uid
                           join u2 in ue.Users
                           on cf.Users1.uid equals u2.uid
                           where cf.cfunread == true && cf.Users2.username == currentUser && cf.Users1.username == studentUsername
                           select cf).ToList();
            if (readMsg.Count != 0)
            {
                foreach (var data in readMsg)
                    data.cfunread = false;
                ue.SaveChanges();
            }
            #endregion

            if (!IsPostBack)
            {
                PrevMsg();
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Sends message to student..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            string faculty = (string)Session["username"];
            string student = Request.QueryString["studentusername"];

            var facultyUser = (from u in ue.Users
                               where u.username == faculty
                               select u).FirstOrDefault();

            var studentUser = (from u in ue.Users
                               where u.username == student
                               select u).FirstOrDefault();

            if (txtSend.Text != "")
            {
                CommunicateFaculty cf = new CommunicateFaculty();
                cf.Users1Reference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Users", "uid", facultyUser.uid);
                cf.Users2Reference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Users", "uid", studentUser.uid);
                cf.cfcontent = txtSend.Text;
                cf.cfunread = true;
                cf.cfdatetime = DateTime.Now;
                ue.AddToCommunicateFaculty(cf);
                //cf.cfsubject = "";
                cf.cfvalid = true;
                ue.SaveChanges();

                //lblMsg.Text = "Message Sent!!!";
                lblPrevMsg.Text = "";
                PrevMsg();
                txtSend.Text = "";
            }
            else
                lblMsg.Text = "Message cannot be blank!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    //Gets list of communication between selected student and logged in faculty..
    public void PrevMsg()
    {
        try
        {
            int a = Convert.ToInt32(Request.QueryString["a"]);

            if (a == 1)
            {
                lblPrevMsg.Text = "";
                string facultyUsername = (string)Session["username"];
                string studentUsername = Request.QueryString["studentusername"];

                var student = (from u in ue.Users
                               where u.username == studentUsername
                               select u).FirstOrDefault();

                lblStudent.Text = student.uFullname;

                var prevMsg = (from cf in ue.CommunicateFaculty
                               join u1 in ue.Users
                               on cf.Users1.uid equals u1.uid
                               join u2 in ue.Users
                               on cf.Users2.uid equals u2.uid
                               where (u1.username == studentUsername || u1.username == facultyUsername) && (u2.username == facultyUsername || u2.username == studentUsername)
                               orderby cf.cfdatetime descending
                               select new { cf, u1, u2 }).ToList();
                if (prevMsg.Count != 0)
                {
                    foreach (var data in prevMsg)
                    {
                        if (data.u1.username == facultyUsername)
                            lblPrevMsg.Text += "<div style='text-align:left;padding-left:20%;color:orange;font-size:large'>(" + data.cf.cfdatetime.ToString("MMM. dd yyyy HH:mm") + ")" + "You:" + data.cf.cfcontent + "</div><br/>";
                        else if (data.u1.username == studentUsername)
                            lblPrevMsg.Text += "<div style='text-align:right;padding-right:20%;color:purple;font-size:large'>(" + data.cf.cfdatetime.ToString("MMM. dd yyyy HH:mm") + ")" + data.u1.uFullname + ":" + data.cf.cfcontent + "</div><br/>";
                    }
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
