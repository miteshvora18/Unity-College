using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.Net;
using System.Net.Mail;

public partial class OtherPages_ChangePassword : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string currentUser = (string)Session["username"];

            var user = (from u in ue.Users
                        where u.username == currentUser && u.uvalid == true
                        select u).FirstOrDefault();
            if (user == null)
                Response.Redirect("../Login.aspx?error=invalid");
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Changes password for user after verifying old password
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnChange_Click(object sender, EventArgs e)
    {
        try
        {
            string currentUser = (string)Session["username"];

            var user = (from u in ue.Users
                        where u.uvalid == true && u.username == currentUser
                        select u).FirstOrDefault();

            if (user.upassword == txtOldPass.Text.Trim())
            {
                user.upassword = txtNewPass.Text.Trim();
                ue.SaveChanges();

                //Send Mail
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mitesh.vora@webaccess.co.in", "Admin,UnityCollege");
                mail.To.Add("mitesh.vora@webaccess.co.in");

                //mail.To.Add(user.uemail); //Actual Email Id to be sent
                mail.Subject = "Password Change";
                mail.Body = "Hello " + user.uFullname + ",\n\nThis is a system generated message from Unity College.\n\nYour password has been changed for your account in Unity College Website.\n\nKindly find the login details below:\nUsername:" + user.username + "\nPassword:" + user.upassword + "\n\nThank You,\nAdmin,UnityCollege";

                SmtpClient smtp = new SmtpClient("webaccess.co.in", 25);
                //smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("mitesh.vora@webaccess.co.in", "musu30in");
                smtp.Timeout = 20000;
                //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);

                lblMsg.Text = "Password Changed Successfully!!!";
                txtConfirmPass.Text = txtNewPass.Text = txtOldPass.Text = "";
            }
            else
                lblMsg.Text = "Your current password is incorrect!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            string currentUser = (string)Session["username"];

            var user = (from u in ue.Users
                        join r in ue.Roles
                        on u.Roles.rid equals r.rid
                        where u.uvalid == true && u.username == currentUser
                        select r).FirstOrDefault();

            if (user.rid == 1)
                Response.Redirect("Admin/AdminHome.aspx");
            else if (user.rid == 2)
                Response.Redirect("Student/StudentHome.aspx");
            else if (user.rid == 3)
                Response.Redirect("Faculty/FacultyHome.aspx");
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
