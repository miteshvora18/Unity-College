using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.Net;
using System.Net.Mail;

public partial class OtherPages_AddFaculty : System.Web.UI.Page
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

            //Check entered username through ajax and display message on blur event..
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

            //Check entered email through ajax and display message on blur event..
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
                ddlDept.Items.Add("--Select--");

                var dept = (from c in ue.Courses
                            select c).ToList();
                if (dept.Count != 0)
                {
                    foreach (var data in dept)
                        ddlDept.Items.Add(data.cname);
                }
                ddlDept.SelectedIndex = 0;
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }

    }
    /// <summary>
    /// On Submit, Faculty is added and mail sent to faculty
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var getUsername = (from u in ue.Users
                               where u.username == txtUsername.Text
                               select u).FirstOrDefault();

            var getMail = (from u in ue.Users
                           where u.uemail == txtEmail.Text
                           select u).FirstOrDefault();

            var course = (from c in ue.Courses
                          where c.cname == ddlDept.Text
                          select c).FirstOrDefault();


            if (ddlDept.SelectedIndex != 0)
            {
                //Check for username
                if (getUsername == null)
                {
                    //Check for email
                    if (getMail == null)
                    {
                        Users newFaculty = new Users();
                        newFaculty.uFullname = txtName.Text;
                        newFaculty.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", course.cid);
                        newFaculty.username = txtUsername.Text;
                        newFaculty.upassword = txtPassword.Text;
                        newFaculty.uemail = txtEmail.Text;
                        newFaculty.uContact = Convert.ToInt64(txtContact.Text);
                        newFaculty.uInterest = txtInterest.Text;
                        if (ddlValid.SelectedIndex == 0)
                            newFaculty.uvalid = true;
                        else
                            newFaculty.uvalid = false;
                        newFaculty.RolesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Roles", "rid", 3);
                        ue.AddToUsers(newFaculty);
                        ue.SaveChanges();

                        //Send Mail
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("mitesh.vora@webaccess.co.in", "Admin,UnityCollege");
                        mail.To.Add("mitesh.vora@webaccess.co.in");

                        //mail.To.Add(txtEmail.Text); //Actual Email Id to be sent
                        mail.Subject = "Faculty Login Details to log into Unity College Website";
                        mail.Body = "Hello " + txtUsername.Text + ",\n\nThis is a system generated message from Unity College.\n\nYou have been registered as a faculty into Unity College Website.\n\nKindly login to website using following details:\nUsername:" + newFaculty.username + "\nPassword:" + newFaculty.upassword + "\n\nThank You,\nAdmin,UnityCollege";

                        SmtpClient smtp = new SmtpClient("webaccess.co.in", 25);
                        //smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential("mitesh.vora@webaccess.co.in", "musu30in");
                        smtp.Timeout = 20000;
                        //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.Send(mail);

                        lblMsg.Text = "Success!!!Faculty added and mail sent with login details.";
                        txtName.Text = txtUsername.Text = txtEmail.Text = txtContact.Text = txtInterest.Text = "";
                        ddlDept.SelectedIndex = 0;
                    }
                    else
                        lblMsg.Text = "Email exists!";
                }
                else
                    lblMsg.Text = "Username exists!";
            }
            else
                lblMsg.Text = "No department selected!";
        }
        catch (TimeoutException e2)
        {
            lblMsg.Text = "Mail could not be sent due to network error..";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
