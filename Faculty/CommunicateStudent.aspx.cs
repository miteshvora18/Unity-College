using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_CommunicateFaculty : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check Valid Login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 3 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            lblStudentHeader.Visible = false;
            lblStudent.Text = "";
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                var courses = (from c in ue.Courses
                               where c.cvalid == true
                               select c).ToList();

                if (courses.Count != 0)
                {
                    foreach (var data in courses)
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
    /// Gets list of student for selected course..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedIndex != 0)
            {
                var courses = (from c in ue.Courses
                               where c.cname == ddlCourse.Text
                               select c).FirstOrDefault();

                var facultyList = (from u in ue.Users
                                   join sc in ue.StudentCourse
                                   on u.uid equals sc.Users.uid
                                   where u.uvalid == true && u.Roles.rid == 2 && sc.Courses.cid == courses.cid
                                   select u).ToList();
                //On selection of student sends user to message page..
                if (facultyList.Count != 0)
                {
                    lblStudentHeader.Visible = true;
                    foreach (var data in facultyList)
                        lblStudent.Text += "<li><div class='communicate'><a href='MessageFaculty.aspx?a=1&studentusername=" + data.username + "'> " + data.uFullname + "</a></div></li>";
                }
                else
                    lblMsg.Text = "No student available for this course!";
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
