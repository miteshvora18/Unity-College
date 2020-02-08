using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_SubjectDirectory : System.Web.UI.Page
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
                gvSubjectDirectory.DataSource = "";
                gvSubjectDirectory.DataBind();

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
    protected void gvSubjectDirectory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int subject = Convert.ToInt32(e.CommandArgument);
        Session["subject"] = subject;

        Response.Redirect("EditSubject.aspx");
    }
    /// <summary>
    /// Gets semester list for selected course..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                var subjectList = (from u in ue.Users
                                   join sf in ue.SubjectFaculty 
                                   on u.uid equals sf.Users.uid
                                   join s in ue.Subjects
                                   on sf.Subjects.sid equals s.sid
                                   where s.Courses.cid == course.cid
                                   orderby s.ssem
                                   select s.ssem).Distinct().ToList();

                if (subjectList.Count != 0)
                {
                    foreach (var data in subjectList)
                        ddlSem.Items.Add(data.ToString());
                }
                else
                    lblMsg.Text = "No subject available for current selection!";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Gets list of subjects for selected course and semester..
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
                    //Initialize semester values to avoid null reference exception..
                    int sem = 0;
                    if (ddlSem.SelectedIndex != 0)
                        sem = Convert.ToInt32(ddlSem.Text);

                    var subjectList = (from u in ue.Users
                                       join sf in ue.SubjectFaculty
                                       on u.uid equals sf.Users.uid
                                       join s in ue.Subjects
                                       on sf.Subjects.sid equals s.sid
                                       join c in ue.Courses
                                       on s.Courses.cid equals c.cid
                                       where c.cname == ddlCourse.Text && s.ssem == sem 
                                       select new { SubjectId=s.sid,SubjectName = s.sname,Course=c.cname,
                                       Semester=s.ssem, Faculty = u.uFullname,
                                       Valid = s.svalid }).ToList();

                    if (subjectList.Count != 0)
                    {
                        //var test = subjectlist.Select(u => new { Name = u.Name, Username = u.Username, Course = u.Course, AdmissionDate = u.AdmissionDate.ToString("MMM. dd yyyy"), EndDate = u.EndDate.ToString("MMM. dd yyyy"), CurrentSemester = u.CurrentSemester, Email = u.Email, ContactNumber = u.ContactNumber, GS = u.GS, Valid = u.Valid });
                        gvSubjectDirectory.DataSource = subjectList;
                        gvSubjectDirectory.DataBind();
                    }
                    else
                    {
                        gvSubjectDirectory.DataSource = "";
                        gvSubjectDirectory.DataBind();
                        lblMsg.Text = "No subject available for given selection!";
                    }
                }
                else
                {
                    gvSubjectDirectory.DataSource = "";
                    gvSubjectDirectory.DataBind();
                    lblMsg.Text = "No semester selected!";
                }
            }
            else
            {
                gvSubjectDirectory.DataSource = "";
                gvSubjectDirectory.DataBind();
                lblMsg.Text = "No course selected!";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
