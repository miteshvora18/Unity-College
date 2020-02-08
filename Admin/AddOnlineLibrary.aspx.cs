using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;
using System.IO;
using System.Configuration;

public partial class OtherPages_OnlineLibrary : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Checks valid login..
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

                ddlSem.Items.Clear();
                ddlSem.Items.Add("--Select--");

                ddlSubject.Items.Clear();
                ddlSubject.Items.Add("--Select--");

                var course = (from c in ue.Courses
                              where c.cvalid == true
                              select c).ToList();

                if (course != null)
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
    /// Pdf is uploaded on Submit and can be viewed from main page for that course and semester
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var headerExistsInCourse = (from c in ue.Courses
                                        join ol in ue.OnlineLibrary
                                        on c.cid equals ol.Courses.cid
                                        where c.cname == ddlCourse.Text && ol.oltname == txtHeader.Text && ol.olvalid == true
                                        select ol).FirstOrDefault();

            var selectedCourse = (from c in ue.Courses
                                  where c.cname == ddlCourse.Text
                                  select c).FirstOrDefault();

            //Check selected semester
            int sem = 0;
            if (ddlSem.SelectedIndex != 0)
                sem = Convert.ToInt32(ddlSem.Text);

            var subject = (from s in ue.Subjects
                           join c in ue.Courses
                           on s.Courses.cid equals c.cid
                           where c.cname == ddlCourse.Text && s.ssem == sem && s.sname==ddlSubject.Text
                           select s).FirstOrDefault();


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
                if (ddlSem.SelectedIndex != 0)
                {
                    if (ddlSubject.SelectedIndex != 0)
                    {
                        //Check if pdf..
                        if (contentType != String.Empty)
                        {
                            //Check for duplicate header in course
                            if (headerExistsInCourse == null)
                            {
                                //Stream fs = FileUpload1.PostedFile.InputStream;
                                //BinaryReader br = new BinaryReader(fs);
                                //Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                
                                unitycollegeModel.OnlineLibrary addOnlineLibrary = new unitycollegeModel.OnlineLibrary();
                                addOnlineLibrary.oltname = txtHeader.Text;
                                addOnlineLibrary.csem = Convert.ToInt32(ddlSem.Text);
                                addOnlineLibrary.SubjectsReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Subjects", "sid", subject.sid);
                                FileUpload1.SaveAs(Server.MapPath("~\\" + libraryPath + "\\" + fileName));
                                addOnlineLibrary.oldesc = fileName;
                                //ol.olfile = bytes;
                                addOnlineLibrary.CoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Courses", "cid", selectedCourse.cid);
                                if (ddlValid.SelectedIndex == 0)
                                    addOnlineLibrary.olvalid = true;
                                else
                                    addOnlineLibrary.olvalid = false;
                                ue.AddToOnlineLibrary(addOnlineLibrary);
                                ue.SaveChanges();

                                lblMsg.Text = "Success!!!Record Saved!";
                                txtHeader.Text = "";
                                ddlValid.SelectedIndex = ddlCourse.SelectedIndex = ddlSem.SelectedIndex = ddlSubject.SelectedIndex = 0;
                                //}
                                //else
                                //    lblMsg.Text = "Uploaded file already exists for this course!";
                            }
                            else
                                lblMsg.Text = "Header already exists for this course!";
                        }
                        else
                        {
                            //lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "File format not recognised. Upload PDF formats!";
                        }
                    }
                    else
                        lblMsg.Text = "No subject selected!";
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
    /// <summary>
    /// Populates list of Semesters on index change of course..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add("--Select--");

        ddlSubject.Items.Clear();
        ddlSubject.Items.Add("--Select--");

        var sem =(from c in ue.Courses
                  where c.cname == ddlCourse.Text
                  select c).FirstOrDefault();
        if (sem != null)
        {
            for (int i = 1; i <= sem.csemesters; i++)
                ddlSem.Items.Add(i.ToString());
        }
    }
    /// <summary>
    /// Popluates list of Subject for selected semester of selected course
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubject.Items.Clear();
        ddlSubject.Items.Add("--Select--");

        int selectedSem = 0;
        if(ddlSem.SelectedIndex!=0)
            selectedSem = Convert.ToInt32(ddlSem.Text);

        var subject = (from c in ue.Courses
                       join s in ue.Subjects
                       on c.cid equals s.Courses.cid
                       where c.cname == ddlCourse.Text && s.ssem == selectedSem
                       select s).ToList();

        if (subject.Count != 0)
        {
            foreach (var data in subject)
                ddlSubject.Items.Add(data.sname);
        }    
    }
}
