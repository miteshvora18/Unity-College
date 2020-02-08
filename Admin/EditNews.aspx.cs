using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_UpdateNews : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                ddlHeader.Items.Clear();
                ddlHeader.Items.Add("--Select--");

                var newsHeader = (from n in ue.News
                                  orderby n.ndatetime descending
                                  select n).ToList();
                if (newsHeader.Count != 0)
                {
                    foreach (var data in newsHeader)
                        ddlHeader.Items.Add(data.nheader);
                }
                else
                    lblMsg.Text = "No news available!";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    protected void ddlHeader_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtLongDesc.Text = txtShortDesc.Text = "";
            ddlValid.SelectedIndex = 0;

            var getNews = (from n in ue.News
                           where n.nheader == ddlHeader.Text
                           select n).FirstOrDefault();
            if (getNews != null)
            {
                txtLongDesc.Text = getNews.nlongdesc;
                txtShortDesc.Text = getNews.nshortdesc;
                ddlValid.Text = getNews.nvalid.ToString();
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// News is updated on Submit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var selectedNews = (from n in ue.News
                                where n.nheader == ddlHeader.Text
                                select n).FirstOrDefault();

            if (ddlHeader.SelectedIndex != 0)
            {
                selectedNews.nlongdesc = txtLongDesc.Text;
                selectedNews.nshortdesc = txtShortDesc.Text;
                if (ddlValid.SelectedIndex == 0)
                    selectedNews.nvalid = true;
                else
                    selectedNews.nvalid = false;
                ue.SaveChanges();

                lblMsg.Text = "Success!!!Record Updated!";
            }
            else
                lblMsg.Text = "No News Header selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }
}
