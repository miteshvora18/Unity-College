using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.IO;
using System.Configuration;

public partial class OnlineLibrary : System.Web.UI.Page
{
    
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Loads available and valid courses in drop down list..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblHeader.Visible = false;
            lblMsg.Text = "";
            lblData.Text = "";
            if (!IsPostBack)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                ddlSem.Items.Clear();
                ddlSem.Items.Add("--Select--");

                ddlSubject.Items.Clear();
                ddlSubject.Items.Add("--Select--");

                var course = (from c in ue.Courses
                              where c.cvalid == true
                              select c).ToList();

                if (course.Count != 0)
                {
                    foreach (var data in course)
                        ddlCourse.Items.Add(data.cname);
                }
                else
                    lblMsg.Text = "No courses available";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Lists available library links for that course and semester..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 1;
            lblData.Text = "";
            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlSem.SelectedIndex != 0)
                {
                    if (ddlSubject.SelectedIndex != 0)
                    {
                        var selectedSemester = Convert.ToInt32(ddlSem.Text);

                        var subject = (from c in ue.Courses
                                       join ol in ue.OnlineLibrary
                                       on c.cid equals ol.Courses.cid
                                       join s in ue.Subjects
                                       on ol.Subjects.sid equals s.sid
                                       where c.cname == ddlCourse.Text && ol.csem == selectedSemester && s.sname == ddlSubject.Text && ol.olvalid == true
                                       select ol).ToList();

                        if (subject.Count != 0)
                        {
                            lblHeader.Visible = true;
                            foreach (var data in subject)
                            {
                                var libraryPath = ConfigurationManager.AppSettings["OnlineLibraryPath"];
                                lblData.Text += "<a href='" + libraryPath + "/" + data.oldesc + "' target='_blank'>" + i + ". " + data.oltname + "</a><br/>";
                                i++;
                                string path = Path.GetDirectoryName(data.oldesc);
                            }
                        }
                        else
                            lblMsg.Text = "No books available for " + ddlCourse.Text + "!";
                    }
                    else
                        lblMsg.Text = "No Subject selected!";
                }
                else
                    lblMsg.Text = "No Semester selected!";
            }
            else
                lblMsg.Text = "No Course selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates list of semesters that contains online books..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add("--Select--");

            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("--Select--");

            var course = (from c in ue.Courses
                          join ol in ue.OnlineLibrary
                          on c.cid equals ol.Courses.cid
                          join s in ue.Subjects
                          on ol.Subjects.sid equals s.sid
                          where c.cname == ddlCourse.Text && ol.olvalid == true && s.svalid == true
                          select ol.csem).Distinct().ToList();

            if (course.Count != 0)
            {
                foreach (var data in course)
                    ddlSem.Items.Add(data.ToString());
            }
            else
                lblMsg.Text = "No links available for selected course";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates subjects that contain online books for selected course and semester..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("--Select--");

            var selectedSemester = 0;
            if (ddlSem.SelectedIndex != 0)
                selectedSemester = Convert.ToInt32(ddlSem.Text);

            var sem = (from c in ue.Courses
                       join ol in ue.OnlineLibrary
                       on c.cid equals ol.Courses.cid
                       join s in ue.Subjects
                       on ol.Subjects.sid equals s.sid
                       where c.cname == ddlCourse.Text && ol.csem == selectedSemester && ol.olvalid == true && s.svalid == true
                       select s.sname).Distinct().ToList();

            if (sem.Count != 0)
            {
                foreach (var data in sem)
                    ddlSubject.Items.Add(data);
            }
            else
                lblMsg.Text = "No links available for selected course and semester";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    //protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlHeader.Items.Clear();
    //    ddlHeader.Items.Add("--Select--");

    //    var cursem = 0;
    //    if (ddlSem.SelectedIndex != 0)
    //        cursem = Convert.ToInt32(ddlSem.Text);

    //    var subject = (from c in ue.Courses
    //                   join ol in ue.OnlineLibrary
    //                   on c.cid equals ol.Courses.cid
    //                   join s in ue.Subjects
    //                   on ol.Subjects.sid equals s.sid
    //                   where c.cname == ddlCourse.Text && ol.csem == cursem && s.sname==ddlSubject.Text
    //                   select ol.oltname).ToList();

    //    if (subject.Count != 0)
    //    {
    //        foreach (var data in subject)
    //            ddlHeader.Items.Add(data);
    //    }
    //}
}
