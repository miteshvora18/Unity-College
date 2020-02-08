using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.Net;
using System.Net.Mail;
using System.Threading;

public partial class AdminPages_EditStudentA : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    
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

            //Session["studentusername"] = "test";
            if (!IsPostBack)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");
                ddlCurrentSem.Items.Add("--Select--");

                if (Session["studentusername"] != null)
                {
                    var getStudent = (string)Session["studentusername"];

                    var userData = (from u in ue.Users
                                    join sc in ue.StudentCourse
                                    on u.uid equals sc.Users.uid
                                    join c in ue.Courses
                                    on sc.Courses.cid equals c.cid
                                    where u.username == getStudent
                                    select new { u, c, sc }).FirstOrDefault();

                    //Loads course list in drop down list..
                    var allCourse = (from c in ue.Courses
                                     select c).ToList();
                    foreach (var data in allCourse)
                        ddlCourse.Items.Add(data.cname);


                    txtUsername.Text = getStudent;
                    //txtPassword.Text = userdata.u.upassword;
                    txtName.Text = userData.u.uFullname;
                    //Select Course initially
                    ddlCourse.Text = userData.c.cname;

                    for (int i = 1; i <= userData.c.csemesters; i++)
                        ddlCurrentSem.Items.Add(i.ToString());

                    ddlCurrentSem.Text = userData.sc.scCurrentsem.ToString();
                    txtEmail.Text = userData.u.uemail;
                    txtContact.Text = userData.u.uContact.ToString();
                    txtStartdate.Text = userData.sc.scStartdate.ToShortDateString();
                    txtEnddate.Text = userData.sc.scEnddate.ToShortDateString();

                    if (userData.u.ugs == true)
                        ddlGs.SelectedIndex = 0;
                    else
                        ddlGs.SelectedIndex = 1;

                    if (userData.u.uvalid == true)
                        ddlValidUser.SelectedIndex = 0;
                    else
                        ddlValidUser.SelectedIndex = 1;
                }
                else
                    lblMsg.Text = "Go to Directory-->Student to get list of Students that can be edited!!";

           }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Gets list of semesters for selected course
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCurrentSem.Items.Clear();
            ddlCurrentSem.Items.Add("--Select--");

            var getCourse = (from c in ue.Courses
                             where c.cname == ddlCourse.Text
                             select c.csemesters).FirstOrDefault();

            if (getCourse != null)
            {
                for (int i = 1; i <= getCourse; i++)
                    ddlCurrentSem.Items.Add(i.ToString());
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Student details updated on Submit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string currentuser = (string)Session["studentusername"];

            var getUser = (from u in ue.Users
                           join sc in ue.StudentCourse
                           on u.uid equals sc.Users.uid
                           join c in ue.Courses
                           on sc.Courses.cid equals c.cid
                           where u.username == currentuser
                           select new { u, c, sc }).FirstOrDefault();

            var selectedCourse = (from c in ue.Courses
                                  where c.cname == ddlCourse.Text
                                  select c).FirstOrDefault();

            var gs = (from u in ue.Users
                      where u.ugs == true && u.username!=txtUsername.Text
                      select u).FirstOrDefault();

            var getEmail = (from u in ue.Users
                            where u.uemail == txtEmail.Text && u.username!=txtUsername.Text
                            select u).FirstOrDefault();

            if (getUser != null)
            {
                if (ddlCourse.SelectedIndex != 0)
                {
                    if (ddlCurrentSem.SelectedIndex != 0)
                    {
                        //Check if email exists..
                        if (getEmail == null)
                        {
                            if (Convert.ToDateTime(txtStartdate.Text).Date > selectedCourse.cstartyear)
                            {
                                if (Convert.ToDateTime(txtEnddate.Text).Date > Convert.ToDateTime(txtStartdate.Text))
                                {
                                    if (gs == null || ddlGs.SelectedIndex == 1)
                                    {
                                        getUser.u.uFullname = txtName.Text;
                                        getUser.u.uemail = txtEmail.Text;
                                        getUser.u.uContact = Convert.ToInt64(txtContact.Text);
                                        getUser.sc.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", selectedCourse.cid);
                                        getUser.sc.scCurrentsem = Convert.ToInt32(ddlCurrentSem.Text);
                                        getUser.sc.scStartdate = Convert.ToDateTime(txtStartdate.Text);
                                        getUser.sc.scEnddate = Convert.ToDateTime(txtEnddate.Text);
                                        //}


                                        if (ddlGs.SelectedIndex == 0)
                                            getUser.u.ugs = true;
                                        else
                                            getUser.u.ugs = false;
                                        if (ddlValidUser.SelectedIndex == 0)
                                            getUser.u.uvalid = true;
                                        else
                                            getUser.u.uvalid = false;
                                        ue.SaveChanges();
                                        
                                        //Send Mail

                                        MailMessage mail = new MailMessage();
                                        mail.From = new MailAddress("mitesh.vora@webaccess.co.in", "Admin,UnityCollege");
                                        mail.To.Add("mitesh.vora@webaccess.co.in");

                                        //mail.To.Add(txtEmail.Text); //Actual Email Id to be sent
                                        mail.Subject = "Details of modified profile for your account in Unity College Website";
                                        mail.Body = "Hello " + txtUsername.Text + ",\n\nThis is a system generated message from Unity College.\n\nYour account details in Unity College have been modified.\n\nKindly find details below:\nUsername:" + getUser.u.username + "\nName:" + getUser.u.uFullname + "\nCourse:" + selectedCourse.cname + "\nSemester:" + ddlCurrentSem.Text + "\nEmail:" + txtEmail.Text + "\nContact:" + txtContact.Text + "\nAdmission Date:" + Convert.ToDateTime(txtStartdate.Text).ToString("MMM. dd yyyy") + "\nEnd Date:" + Convert.ToDateTime(txtEnddate.Text).ToString("MMM. dd yyyy") + "\n\nThank You,\nAdmin,UnityCollege";

                                        SmtpClient smtp = new SmtpClient("webaccess.co.in", 25);
                                        //smtp.EnableSsl = true;
                                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        smtp.Credentials = new NetworkCredential("mitesh.vora@webaccess.co.in", "musu30in");
                                        smtp.Timeout = 20000;
                                        //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                                        smtp.Send(mail);

                                        Session.Remove("studentusername");
                                        lblMsg.Text = "Details Updated and mail sent to student!!";
                                        //Thread.Sleep(5000);
                                        //Response.Redirect("StudentDirectory.aspx");
                                        //ClientScript.RegisterStartupScript(GetType(), "alert", "<script>alert('Details Updated and mail sent to student!!!');</script>");
                                        
                                    }
                                    else
                                        lblMsg.Text = "General Secretary already exists!";
                                }
                                else
                                    lblMsg.Text = "Admission date must be greated than course end date";
                            }
                            else
                                lblMsg.Text = "Invalid start date as " + selectedCourse.cname + " was available only after " + selectedCourse.cstartyear.ToString("MMM. dd yyyy");
                        }
                        else
                            lblMsg.Text = "Email already exists for another user!";
                    }
                    else
                        lblMsg.Text = "No semester selected!";
                }
                else
                    lblMsg.Text = "No course selected!";
            }
            else
                lblMsg.Text = "No student selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }

    }
}
