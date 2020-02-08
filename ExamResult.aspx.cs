using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.IO;
using System.Configuration;

public partial class ExamSchedule : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Adding list of valid courses in drop down list..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                ddlSem.Items.Clear();
                ddlSem.Items.Add("--Select--");

                var course = (from c in ue.Courses
                              where c.cvalid == true
                              select c).ToList();

                foreach (var data in course)
                    ddlCourse.Items.Add(data.cname);
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates list of semesters for selected course.. 
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
                          join er in ue.ExamRelated
                          on c.cid equals er.Courses.cid
                          join edt in ue.ExamDetailType
                          on er.ExamDetailType.edtid equals edt.edtid
                          where c.cname == ddlCourse.Text && edt.edtname == "Exam Result" && er.ervalid == true
                          select er.ersem).Distinct().ToList();

            if (course.Count != 0)
            {
                foreach (var data in course)
                    ddlSem.Items.Add(data.ToString());

            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Displays exam result for selected course and semester on Submit..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //string savePath = "ExamRelated/";
            int sem = 0;
            if (ddlSem.SelectedIndex != 0)
                sem = Convert.ToInt32(ddlSem.Text);

            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlSem.SelectedIndex != 0)
                {
                    var currentSemester = (from er in ue.ExamRelated
                                           join edt in ue.ExamDetailType
                                           on er.ExamDetailType.edtid equals edt.edtid
                                           join c in ue.Courses
                                           on er.Courses.cid equals c.cid
                                           where c.cname == ddlCourse.Text && er.ersem == sem && edt.edtname == "Exam Result"
                                           select er).FirstOrDefault();

                    if (currentSemester != null)
                    {
                        //Response.Redirect("ExamRelated/"+cursem.erdesc);

                        //Key for Folder Name from Web.config..
                        var examRelatedPath = ConfigurationManager.AppSettings["ExamRelatedPath"];
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + examRelatedPath + "/" + currentSemester.erdesc + "','_newtab');", true);
                    }
                }
                else
                    lblMsg.Text = "No semester selected!";
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
