using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_AddPlacement : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                var course = (from c in ue.Courses
                              where c.cvalid == true
                              select c).ToList();
                if (course.Count != 0)
                {
                    foreach (var data in course)
                        ddlCourse.Items.Add(data.cname);
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Adds new job/internship on Submit 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var course = (from c in ue.Courses
                          where c.cname == ddlCourse.Text
                          select c).FirstOrDefault();

            if (ddlCourse.SelectedIndex != 0)
            {
                PlacementsList newPlacement = new PlacementsList();
                newPlacement.pcompanyname = txtCompanyName.Text;
                newPlacement.pdate = Convert.ToDateTime(txtDateTime.Text);
                newPlacement.pjobtitle = txtWorkProfile.Text;
                newPlacement.peligibility = txtEligibility.Text;
                newPlacement.pinterviewlocation = txtLocation.Text;

                if (ddlType.SelectedIndex == 0)
                    newPlacement.PlacementTypeReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.PlacementType", "ptid", 1);
                else
                    newPlacement.PlacementTypeReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.PlacementType", "ptid", 2);

                if (ddlValid.SelectedIndex == 0)
                    newPlacement.pvalid = true;
                else
                    newPlacement.pvalid = false;

                if (txtWebsite.Text != "")
                    newPlacement.pwebsite = txtWebsite.Text;

                newPlacement.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", course.cid);
                ue.AddToPlacementsList(newPlacement);
                ue.SaveChanges();

                lblMsg.Text = "Success!!!Record Saved!";

                txtCompanyName.Text = txtDateTime.Text = txtEligibility.Text = txtLocation.Text = txtWebsite.Text = txtWorkProfile.Text = "";
                ddlCourse.SelectedIndex = ddlType.SelectedIndex = ddlValid.SelectedIndex = 0;
            }
            else
                lblMsg.Text = "No course selected!";
        }
        catch(Exception e1)
        {
            lblMsg.Text="Error:"+e1.Message;
        }
    }
}
