using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.Net;
using System.Net.Mail;

public partial class ForgotPassword : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    /// <summary>
    /// Sends mail to user with login details to registered emailid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var email = (from u in ue.Users
                         where u.uemail == txtEmail.Text
                         select u).FirstOrDefault();

            if (email != null)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mitesh.vora@webaccess.co.in", "Admin,Unity College");
                mail.To.Add("mitesh.vora@webaccess.co.in");

                //mail.To.Add(txtEmail.Text); //Actual Email Id to be sent
                mail.Subject = "Password";
                mail.Body = "Hello " + email.uFullname + ",\n\nThis is a system generated message from Unity College and this message was generated on your request.<br/><br/>Kindly Login to website using following details:\nUsername:" + email.username + "\nPassword:" + email.upassword + "\n\nThank You,\nAdmin,Unity College";

                SmtpClient smtp = new SmtpClient("webaccess.co.in", 25);
                //smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("mitesh.vora@webaccess.co.in", "musu30in");
                smtp.Timeout = 20000;
                //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);

                lblMsg.Text = "Success, Email Sent!!!";
            }
            else
                lblMsg.Text = "Email id is not registered!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
