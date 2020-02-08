using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.IO;
using System.Configuration;

public partial class OtherPages_EditOnlineLibrary : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");

                ddlSubject.Items.Clear();
                ddlSubject.Items.Add("--Select--");

                ddlSem.Items.Clear();
                ddlSem.Items.Add("--Select--");

                ddlHeader.Items.Clear();
                ddlHeader.Items.Add("--Select--");

                ddlValid.SelectedIndex = 0;

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
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("--Select--");

            ddlSem.Items.Clear();
            ddlSem.Items.Add("--Select--");

            ddlHeader.Items.Clear();
            ddlHeader.Items.Add("--Select--");

            ddlValid.SelectedIndex = 0;

            var subject = (from s in ue.Subjects
                           join c in ue.Courses
                           on s.Courses.cid equals c.cid
                           join ol in ue.OnlineLibrary
                           on c.cid equals ol.Courses.cid
                           where c.cname == ddlCourse.Text
                           select s.sname).Distinct().ToList();

            //var subjects = subject.ToList();
            if (subject.Count != 0)
            {
                foreach (var data in subject)
                {
                    ddlSubject.Items.Add(data);
                    //ddlSem.Text = data.ol.csem.ToString();
                }
            }

            //var files = (from c in ue.Courses
            //             join ol in ue.OnlineLibrary
            //             on c.cid equals ol.Courses.cid
            //             where c.cname == ddlCourse.Text
            //             select ol.oltname).ToList();

            //foreach (var data in files)
            //    ddlHeader.Items.Add(data);


            //ddlHeader.SelectedIndex = ddlValid.SelectedIndex = ddlValid.SelectedIndex = 0;
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    protected void ddlHeader_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlValid.SelectedIndex = 0;

            //Initialize semester value 
            var sem = 0;
            if (ddlSem.SelectedIndex != 0)
                sem = Convert.ToInt32(ddlSem.Text);

            var selectedHeader = (from c in ue.Courses
                                  join ol in ue.OnlineLibrary
                                  on c.cid equals ol.Courses.cid
                                  join s in ue.Subjects
                                  on ol.Subjects.sid equals s.sid
                                  where c.cname == ddlCourse.Text && ol.csem == sem && s.sname == ddlSubject.Text && ol.oltname == ddlHeader.Text
                                  select ol).FirstOrDefault();

            if (selectedHeader != null)
            {
                if (selectedHeader.olvalid == true)
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

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlHeader.Items.Clear();
            ddlHeader.Items.Add("--Select--");

            ddlSem.Items.Clear();
            ddlSem.Items.Add("--Select--");

            var subjectSem = (from c in ue.Courses
                              join ol in ue.OnlineLibrary
                              on c.cid equals ol.Courses.cid
                              join s in ue.Subjects
                              on ol.Subjects.sid equals s.sid
                              where c.cname == ddlCourse.Text && s.sname == ddlSubject.Text
                              select ol.csem).FirstOrDefault();

            if (subjectSem != null)
            {
                var totalSem = (from c in ue.Courses
                                where c.cname == ddlCourse.Text
                                select c).FirstOrDefault();

                if (totalSem != null)
                {
                    for (int i = 1; i <= totalSem.csemesters; i++)
                        ddlSem.Items.Add(i.ToString());
                }

                ddlSem.Text = subjectSem.ToString();
            }

            var subjectHeader = (from c in ue.Courses
                                 join ol in ue.OnlineLibrary
                                 on c.cid equals ol.Courses.cid
                                 join s in ue.Subjects
                                 on ol.Subjects.sid equals s.sid
                                 where c.cname == ddlCourse.Text && s.sname == ddlSubject.Text
                                 select ol.oltname).ToList();
            if (subjectHeader != null)
            {
                foreach (var data in subjectHeader)
                    ddlHeader.Items.Add(data);
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Data in course is updated on Submit..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var sem = 0;
            if (ddlSem.SelectedIndex != 0)
                sem = Convert.ToInt32(ddlSem.Text);

            var editFile = (from c in ue.Courses
                            join ol in ue.OnlineLibrary
                            on c.cid equals ol.Courses.cid
                            join s in ue.Subjects
                            on ol.Subjects.sid equals s.sid
                            where c.cname == ddlCourse.Text && ol.csem == sem && s.sname == ddlSubject.Text && ol.oltname == ddlHeader.Text
                            select ol).FirstOrDefault();

            // Read the file and convert it to Byte Array
            string filePath = FileUpload1.PostedFile.FileName;
            string fileName = Path.GetFileName(filePath);
            string ext = Path.GetExtension(fileName);
            string contentType = String.Empty;

            //Key for Folder Name from Web.config..
            var libraryPath = ConfigurationManager.AppSettings["OnlineLibraryPath"];

            // Specify the path to save the uploaded file to.
            string savePath = Server.MapPath("~\\"+libraryPath+"\\");


            // Create the path and file name to check for duplicates.
            string pathToCheck = savePath + fileName;

            //Set the contenttype based on File Extension
            switch (ext)
            {
                case ".pdf":
                    contentType = "application/pdf";
                    break;
            }

            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlSubject.SelectedIndex != 0)
                {
                    if (ddlHeader.SelectedIndex != 0)
                    {
                        //Stream fs = FileUpload1.PostedFile.InputStream;
                        //BinaryReader br = new BinaryReader(fs);
                        //Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        if (contentType != String.Empty)
                        {
                            File.Delete(Server.MapPath("~\\" + libraryPath + "\\" + editFile.oldesc));
                            FileUpload1.SaveAs(Server.MapPath("~\\" + libraryPath + "\\" + fileName));
                            editFile.oldesc = fileName;
                            //editfile.olfile = bytes;

                            if (ddlValid.SelectedIndex == 0)
                                editFile.olvalid = true;
                            else
                                editFile.olvalid = false;
                            ue.SaveChanges();

                            lblMsg.Text = "Success!!!Record Updated!";

                            ddlValid.SelectedIndex = ddlCourse.SelectedIndex = 0;

                            ddlSubject.Items.Clear();
                            ddlSubject.Items.Add("--Select--");

                            ddlSem.Items.Clear();
                            ddlSem.Items.Add("--Select--");

                            ddlHeader.Items.Clear();
                            ddlHeader.Items.Add("--Select--");

                        }
                        else
                            lblMsg.Text = "File format not recognised. Upload PDF formats!";

                    }
                    else
                        lblMsg.Text = "No File selected to be edited!";
                }
                else
                    lblMsg.Text = "No subject selected!";
            }
            else
                lblMsg.Text = "No Course selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    
}
