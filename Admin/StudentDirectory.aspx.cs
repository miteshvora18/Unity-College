using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_StudentDirectory : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check Valid Login..
            var curuser = (string)Session["username"];

            var checkuser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == curuser
                             select u).FirstOrDefault();
            if (checkuser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            lblMsg.Text = "";


            if (!IsPostBack)
            {
                gvStudentDirectory.DataSource = "";
                gvStudentDirectory.DataBind();

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                ddlSem.Items.Clear();
                ddlSem.Items.Add("--Select--");

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
    /// Sends user detail to edit page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvStudentDirectory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string username = e.CommandArgument.ToString();
        Session["studentusername"] = username;
        Response.Redirect("EditStudent.aspx");
    }
    
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add("--Select--");

            var course = (from c in ue.Courses
                          where c.cname == ddlCourse.Text
                          select c).FirstOrDefault();
            if (course != null)
            {
                var studentList = (from u in ue.Users
                                   join sc in ue.StudentCourse
                                   on u.uid equals sc.Users.uid
                                   where sc.Courses.cid == course.cid
                                   orderby sc.scCurrentsem
                                   select sc.scCurrentsem).Distinct().ToList();

                if (studentList.Count != 0)
                {
                    foreach (var data in studentList)
                        ddlSem.Items.Add(data.ToString());
                }
                else
                    lblMsg.Text = "No student available for current selection!";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Gets list of student in selected course and semester
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlSem.SelectedIndex != 0)
                {
                    int sem = 0;
                    if (ddlSem.SelectedIndex != 0)
                        sem = Convert.ToInt32(ddlSem.Text);

                    var studentList = (from u in ue.Users
                                       join sc in ue.StudentCourse
                                       on u.uid equals sc.Users.uid
                                       join c in ue.Courses
                                       on sc.Courses.cid equals c.cid
                                       where u.Roles.rid == 2 && c.cname == ddlCourse.Text && sc.scCurrentsem == sem
                                       select new { Name = u.uFullname, Username = u.username, Course = c.cname, AdmissionDate = sc.scStartdate, EndDate = sc.scEnddate, CurrentSemester = sc.scCurrentsem, Email = u.uemail, ContactNumber = u.uContact, GS = u.ugs, Valid = u.uvalid }).ToList();

                    if (studentList.Count != 0)
                    {
                        var test = studentList.Select(u => new { Name = u.Name, Username = u.Username, Course = u.Course, AdmissionDate = u.AdmissionDate.ToString("MMM. dd yyyy"), EndDate = u.EndDate.ToString("MMM. dd yyyy"), CurrentSemester = u.CurrentSemester, Email = u.Email, ContactNumber = u.ContactNumber, GS = u.GS, Valid = u.Valid });

                        gvStudentDirectory.DataSource = test;
                        gvStudentDirectory.DataBind();
                    }
                    else
                    {
                        gvStudentDirectory.DataSource = "";
                        gvStudentDirectory.DataBind();
                        lblMsg.Text = "No student available for given selection!";
                    }
                }
                else
                {
                    gvStudentDirectory.DataSource = "";
                    gvStudentDirectory.DataBind();
                    lblMsg.Text = "No semester selected!";
                }
            }
            else
            {
                gvStudentDirectory.DataSource = "";
                gvStudentDirectory.DataBind();
                lblMsg.Text = "No course selected!";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
