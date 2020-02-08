using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.Net;
using System.Net.Mail;

public partial class OtherPages_EditFaculty : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                ddlDept.Items.Clear();
                ddlDept.Items.Add("--Select--");
                var course = (from c in ue.Courses
                              select c.cname).ToList();
                foreach (var data in course)
                    ddlDept.Items.Add(data);

                string user = (string)Session["facultyusername"];

                var currentFaculty = (from u in ue.Users
                                      join c in ue.Courses
                                      on u.Courses.cid equals c.cid
                                      where u.username == user
                                      select new { u, c }).FirstOrDefault();

                if (user != null)
                {
                    txtUsername.Text = user;
                    txtName.Text = currentFaculty.u.uFullname;
                    txtEmail.Text = currentFaculty.u.uemail;
                    txtContact.Text = currentFaculty.u.uContact.ToString();
                    txtInterest.Text = currentFaculty.u.uInterest;
                    ddlDept.Text = currentFaculty.c.cname;
                    if (currentFaculty.u.uvalid == true)
                        ddlValid.SelectedIndex = 0;
                    else
                        ddlValid.SelectedIndex = 1; 
                    
                }
                else
                {
                    lblMsg.Text = "Go to Directory -->Faculty to get list of faculty whose details can be edited!!";
                    ddlDept.SelectedIndex = 0;
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Faculty details are updated on Submit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var email = (from u in ue.Users
                         where u.uemail == txtEmail.Text && u.username != txtUsername.Text
                         select u).FirstOrDefault();

            var user = (from u in ue.Users
                        where u.username == txtUsername.Text
                        select u).FirstOrDefault();

            var course = (from c in ue.Courses
                          where c.cname == ddlDept.Text
                          select c).FirstOrDefault();

            var loginFacultySubjects = (from sf in ue.SubjectFaculty
                                        join u in ue.Users
                                        on sf.Users.uid equals u.uid
                                        where u.username == user.username                             
                                        select sf.Subjects.sid).ToList();

            if (ddlDept.SelectedIndex != 0)
            {
                //Check if email exists..
                if (email == null)
                {
                    user.uFullname = txtName.Text;
                    user.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", course.cid);
                    user.uemail = txtEmail.Text;
                    user.uContact = Convert.ToInt64(txtContact.Text);
                    user.uInterest = txtInterest.Text;
                    if (ddlValid.SelectedIndex == 0)
                    {
                        //To check if subject faculty also to be made true..
                        if (loginFacultySubjects.Count != 0)
                        {
                            foreach (var data in loginFacultySubjects)
                            {
                               // var subjectsId = data.Subjects.sid;

                                var subject=(from s in ue.Subjects
                                             where s.sid == data
                                             select s).FirstOrDefault();

                                var presentFaculty = (from sf in ue.SubjectFaculty
                                                      join u in ue.Users
                                                      on sf.Users.uid equals u.uid
                                                      where sf.Subjects.sid == subject.sid
                                                      orderby sf.sfstartdate descending
                                                      select new { u ,sf}).FirstOrDefault();

                                if (presentFaculty.u.username == user.username)
                                {
                                    if (subject.svalid == true)
                                    {
                                        presentFaculty.sf.sfvalid = true;
                                    }
                                }                           
                            }
                        }
                        user.uvalid = true;
                    }
                    else
                    {
                        var loginFacultySubject = (from sf in ue.SubjectFaculty
                                                    join u in ue.Users
                                                    on sf.Users.uid equals u.uid
                                                    where u.username == user.username
                                                    select sf).ToList();

                        //Make rows of faculty subjects as false..
                        if (loginFacultySubject.Count != 0)
                        {
                            foreach (var data in loginFacultySubject)
                                data.sfvalid = false;
                        }
                        user.uvalid = false;
                    }
                    ue.SaveChanges();

                    //Send Mail

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("mitesh.vora@webaccess.co.in", "Admin,UnityCollege");
                    mail.To.Add("mitesh.vora@webaccess.co.in");

                    //mail.To.Add(txtEmail.Text); //Actual Email Id to be sent
                    mail.Subject = "Details of modified profile for your account in Unity College Website";
                    mail.Body = "Hello " + txtUsername.Text + ",\n\nThis is a system generated message from Unity College.\n\nYour account details in Unity College have been modified.\n\nKindly find details below:\nUsername:" + user.username + "\nName:" + user.uFullname + "\nEmail:" + txtEmail.Text + "\nContact:" + txtContact.Text + "\nDepartment:" + course.cname + "\nInterest Area:" + user.uInterest + "\n\nThank You,\nAdmin,UnityCollege";

                    SmtpClient smtp = new SmtpClient("webaccess.co.in", 25);
                    //smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential("mitesh.vora@webaccess.co.in", "musu30in");
                    smtp.Timeout = 20000;
                    //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    smtp.Send(mail);

                    lblMsg.Text = "Success!!!Profile Updated Successfully and mail sent to faculty!";
                }
                else
                    lblMsg.Text = "Email already exists";
            }
            else
                lblMsg.Text = "No department selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
