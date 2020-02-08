using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

public partial class AdminPages_AddStudent : System.Web.UI.Page
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

            lblMsg.Text = "";
            lblUsernameValid.Text = "";

            //Gets username through ajax and checks if it already exists for another user..
            if (Request.QueryString["a"] != null)
            {
                if (Request.QueryString["a"].ToString() == "1")
                {
                    var user = Request.QueryString["user"].ToString();
                    var getUsername = (from u in ue.Users
                                       where u.username == user
                                       select u).ToList();
                    if (user != "")
                    {
                        if (getUsername.Count != 0)
                        {
                            Response.Clear();
                            Response.Write("Username exists");
                            Response.Flush();
                            Response.Close();
                        }
                        else
                        {
                            Response.Clear();
                            Response.Write("Valid");
                            Response.Flush();
                            Response.Close();
                        }
                    }
                }
            }

            //Check if email entered in textbox exists for another user..
            if (Request.QueryString["b"] != null)
            {
                if (Request.QueryString["b"].ToString() == "1")
                {
                    var mail = Request.QueryString["mail"].ToString();
                    var getMail = (from u in ue.Users
                                   where u.uemail == mail
                                   select u).FirstOrDefault();
                    if (mail != "")
                    {
                        if (getMail != null)
                        {
                            Response.Clear();
                            Response.Write("Email exists");
                            Response.Flush();
                            Response.Close();
                        }
                        else
                        {
                            Response.Clear();
                            Response.Write("");
                            Response.Flush();
                            Response.Close();
                        }
                    }
                }
            }

            if (!IsPostBack)
            {
                var getCourse = (from c in ue.Courses
                                 where c.cvalid == true
                                 select c).ToList();

                ddlCourse.Items.Add("--Select--");
                foreach (var data in getCourse)
                    ddlCourse.Items.Add(data.cname.ToString());

                ddlCurrentSem.Items.Add("--Select--");
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates list of semesters for that course
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

            if (ddlCourse.SelectedIndex != 0)
            {
                for (int i = 1; i <= getCourse; i++)
                {
                    ddlCurrentSem.Items.Add(i.ToString());
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }
    /// <summary>
    /// Adds Student on Submit and sends email with login details..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var checkGS = (from u in ue.Users
                         where u.ugs == true
                         select u).ToList();

            var courseStartDate = (from c in ue.Courses
                                    where c.cname == ddlCourse.Text
                                    select c.cstartyear).FirstOrDefault();

            var course = (from c in ue.Courses
                          where c.cname == ddlCourse.Text
                          select c).FirstOrDefault();

            var getEmail = (from u in ue.Users
                        where u.uemail == txtEmail.Text
                        select u).FirstOrDefault();

            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlCurrentSem.SelectedIndex != 0)
                {
                    //If selected student is entered as Gs, checks if gs already exists.
                    if (checkGS.Count == 0 || ddlGs.SelectedIndex == 0)
                    {
                        //Checks if username exists..
                        if (CheckUsername(txtUsername.Text) == false)
                        {
                            //Checks if email already exists for another user..
                            if (getEmail == null)
                            {
                                //Check if Student's course start date is greater than Course's Start Year..
                                if (Convert.ToDateTime(txtStartDate.Text) > courseStartDate)
                                {
                                    Users newStudent = new Users();
                                    newStudent.uFullname = txtName.Text;
                                    newStudent.username = txtUsername.Text;
                                    newStudent.upassword = txtPassword.Text;
                                    newStudent.uvalid = true;
                                    newStudent.uemail = txtEmail.Text;
                                    newStudent.uContact = Convert.ToInt64(txtContact.Text);
                                    newStudent.RolesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Roles", "rid", 2);
                                    if (ddlGs.SelectedIndex == 0)
                                        newStudent.ugs = false;
                                    else
                                        newStudent.ugs = true;
                                    ue.AddToUsers(newStudent);
                                    ue.SaveChanges();


                                    StudentCourse newStudentCourse = new StudentCourse();
                                    newStudentCourse.UsersReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Users", "uid", newStudent.uid);
                                    newStudentCourse.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", course.cid);
                                    newStudentCourse.scStartdate = Convert.ToDateTime(txtStartDate.Text);
                                    newStudentCourse.scEnddate = Convert.ToDateTime(txtStartDate.Text).AddYears(course.cduration);
                                    newStudentCourse.scCurrentsem = Convert.ToInt32(ddlCurrentSem.Text);
                                    ue.AddToStudentCourse(newStudentCourse);
                                    ue.SaveChanges();

                                    //Send Mail

                                    MailMessage mail = new MailMessage();
                                    mail.From = new MailAddress("mitesh.vora@webaccess.co.in", "Admin,UnityCollege");
                                    mail.To.Add("mitesh.vora@webaccess.co.in");

                                    //mail.To.Add(txtEmail.Text); //Actual Email Id to be sent
                                    mail.Subject = "Student Login Details to log into Unity College Website";
                                    mail.Body = "Hello " + txtUsername.Text + ",\n\nThis is a system generated message from Unity College.\n\nYou have been registered as a student into Unity College Website.\n\nKindly login to website using following details:\nUsername:" + newStudent.username + "\nPassword:" + newStudent.upassword + "\n\nThank You,\nAdmin,UnityCollege";

                                    SmtpClient smtp = new SmtpClient("webaccess.co.in", 25);
                                    //smtp.EnableSsl = true;
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Credentials = new NetworkCredential("mitesh.vora@webaccess.co.in", "musu30in");
                                    smtp.Timeout = 20000;
                                    //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                                    smtp.Send(mail);

                                    lblMsg.Text = "Success!!!Student added and Mail sent with login details.";

                                    txtName.Text = txtUsername.Text = txtEmail.Text = txtContact.Text = txtStartDate.Text = "";
                                    ddlCurrentSem.Items.Clear();
                                    ddlCurrentSem.Items.Add("--Select--");
                                    ddlCurrentSem.SelectedIndex = ddlCourse.SelectedIndex = ddlGs.SelectedIndex = 0;
                                }
                                else
                                    lblMsg.Text = "Admission year should be after " + courseStartDate.Date.ToString("MMM. dd yyyy"); ;
                            }
                            else
                                lblMsg.Text = "Email exists!";
                        }
                        else
                            lblMsg.Text = "Username already exists!";
                    }
                    else if (checkGS.Count != 0 && ddlGs.SelectedIndex == 1)
                        lblMsg.Text = "General Secretary already exists.";
                }
                else
                    lblMsg.Text = "Error! No Semester selected";
            }
            else
                lblMsg.Text = "Error! Course must be selected";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }

    /// <summary>
    /// Function to check if username exists..
    /// </summary>
    /// <param name="name">Name of the Student</param>
    /// <returns></returns>
    public bool CheckUsername(string name)
    {
        var getUsername = (from u in ue.Users
                           where u.username == name
                           select u).ToList();

        if (getUsername.Count == 0)
            return false;
        else
            return true;
    }

}

