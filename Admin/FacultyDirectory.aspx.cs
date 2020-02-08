using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_FacultyDirectory : System.Web.UI.Page
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
                gvFacultyDirectory.DataSource = "";
                gvFacultyDirectory.DataBind();

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
    /// Faculty details sent to edit faculty page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvFacultyDirectory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string username = e.CommandArgument.ToString();
        Session["facultyusername"] = username;

        Response.Redirect("EditFaculty.aspx");
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
            if (ddlCourse.SelectedIndex != 0)
            {
                var facultyList = (from u in ue.Users
                                   join c in ue.Courses
                                   on u.Courses.cid equals c.cid
                                   where u.Roles.rid == 3 && c.cname == ddlCourse.Text
                                   select new { Name = u.uFullname, Username = u.username, Department = u.Courses.cname, Email = u.uemail, ContactNumber = u.uContact, Valid = u.uvalid }).ToList();

                //var test = studentlist.Select(u => new { Name = u.Name, Username = u.Username, Course = u.Course, AdmissionDate = u.AdmissionDate.ToString("MMM. dd yyyy"), EndDate = u.EndDate.ToString("MMM. dd yyyy"), CurrentSemester = u.CurrentSemester, Email = u.Email, ContactNumber = u.ContactNumber, GeneralSecretary = u.GeneralSecretary, ValidUser = u.ValidUser });
                if (facultyList.Count != 0)
                {
                    gvFacultyDirectory.DataSource = facultyList;
                    gvFacultyDirectory.DataBind();
                }
                else
                {
                    gvFacultyDirectory.DataSource = "";
                    gvFacultyDirectory.DataBind();
                    lblMsg.Text = "No faculty for this course!";
                }
            }
            else
            {
                gvFacultyDirectory.DataSource = "";
                gvFacultyDirectory.DataBind();
                lblMsg.Text = "No course selected!";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
