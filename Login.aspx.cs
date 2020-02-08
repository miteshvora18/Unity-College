using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class Login : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblError.Text = "";

            if (!IsPostBack)
            {
                //Removes user session on logout..
                string a = Request.QueryString["a"];
                if (a == "logout")
                {
                    Session.Remove("username");
                    lblError.Text = "You are now logged out!";
                    //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //HttpContext.Current.Response.Cache.SetNoServerCaching();
                    //HttpContext.Current.Response.Cache.SetNoStore();
                    //Session.Abandon();
                    //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                }

                //On invalid access to specified page it will be redirected to login..with message
                string error = Request.QueryString["error"];

                if (error == "invalid")
                {
                    lblError.Text = "Login Again!!!";
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Sends user to their authorised page based on login details..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var getuser = (from u in ue.Users
                           join r in ue.Roles
                           on u.Roles.rid equals r.rid
                           where u.username == txtUsername.Text && u.upassword == txtPassword.Text
                           select new { u, r }).FirstOrDefault();

            if (getuser != null)
            {
                if (getuser.u.uvalid == true)
                {
                    if (getuser.r.rname == "Admin")
                    {
                        Session["username"] = txtUsername.Text;
                        Response.Redirect("Admin/AdminHome.aspx");
                    }
                    if (getuser.r.rname == "Student")
                    {
                        Session["username"] = txtUsername.Text;
                        Response.Redirect("Student/StudentHome.aspx");
                    }
                    if (getuser.r.rname == "Faculty")
                    {
                        Session["username"] = txtUsername.Text;
                        Response.Redirect("Faculty/FacultyHome.aspx");
                    }
                }
                else
                    lblMsg.Text = "You are not authorized to login!!";
            }
            else
                lblMsg.Text = "Invalid Username and/or Password";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
