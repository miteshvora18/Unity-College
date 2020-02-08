using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_AddSubject : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Checks Valid Login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            if (!IsPostBack)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                ddlFaculty.Items.Clear();
                ddlFaculty.Items.Add("--Select--");

                ddlSem.Items.Clear();
                ddlSem.Items.Add("--Select--");

                var course = (from c in ue.Courses
                              where c.cvalid == true
                              select c).ToList();
                if (course.Count != 0)
                {
                    foreach (var data in course)
                        ddlCourse.Items.Add(data.cname);
                }
                else
                    lblMsg.Text = "No course available!";

                var faculty = (from u in ue.Users
                               join c in ue.Courses
                               on u.Courses.cid equals c.cid
                               where u.Roles.rid == 3 && u.uvalid == true && c.cvalid != false
                               select u).ToList();
                if (faculty.Count != 0)
                {
                    foreach (var data in faculty)
                        ddlFaculty.Items.Add(data.username);
                }
                else
                    lblMsg.Text = "No faculty available";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates list of semesters for that course
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add("--Select--");

        var sem = (from c in ue.Courses
                   where c.cname == ddlCourse.Text
                   select c.csemesters).FirstOrDefault();
        if (sem != null)
        {
            for (int i = 1; i <= sem; i++)
                ddlSem.Items.Add(i.ToString());
        }
     }
    /// <summary>
    /// Gets List of Faculty for selection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDept.Text="";

        var dept = (from u in ue.Users
                    join c in ue.Courses
                    on u.Courses.cid equals c.cid
                    where u.username == ddlFaculty.Text
                    select c).FirstOrDefault();
        
        if (dept != null)
        {
            txtDept.Text = dept.cname;
        }
    }
    /// <summary>
    /// Changes for Subject are saved on Submit
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

            var user = (from u in ue.Users
                        where u.username == ddlFaculty.Text
                        select u).FirstOrDefault();

            var subjectExists = (from c in ue.Courses
                               join s in ue.Subjects
                               on c.cid equals s.Courses.cid
                               where c.cname == ddlCourse.Text && s.sname == txtSubName.Text
                               select s).FirstOrDefault();


            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlFaculty.SelectedIndex != 0)
                {
                    if (ddlSem.SelectedIndex != 0)
                    {
                        //Check if subject exists for that selected course
                        if (subjectExists == null)
                        {
                            Subjects newSubject = new Subjects();
                            newSubject.sname = txtSubName.Text;
                            newSubject.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", course.cid);
                            newSubject.ssem = Convert.ToInt32(ddlSem.Text);
                            //newSubject.UsersReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Users", "uid", user.uid);
                            if (ddlValid.SelectedIndex == 0)
                                newSubject.svalid = true;
                            else
                                newSubject.svalid = false;
                            ue.AddToSubjects(newSubject);
                            ue.SaveChanges();

                            SubjectFaculty subjectFaculty = new SubjectFaculty();
                            subjectFaculty.SubjectsReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Subjects", "sid", newSubject.sid);
                            subjectFaculty.UsersReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Users", "uid", user.uid);
                            subjectFaculty.sfstartdate = DateTime.Now.Date;
                            subjectFaculty.sfvalid = true;
                            ue.AddToSubjectFaculty(subjectFaculty);
                            ue.SaveChanges();

                            lblMsg.Text = "Success!!!Record Saved!";
                            txtSubName.Text = txtDept.Text = "";
                            ddlCourse.SelectedIndex = ddlSem.SelectedIndex = ddlValid.SelectedIndex = ddlFaculty.SelectedIndex = 0;
                        }
                        else
                            lblMsg.Text = "Subject Name:" + txtSubName.Text + " already exists for " + ddlCourse.Text;
                    }
                    else
                        lblMsg.Text = "No semester selected!";
                }
                else
                    lblMsg.Text = "Faculty is required!";
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
