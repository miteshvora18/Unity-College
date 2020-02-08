using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_EditPlacement : System.Web.UI.Page
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
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                ddlCompanyName.Items.Clear();
                ddlCompanyName.Items.Add("--Select--");

                ddlWorkProfile.Items.Clear();
                ddlWorkProfile.Items.Add("--Select--");

                var placementCourseList = (from c in ue.Courses
                                           join pl in ue.PlacementsList
                                           on c.cid equals pl.Courses.cid
                                           join pt in ue.PlacementType
                                           on pl.PlacementType.ptid equals pt.ptid
                                           where pt.ptname == ddlType.Text
                                           select c).Distinct().ToList();


                if (placementCourseList != null)
                {
                    foreach (var data in placementCourseList)
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
    /// Existing Placement details are updated on Submit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var currentProfile = (from c in ue.Courses
                                  join pl in ue.PlacementsList
                                  on c.cid equals pl.Courses.cid
                                  join pt in ue.PlacementType
                                  on pl.PlacementType.ptid equals pt.ptid
                                  where c.cname == ddlCourse.Text && pt.ptname == ddlType.Text && pl.pcompanyname == ddlCompanyName.Text && pl.pjobtitle == ddlWorkProfile.Text
                                  select pl).FirstOrDefault();

            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlCompanyName.SelectedIndex != 0)
                {
                    if (ddlWorkProfile.SelectedIndex != 0)
                    {
                        currentProfile.pinterviewlocation = txtLocation.Text;
                        currentProfile.pdate = Convert.ToDateTime(txtDateTime.Text);
                        currentProfile.peligibility = txtEligibility.Text;
                        if (txtWebsite.Text != "")
                            currentProfile.pwebsite = txtWebsite.Text;
                        if (ddlValid.SelectedIndex == 0)
                            currentProfile.pvalid = true;
                        else
                            currentProfile.pvalid = false;
                        ue.SaveChanges();

                        lblMsg.Text = "Success!!!Record Updated!";

                        ddlCourse.SelectedIndex = ddlCompanyName.SelectedIndex = ddlWorkProfile.SelectedIndex = ddlValid.SelectedIndex = 0;
                        txtDateTime.Text = txtEligibility.Text = txtLocation.Text = txtWebsite.Text = "";

                    }
                    else
                        lblMsg.Text = "No Work Profile selected!";
                }
                else
                    lblMsg.Text = "No comapnty selected!";
            }
            else
                lblMsg.Text = "No course selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates course that exists for jobs/internship 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add("--Select--");

            ddlCompanyName.Items.Clear();
            ddlCompanyName.Items.Add("--Select--");

            ddlWorkProfile.Items.Clear();
            ddlWorkProfile.Items.Add("--Select--");

            txtDateTime.Text = txtEligibility.Text = txtLocation.Text = txtWebsite.Text = "";
            ddlValid.SelectedIndex = 0;

            var placementCourseList = (from c in ue.Courses
                                       join pl in ue.PlacementsList
                                       on c.cid equals pl.Courses.cid
                                       join pt in ue.PlacementType
                                       on pl.PlacementType.ptid equals pt.ptid
                                       where pt.ptname == ddlType.Text
                                       select c).Distinct().ToList();
            if (placementCourseList != null)
            {
                foreach (var data in placementCourseList)
                    ddlCourse.Items.Add(data.cname);
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates semesters for selected Course
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCompanyName.Items.Clear();
            ddlCompanyName.Items.Add("--Select--");

            ddlWorkProfile.Items.Clear();
            ddlWorkProfile.Items.Add("--Select--");
            txtDateTime.Text = txtEligibility.Text = txtLocation.Text = txtWebsite.Text = "";
            ddlValid.SelectedIndex = 0;

            var companyList = (from c in ue.Courses
                               join pl in ue.PlacementsList
                               on c.cid equals pl.Courses.cid
                               join pt in ue.PlacementType
                               on pl.PlacementType.ptid equals pt.ptid
                               where c.cname == ddlCourse.Text && pt.ptname == ddlType.Text
                               select pl).ToList();

            if (companyList != null)
            {
                foreach (var data in companyList)
                    ddlCompanyName.Items.Add(data.pcompanyname);
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates work profile for selected company
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlWorkProfile.Items.Clear();
            ddlWorkProfile.Items.Add("--Select--");
            txtDateTime.Text = txtEligibility.Text = txtLocation.Text = txtWebsite.Text = "";
            ddlValid.SelectedIndex = 0;

            var workProfile = (from c in ue.Courses
                               join pl in ue.PlacementsList
                               on c.cid equals pl.Courses.cid
                               join pt in ue.PlacementType
                               on pl.PlacementType.ptid equals pt.ptid
                               where c.cname == ddlCourse.Text && pt.ptname == ddlType.Text && pl.pcompanyname == ddlCompanyName.Text
                               select pl).ToList();

            if (workProfile != null)
            {
                foreach (var data in workProfile)
                    ddlWorkProfile.Items.Add(data.pjobtitle);

            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates details of selected work profile
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlWorkProfile_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtDateTime.Text = txtEligibility.Text = txtLocation.Text = txtWebsite.Text = "";
            ddlValid.SelectedIndex = 0;

            var currentProfile = (from c in ue.Courses
                                  join pl in ue.PlacementsList
                                  on c.cid equals pl.Courses.cid
                                  join pt in ue.PlacementType
                                  on pl.PlacementType.ptid equals pt.ptid
                                  where c.cname == ddlCourse.Text && pt.ptname == ddlType.Text && pl.pcompanyname == ddlCompanyName.Text && pl.pjobtitle == ddlWorkProfile.Text
                                  select pl).FirstOrDefault();

            if (currentProfile != null)
            {
                txtDateTime.Text = currentProfile.pdate.ToString("MM/dd/yyyy HH:mm");
                txtLocation.Text = currentProfile.pinterviewlocation;
                txtEligibility.Text = currentProfile.peligibility;
                if (currentProfile.pwebsite != null)
                    txtWebsite.Text = currentProfile.pwebsite;
                if (currentProfile.pvalid == true)
                    ddlValid.SelectedIndex = 0;
                else
                    ddlValid.SelectedIndex = 1;
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
