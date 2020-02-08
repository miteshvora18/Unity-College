using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_FacultyHome : System.Web.UI.Page
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

            //Shows list of unread message from students..
            var unreadMsg = (from cf in ue.CommunicateFaculty
                             join u in ue.Users
                             on cf.Users2.uid equals u.uid
                             join u2 in ue.Users
                             on cf.Users1.uid equals u2.uid
                             where cf.cfunread == true && cf.Users2.username == currentUser
                             select new { cf.Users1.uid, u2.username }).Distinct().ToList();

            if (unreadMsg.Count != 0)
            {

                foreach (var data in unreadMsg)
                {
                    var count = (from cf in ue.CommunicateFaculty
                                 join u in ue.Users
                                 on cf.Users2.uid equals u.uid
                                 where cf.cfunread == true && cf.Users2.username == currentUser && cf.Users1.uid == data.uid
                                 select cf.cfcontent).Count();

                    lblMsg.Text += "<span class='notify'><a href='MessageFaculty.aspx?a=1&studentusername=" + data.username + "'>You have " + count + " unread Message from " + data.username + "</a></span><br/>";
                }
            }
            //Populates details of logged in faculty..
            if (!IsPostBack)
            {
                int i = 1, j = 1;
                string username = (string)Session["username"];
                var user = (from u in ue.Users
                            join c in ue.Courses
                            on u.Courses.cid equals c.cid
                            where u.username == username && u.Roles.rid == 3
                            select new { u, c }).FirstOrDefault();

                if (user != null)
                {
                    var userId = (from u in ue.Users
                                  where u.username == username
                                  select u.uid).FirstOrDefault();

                    var subjects = (from s in ue.Subjects
                                    join sf in ue.SubjectFaculty
                                    on s.sid equals sf.Subjects.sid
                                    join c in ue.Courses
                                    on s.Courses.cid equals c.cid
                                    where sf.Users.uid == userId && sf.sfvalid==true
                                    select new { s, c }).ToList();

                    var todayDate = DateTime.Now;

                    var extraCourse = (from ec in ue.ExtraCourses
                                       join rec in ue.ExtraCourseRegister
                                       on ec.ecid equals rec.ExtraCourses.ecid
                                       join u in ue.Users
                                       on rec.Users.uid equals u.uid
                                       where u.username == username && rec.recenddate > todayDate
                                       select new { ec.ecname, rec.recstartdate, rec.recenddate }).ToList();

                    lblName.Text = user.u.uFullname;
                    lblEmail.Text = user.u.uemail;
                    lblContact.Text = user.u.uContact.ToString();
                    lblDept.Text = user.c.cname;

                    if (subjects.Count != 0)
                    {
                        foreach (var data in subjects)
                        {
                            lblSubjects.Text += i + ". " + data.s.sname + " : " + data.c.cname + "(Sem:" + data.s.ssem + ")" + "<br/>";
                            i++;
                        }
                    }
                    else
                        lblSubjects.Text = "(No subjects assigned)";

                    if (extraCourse.Count != 0)
                    {
                        foreach (var data in extraCourse)
                        {
                            lblExtraCourse.Text += j + "." + data.ecname + "<br>Start Date:" + data.recstartdate.ToString("MMM. dd yyyy") + "<br>End Date:" + data.recenddate.ToString("MMM. dd yyyy") + "<br>";
                            j++;
                        }
                    }
                    else
                        lblExtraCourse.Text = "(No extra courses regsitered now..)";
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    protected void btnEditProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditProfileFaculty.aspx");
    }
}
