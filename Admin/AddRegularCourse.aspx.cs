using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_AddRegularCourse : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                ddlDuration.Items.Clear();
                ddlDuration.Items.Add("--Select--");
                for (int i = 1; i < 7; i++)
                    ddlDuration.Items.Add(i.ToString());

                ddlStartYear.Items.Clear();
                ddlStartYear.Items.Add("--Select--");
                for (int i = 1989; i <= DateTime.Now.Year; i++)
                    ddlStartYear.Items.Add(i.ToString());
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }

    }
    /// <summary>
    /// Adds Regular Course on Submit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var courseExists = (from c in ue.Courses
                                where c.cname == txtCourseName.Text
                                select c).FirstOrDefault();

            if (ddlDuration.SelectedIndex != 0)
            {
                if (ddlStartYear.SelectedIndex != 0)
                {
                    //Check if course already exists..
                    if (courseExists == null)
                    {
                        unitycollegeModel.Courses newCourse = new unitycollegeModel.Courses();
                        newCourse.cname = txtCourseName.Text;
                        newCourse.cduration = Convert.ToInt32(ddlDuration.Text);
                        newCourse.cdescription = txtDesc.Text;
                        //c.cstartyear = DateTime.Parse(ddlStartYear.Text);
                        DateTime date = new DateTime(Convert.ToInt32(ddlStartYear.Text), 1, 1);
                        newCourse.cstartyear = date;
                        if (txtAccreditation.Text != "")
                            newCourse.caccreditation = txtAccreditation.Text;
                        if (ddlValid.SelectedIndex == 0)
                            newCourse.cvalid = true;
                        else
                            newCourse.cvalid = false;
                        ue.AddToCourses(newCourse);
                        ue.SaveChanges();

                        lblMsg.Text = "Success!!!Record Saved!";
                        txtAccreditation.Text = txtCourseName.Text = txtDesc.Text = "";
                        ddlDuration.SelectedIndex = ddlStartYear.SelectedIndex = ddlValid.SelectedIndex = 0;
                    }
                    else
                        lblMsg.Text = "Course already exists!";
                }
                else
                    lblMsg.Text = "Start Year is required!";
            }
            else
                lblMsg.Text = "Course Duration is required!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }
}
