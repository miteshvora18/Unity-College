using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_EditAlumni : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var user = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == user
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            lblMsg.Text = "";
            //Gets details of current alumni..
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

                //Session["alumniid"] = 2;
                var currentAlumni = Convert.ToInt32(Session["alumniid"]);

                var alumni = (from a in ue.Alumni
                              join c in ue.Courses
                              on a.Courses.cid equals c.cid
                              where a.aid == currentAlumni
                              select new { a, c }).FirstOrDefault();
                if (alumni != null)
                {
                    for (int i = alumni.c.cstartyear.Year; i < DateTime.Now.Year; i++)
                    {
                        ddlPassYear.Items.Add(i.ToString());
                    }

                    txtName.Text = alumni.a.aname;
                    txtEmail.Text = alumni.a.aemail;
                    txtCompany.Text = alumni.a.acompany;
                    txtWorkProfile.Text = alumni.a.aworkprofile;
                    ddlCourse.Text = alumni.c.cname;
                    //if (alumni.a.avalid == true)
                    //    ddlValid.SelectedIndex = 0;
                    //else
                    //    ddlValid.SelectedIndex = 1;
                    ddlPassYear.Text = alumni.a.apassyear.ToString();
                }
                else
                    lblMsg.Text = "Please visit alumni directory to select alumni!";

            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates semesters for that course
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var course = (from c in ue.Courses
                          where c.cname == ddlCourse.Text
                          select c).FirstOrDefault();

            ddlPassYear.Items.Clear();
            ddlPassYear.Items.Add("--Select--");

            if (course != null)
            {
                for (int i = course.cstartyear.Year; i < DateTime.Now.Year; i++)
                    ddlPassYear.Items.Add(i.ToString());
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Saves updated alumni details..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var currentAlumni = Convert.ToInt32(Session["alumniid"]);

            var course = (from c in ue.Courses
                          where c.cname == ddlCourse.Text
                          select c).FirstOrDefault();

            var email = (from a in ue.Alumni
                         where a.aemail == txtEmail.Text && a.aid != currentAlumni
                         select a).FirstOrDefault();

            var alumni = (from a in ue.Alumni
                          where a.aid == currentAlumni
                          select a).FirstOrDefault();

            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlPassYear.SelectedIndex != 0)
                {
                    if (email == null)
                    {
                        alumni.aname = txtName.Text;
                        alumni.aemail = txtEmail.Text;
                        alumni.acompany = txtCompany.Text;
                        alumni.apassyear = Convert.ToInt32(ddlPassYear.Text);
                        alumni.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", course.cid);
                        alumni.aworkprofile = txtWorkProfile.Text;
                        //if (ddlValid.SelectedIndex == 0)
                        //    alumni.avalid = true;
                        //else
                        //    alumni.avalid = false;
                        ue.SaveChanges();

                        lblMsg.Text = "Success!!!Record Updated Successfully!";
                    }
                    else
                        lblMsg.Text = "Email already exists!";
                }
                else
                    lblMsg.Text = "Passout Year is required!";
            }
            else
                lblMsg.Text = "No course selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }
}
