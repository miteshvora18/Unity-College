using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_EditSubject : System.Web.UI.Page
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

            //Gets details of selected subject..
            if (!IsPostBack)
            {
                var subjectId = (int)Session["subject"];

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

                var subject = (from s in ue.Subjects
                               join sf in ue.SubjectFaculty
                               on s.sid equals sf.Subjects.sid
                               join c in ue.Courses
                               on s.Courses.cid equals c.cid
                               join u in ue.Users
                               on sf.Users.uid equals u.uid
                               where s.sid == subjectId
                               select new { s, c, u }).FirstOrDefault();
                if (subject != null)
                {
                    txtSubName.Text = subject.s.sname;
                    ddlCourse.Text = subject.c.cname;

                    var courseSemester = (from c in ue.Courses
                                          where c.cname == ddlCourse.Text
                                          select c).FirstOrDefault();

                    if (courseSemester != null)
                    {
                        for (int i = 1; i <= courseSemester.csemesters; i++)
                            ddlSem.Items.Add(i.ToString());
                    }

                    ddlSem.Text = subject.s.ssem.ToString();
                    ddlFaculty.Text = subject.u.username;
                    txtDept.Text = subject.u.Courses.cname;
                    if (subject.s.svalid == true)
                        ddlValid.SelectedIndex = 0;
                    else
                        ddlValid.SelectedIndex = 1;
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }

    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtDept.Text = "";

            var subject = (from u in ue.Users
                           join c in ue.Courses
                           on u.Courses.cid equals c.cid
                           where u.username == ddlFaculty.Text
                           select c).FirstOrDefault();
            if (subject != null)
            {
                txtDept.Text = subject.cname;
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    //protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtDept.Text = "";
    //    ddlSem.SelectedIndex = ddlFaculty.SelectedIndex = ddlValid.SelectedIndex = 0;
    //    var getsubsem=(from s in ue.Subjects
    //                   join u in ue.Users
    //                   on s.Users.uid equals u.uid
    //                  where s.sname==ddlSubject.Text
    //                  select new {s,u}).FirstOrDefault();
    //    if (getsubsem != null)
    //    {
    //        var facultydept = (from c in ue.Courses
    //                           join u in ue.Users
    //                           on c.cid equals u.Courses.cid
    //                           where u.username == getsubsem.u.username
    //                           select c.cname).FirstOrDefault();
        
    //        ddlSem.Text = getsubsem.s.ssem.ToString();
    //        ddlFaculty.SelectedValue = getsubsem.u.username;
    //        txtDept.Text = facultydept;
    //        if(getsubsem.s.svalid==true)
    //            ddlValid.SelectedIndex=0;
    //        else
    //            ddlValid.SelectedIndex=1;
    //    }   
    //}

    /// <summary>
    /// Subject details are updated on Submit..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var getSubject = (from s in ue.Subjects
                              join c in ue.Courses
                              on s.Courses.cid equals c.cid
                              where s.sname == txtSubName.Text && c.cname == ddlCourse.Text
                              select s).FirstOrDefault();         

            var faculty = (from u in ue.Users
                           where u.username == ddlFaculty.Text
                           select u).FirstOrDefault();

            var subjectId = (int)Session["subject"];

            var subjectFaculty = (from sf in ue.SubjectFaculty
                                  where sf.Subjects.sid == subjectId
                                  orderby sf.sfstartdate descending
                                  select sf.Users.uid).FirstOrDefault();

            var subjectsFaculty = (from sf in ue.SubjectFaculty
                                  where sf.Subjects.sid == subjectId
                                  orderby sf.sfstartdate descending
                                  select sf).FirstOrDefault();
           
            //var courseexistsinsub = (from s in ue.Subjects
            //              join c in ue.Courses
            //              on s.Courses.cid equals c.cid
            //              where c.cname == ddlCourse.Text && s.sname == txtSubName.Text && s.sid != subjectid
            //              select s).FirstOrDefault();

            var course = (from c in ue.Courses
                          where c.cname == ddlCourse.Text
                          select c).FirstOrDefault();

            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlFaculty.SelectedIndex != 0)
                {
                    if (ddlSem.SelectedIndex != 0)
                    {
                        //if (courseexistsinsub == null)
                        //{

                        getSubject.ssem = Convert.ToInt32(ddlSem.Text);
                        if (subjectFaculty != faculty.uid)
                        {
                            subjectsFaculty.sfvalid = false;
                            ue.SaveChanges();
                            SubjectFaculty updatedFaculty = new SubjectFaculty();
                            updatedFaculty.SubjectsReference.EntityKey=new System.Data.EntityKey("unitycollegeEntities1.Subjects","sid",subjectId);
                            updatedFaculty.UsersReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Users", "uid", faculty.uid);
                            updatedFaculty.sfstartdate = DateTime.Now.Date;
                            updatedFaculty.sfvalid = true;
                            ue.AddToSubjectFaculty(updatedFaculty);
                            ue.SaveChanges();
                        }
                        //getSubject.UsersReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Users", "uid", faculty.uid);
                        if (ddlValid.SelectedIndex == 0)
                        {
                            getSubject.svalid = true;

                            var checkSubjectFaculty = (from sf in ue.SubjectFaculty
                                                       where sf.Subjects.sid == subjectId
                                                       orderby sf.sfstartdate descending
                                                       select sf.Users.uid).FirstOrDefault();

                            var checkSubjectsFaculty = (from sf in ue.SubjectFaculty
                                                       where sf.Subjects.sid == subjectId
                                                       orderby sf.sfstartdate descending
                                                       select sf).FirstOrDefault();

                            var getFaculty = (from u in ue.Users
                                              where u.uid == checkSubjectFaculty
                                              select u).FirstOrDefault();

                            if (getFaculty.uvalid == true)
                            {
                                checkSubjectsFaculty.sfvalid = true;
                            }
                        }
                        else
                        {
                            getSubject.svalid = false;

                            var makeSubjectFacultyFalse = (from sf in ue.SubjectFaculty
                                                           where sf.Subjects.sid == subjectId
                                                           select sf).ToList();
                            if(makeSubjectFacultyFalse.Count!=0)
                            {
                                foreach (var data in makeSubjectFacultyFalse)
                                    data.sfvalid = false;
                            }
                        }
                        ue.SaveChanges();

                        lblMsg.Text = "Success!!!Record Updated!";
                        //txtDept.Text = "";
                        //ddlValid.SelectedIndex = ddlFaculty.SelectedIndex = ddlCourse.SelectedIndex = ddlSem.SelectedIndex = 0;
                        //}
                        //else
                        //    lblMsg.Text = "Subject already exists in selected course";
                    }
                    else
                        lblMsg.Text = "No semester selected!";
                }
                else
                    lblMsg.Text = "No Faculty selected for Subject!";
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
