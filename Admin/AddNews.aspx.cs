using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_AddNews : System.Web.UI.Page
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
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// News added on Submit and can be viewed on Home page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var existingNews = (from n in ue.News
                                where n.nheader == txtNewsHeader.Text.Trim()
                                select n).FirstOrDefault();
            //Check if news exists
            if (existingNews == null)
            {
                unitycollegeModel.News addNews = new unitycollegeModel.News();

                addNews.nheader = txtNewsHeader.Text;
                addNews.nshortdesc = txtShortDesc.Text;
                addNews.nlongdesc = txtLongDesc.Text;
                if (ddlValid.SelectedIndex == 0)
                    addNews.nvalid = true;
                else
                    addNews.nvalid = false;
                addNews.ndatetime = DateTime.Now;
                ue.AddToNews(addNews);
                ue.SaveChanges();

                lblMsg.Text = "Success!!!Record Saved!";
                txtLongDesc.Text = txtNewsHeader.Text = txtShortDesc.Text = "";
                ddlValid.SelectedIndex = 0;
            }
            else
                lblMsg.Text = "News header already exists!Try another one.";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }

}
