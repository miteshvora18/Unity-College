using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.IO;
using System.Configuration;

public partial class OtherPages_EditExamRelated : System.Web.UI.Page
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

                //var course = (from c in ue.Courses
                //              where c.cvalid == true
                //              select c).ToList();
                //if (course != null)
                //{
                //    foreach (var data in course)
                //        ddlCourse.Items.Add(data.cname);
                //}
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Gets list of courses that have exam schedule/result
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add("--Select--");

            ddlSem.Items.Clear();
            ddlSem.Items.Add("--Select--");

            ddlValid.SelectedIndex = 0;

            var option = (from edt in ue.ExamDetailType
                          join er in ue.ExamRelated
                          on edt.edtid equals er.ExamDetailType.edtid
                          join c in ue.Courses
                          on er.Courses.cid equals c.cid
                          where edt.edtname == ddlOption.Text
                          select c).Distinct().ToList();
            if (option.Count != 0)
            {
                foreach (var data in option)
                    ddlCourse.Items.Add(data.cname);
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
        try
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add("--Select--");

            ddlValid.SelectedIndex = 0;

            var course = (from c in ue.Courses
                          join er in ue.ExamRelated
                          on c.cid equals er.Courses.cid
                          join edt in ue.ExamDetailType
                          on er.ExamDetailType.edtid equals edt.edtid
                          where c.cvalid == true && edt.edtname == ddlOption.Text && c.cname == ddlCourse.Text
                          select er).ToList();
            if (course.Count != 0)
            {
                foreach (var data in course)
                    ddlSem.Items.Add(data.ersem.ToString());
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }

    }
    /// <summary>
    /// Updates changes and saves for that course
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
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
                        //Check if file is pdf..
                        if (contentType != String.Empty)
                        {
                            File.Delete(savePath + examRelated.erdesc);
                            FileUpload1.SaveAs(savePath + fileName);
                            examRelated.erdesc = fileName;
                            if (ddlValid.SelectedIndex == 0)
                                examRelated.ervalid = true;
                            else
                                examRelated.ervalid = false;

                            //examrelated.erfile = bytes;
                            ue.SaveChanges();

                            lblMsg.Text = "Success!!!Record Updated!";
                            ddlValid.SelectedIndex = ddlSem.SelectedIndex = ddlOption.SelectedIndex = ddlCourse.SelectedIndex = 0;
                        }
                        else
                            lblMsg.Text = "File format not recognised. Upload PDF formats!";
                    }
                    else
                        lblMsg.Text = "No Semester selected!";
                }
                else
                    lblMsg.Text = "No course selected!";
            }
            else
                lblMsg.Text = "No option selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Loads whether course is valid or invalid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlValid.SelectedIndex = 0;

            int sem = Convert.ToInt32(ddlSem.Text);

            var valid = (from c in ue.Courses
                         join er in ue.ExamRelated
                         on c.cid equals er.Courses.cid
                         join edt in ue.ExamDetailType
                         on er.ExamDetailType.edtid equals edt.edtid
                         where c.cvalid == true && edt.edtname == ddlOption.Text && c.cname == ddlCourse.Text && er.ersem == sem
                         select er).FirstOrDefault();

            if (valid != null)
            {
                if (valid.ervalid == true)
                    ddlValid.SelectedIndex = 0;
                else
                    ddlValid.SelectedIndex = 1;
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
