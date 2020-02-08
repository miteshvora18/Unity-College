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
                             where u.uvalid == true && u.Roles.rid == 2 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            lblFacultyHeader.Visible = false;
            lblMsg.Text = "";
            lblFaculty.Text = "";

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
    /// Gets list of faculty for selected course..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var courses = (from c in ue.Courses
                           where c.cvalid == true
                           select c).ToList();

            if (ddlCourse.SelectedIndex != 0)
            {
                var facultyList = (from u in ue.Users
                                   join c in ue.Courses
                                   on u.Courses.cid equals c.cid
                                   where u.uvalid == true && u.Roles.rid == 3 && c.cname == ddlCourse.Text
                                   select u).ToList();

                //On selection of faculty, sends user to message page..
                if (facultyList.Count != 0)
                {
                    lblFacultyHeader.Visible = true;
                    foreach (var data in facultyList)
                        lblFaculty.Text += "<li><div class='communicate'><a href='MessageStudent.aspx?a=1&facultyusername=" + data.username + "'>" + data.uFullname + "</a></div></li>";
                }
                else
                    lblMsg.Text = "No faculty available for this course!";
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
