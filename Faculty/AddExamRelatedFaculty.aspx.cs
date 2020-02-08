using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.IO;
using System.Configuration;

public partial class OtherPages_AddExamRelated : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            //Checks valid login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 3 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            if (!IsPostBack)
            {
                ddlOption.Items.Clear();
                ddlOption.Items.Add("--Select--");
                ddlOption.Items.Add("Exam Schedule");
                ddlOption.Items.Add("Exam Result");

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                ddlSem.Items.Clear();
                ddlSem.Items.Add("--Select--");

                ddlValid.SelectedIndex = 0;

                var course = (from c in ue.Courses
                              where c.cvalid == true
                              select c).ToList();
                if (course.Count != 0)
                {
                    foreach (var data in course)
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
    /// Populates list of semesters in selected course
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
                for (int i = 1; i <= course.csemesters; i++)
                    ddlSem.Items.Add(i.ToString());
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Saves exam schedule/result on Submit
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

            var examDetail = (from edt in ue.ExamDetailType
                              where edt.edtname == ddlOption.Text
                              select edt).FirstOrDefault();
            //If semester not selected..
            int sem = 0;
            if (ddlSem.SelectedIndex != 0)
                sem = Convert.ToInt32(ddlSem.Text);

            var examRelated = (from c in ue.Courses
                               join er in ue.ExamRelated
                               on c.cid equals er.Courses.cid
                               join edt in ue.ExamDetailType
                               on er.ExamDetailType.edtid equals edt.edtid
                               where edt.edtname == ddlOption.Text && c.cname == ddlCourse.Text && er.ersem == sem
                               select er).FirstOrDefault();

            // Read the file and convert it to Byte Array
            string filePath = FileUpload1.PostedFile.FileName;
            string fileName = Path.GetFileName(filePath);
            string ext = Path.GetExtension(fileName);
            string contentType = String.Empty;

            //Key for Folder Name from Web.config..
            var examRelatedPath = ConfigurationManager.AppSettings["ExamRelatedPath"];

            // Specify the path to save the uploaded file to.
            string savePath = Server.MapPath("~\\" + examRelatedPath + "\\");


            // Create the path and file name to check for duplicates.
            string pathToCheck = savePath + fileName;

            //Set the contenttype based on File Extension
            switch (ext)
            {
                case ".pdf":
                    contentType = "application/pdf";
                    break;
            }

            if (ddlOption.SelectedIndex != 0)
            {
                if (ddlCourse.SelectedIndex != 0)
                {
                    if (ddlSem.SelectedIndex != 0)
                    {
                        //To avoid duplicate records of exam schedule/result
                        if (examRelated == null)
                        {
                            //Check if file is pdf..
                            if (contentType != String.Empty)
                            {
                                //Stream fs = FileUpload1.PostedFile.InputStream;
                                //BinaryReader br = new BinaryReader(fs);
                                //Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                ExamRelated newExamRelated = new ExamRelated();
                                newExamRelated.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", course.cid);
                                newExamRelated.ersem = Convert.ToInt32(ddlSem.Text);
                                FileUpload1.SaveAs(savePath + fileName);
                                newExamRelated.erdesc = fileName;
                                //er.erfile = bytes;
                                newExamRelated.ExamDetailTypeReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.ExamDetailType", "edtid", examDetail.edtid);
                                if (ddlValid.SelectedIndex == 0)
                                    newExamRelated.ervalid = true;
                                else
                                    newExamRelated.ervalid = false;
                                ue.AddToExamRelated(newExamRelated);
                                ue.SaveChanges();

                                lblMsg.Text = "Success!!!Record Saved!";

                                ddlCourse.SelectedIndex = ddlOption.SelectedIndex = ddlSem.SelectedIndex = ddlValid.SelectedIndex = 0;
                            }
                            else
                                lblMsg.Text = "File format not recognised. Upload PDF formats!";
                        }
                        else
                            lblMsg.Text = "Record already exist!!!Kindly visit Edit Exam Schedule/Result Module to update data!";
                    }
                    else
                        lblMsg.Text = "No Semester selected!";
                }
                else
                    lblMsg.Text = "No course selected!";
            }
            else
                lblMsg.Text = "Option must be selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
