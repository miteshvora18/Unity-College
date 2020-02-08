using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.Net;
using System.Net.Mail;

public partial class OtherPages_EditProfileS : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check Valid Login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 2 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            //Get student details..
            if (!IsPostBack)
            {
                string username = (string)Session["username"];

                var user = (from u in ue.Users
                            where u.username == username && u.Roles.rid == 2
                            select u).FirstOrDefault();

                if (username != null)
                {
                    txtName.Text = user.uFullname;
                    txtEmail.Text = user.uemail;
                    txtContact.Text = user.uContact.ToString();
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Profile for user is updated..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string username = (string)Session["username"];

            var user = (from u in ue.Users
                        where u.username == username
                        select u).FirstOrDefault();

            if (user != null)
            {
                user.uFullname = txtName.Text;
                user.uemail = txtEmail.Text;
                user.uContact = Convert.ToInt64(txtContact.Text);
                ue.SaveChanges();

                //Send Mail

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mitesh.vora@webaccess.co.in", "Admin,UnityCollege");
                mail.To.Add("mitesh.vora@webaccess.co.in");

                //mail.To.Add(txtEmail.Text); //Actual Email Id to be sent
                mail.Subject = "Modified Details of your account in Unity College Website";
                mail.Body = "Hello " + username + ",\n\nThis is a system generated message from Unity College.\n\nYou have modified your details in Unity College Website.\n\nKindly find the details below:\nName:" + txtName.Text + "\nEmail:" + txtEmail.Text + "\nMobile Number:" + txtContact.Text + "\n\nThank You,\nAdmin,UnityCollege";

                SmtpClient smtp = new SmtpClient("webaccess.co.in", 25);
                //smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("mitesh.vora@webaccess.co.in", "musu30in");
                smtp.Timeout = 20000;
                //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);

                lblMsg.Text = "Success!!!Details Updated!";

                //txtContact.Text = txtEmail.Text = txtName.Text = "";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentHome.aspx");
    }
}
