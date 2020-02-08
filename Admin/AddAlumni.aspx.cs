using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_AddAlumni : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            //Check Valid Login..if invalid login credentials then redirect to login page
            var currentUser = (string)Session["username"];
            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            //Loads courses in Course Drop down list..
            if (!IsPostBack)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                var course = (from c in ue.Courses
                              select c).ToList();
                if (course.Count != 0)
                {
                    foreach (var data in course)
                        ddlCourse.Items.Add(data.cname);
                }
                ddlPassYear.Items.Clear();
                ddlPassYear.Items.Add("--Select--");
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// When user selects course,it populates list of passout year starting from the start year of selected course..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlPassYear.Items.Clear();
            ddlPassYear.Items.Add("--Select--");

            var course = (from c in ue.Courses
                          where c.cname == ddlCourse.Text
                          select c).FirstOrDefault();
            if (course != null)
            {
                for (int i = course.cstartyear.Year; i <= DateTime.Now.Year; i++)
                {
                    ddlPassYear.Items.Add(i.ToString());
                }
            }
            else
                lblMsg.Text = "Course is required!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Adds alumni details to database..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var email = (from a in ue.Alumni
                         where a.aemail == txtEmail.Text
                         select a.aemail).FirstOrDefault();

            var course = (from c in ue.Courses
                          where c.cname == ddlCourse.Text
                          select c).FirstOrDefault();

            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlPassYear.SelectedIndex != 0)
                {
                    //Checks if same email already exists in alumni record..
                    if (email == null)
                    {
                        Alumni newAlumni = new Alumni();
                        newAlumni.aname = txtName.Text;
                        newAlumni.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", course.cid);
                        newAlumni.aemail = txtEmail.Text;
                        newAlumni.acompany = txtCompany.Text;
                        newAlumni.apassyear = Convert.ToInt32(ddlPassYear.Text);
                        newAlumni.RolesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Roles", "rid", 4);
                        newAlumni.aworkprofile = txtWorkProfile.Text;
                        //if (ddlValid.SelectedIndex == 0)
                        newAlumni.avalid = true;
                        //else
                        //    a.avalid = false;
                        ue.AddToAlumni(newAlumni);
                        ue.SaveChanges();
                        lblMsg.Text = "Success!!!Record Saved Successfully!";

                        txtName.Text = txtEmail.Text = txtCompany.Text = txtWorkProfile.Text = "";
                        ddlCourse.SelectedIndex = ddlPassYear.SelectedIndex = 0;
                    }
                    else
                        lblMsg.Text = "Email already exists!";
                }
                else
                    lblMsg.Text = "Passout Year is required!";
            }
            else
                lblMsg.Text = "Course is required!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = e1.Message;
        }
    }
}
