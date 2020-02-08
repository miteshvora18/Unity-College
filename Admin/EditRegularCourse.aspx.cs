using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_EditRegularCourse : System.Web.UI.Page
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

                ddlDuration.Items.Clear();
                ddlDuration.Items.Add("--Select--");

                ddlStartYear.Items.Clear();
                ddlStartYear.Items.Add("--Select--");

                var course = (from c in ue.Courses
                              select c).ToList();
                if (course != null)
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
    /// Populated list of years for course..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDuration.Items.Clear();
            ddlDuration.Items.Add("--Select--");

            ddlStartYear.Items.Clear();
            ddlStartYear.Items.Add("--Select--");

            txtDesc.Text = "";
            txtAccreditation.Text = "";

            ddlValid.SelectedIndex = 0;

            var course = (from c in ue.Courses
                          where c.cname == ddlCourse.Text
                          select c).FirstOrDefault();

            if (course != null)
            {
                for (int i = 1; i < 7; i++)
                    ddlDuration.Items.Add(i.ToString());
                ddlDuration.Text = course.cduration.ToString();

                int currentyear = Convert.ToInt32(DateTime.Now.Year);
                int startyear = Convert.ToInt32(course.cstartyear.Year);

                for (int i = 1989; i <= currentyear; i++)
                    ddlStartYear.Items.Add(i.ToString());
                ddlStartYear.Text = startyear.ToString();

                txtDesc.Text = course.cdescription;
                if (course.caccreditation != "")
                    txtAccreditation.Text = course.caccreditation;
                if (course.cvalid == true)
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
    /// <summary>
    /// Course details are updated on Submit
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
                if (ddlDuration.SelectedIndex != 0)
                {
                    if (ddlStartYear.SelectedIndex != 0)
                    {
                        course.cduration = Convert.ToInt32(ddlDuration.Text);
                        course.cdescription = txtDesc.Text;
                        //course.cstartyear = Convert.ToDateTime(ddlStartYear.Text);
                        DateTime date = new DateTime(Convert.ToInt32(ddlStartYear.Text), 1, 1);
                        course.cstartyear = date;
                        course.caccreditation = txtAccreditation.Text;
                        if (ddlValid.SelectedIndex == 0)
                            course.cvalid = true;
                        else
                            course.cvalid = false;
                        ue.SaveChanges();
                        lblMsg.Text = "Success!!!Course Details Updated!";

                        txtAccreditation.Text = txtDesc.Text = "";
                        ddlCourse.SelectedIndex = ddlValid.SelectedIndex = ddlDuration.SelectedIndex = ddlStartYear.SelectedIndex = 0;
                    }
                    else
                        lblMsg.Text = "Start Year is required!";
                }
                else
                    lblMsg.Text = "Duration is required";

            }
            else
                lblMsg.Text = "No course selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
