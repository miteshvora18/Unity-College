using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_StudentHome : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check Valid Login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 2 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            var checkGs = (from u in ue.Users
                           where u.uvalid == true && u.Roles.rid == 2 && u.ugs == true && u.username == currentUser
                           select u).FirstOrDefault();

            if (checkGs != null)
            {
                hlkAddEvent.Visible = true;
                hlkEditEvent.Visible = true;
            }

            //Lists unread message on home page..
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

                    lblMsg.Text += "<span class='notify'><a href='MessageStudent.aspx?a=1&facultyusername=" + data.username + "'>You have " + count + " unread Message from " + data.username + "</a></span><br/>";
                }
            }

            //Populates profile for student..
            if (!IsPostBack)
            {
                string username = (string)Session["username"];
                var user = (from u in ue.Users
                            join sc in ue.StudentCourse
                            on u.uid equals sc.Users.uid
                            where u.username == username && u.Roles.rid == 2
                            select new { u, sc }).FirstOrDefault();

                if (user != null)
                {
                    int scid = user.sc.scid;

                    var course = (from c in ue.Courses
                                  join sc in ue.StudentCourse
                                  on c.cid equals sc.Courses.cid
                                  where sc.scid == scid
                                  select c).FirstOrDefault();

                    if (course != null)
                    {
                        lblName.Text = user.u.uFullname;
                        lblEmail.Text = user.u.uemail;
                        lblContact.Text = user.u.uContact.ToString();
                        lblCourse.Text = course.cname;
                        lblStartDate.Text = user.sc.scStartdate.ToString("MMM. dd yyyy");
                        lblEndDate.Text = user.sc.scEnddate.ToString("MMM. dd yyyy");
                        lblSem.Text = user.sc.scCurrentsem.ToString();
                    }

                    int j = 1;
                    var todayDate = DateTime.Now.AddDays(1).Date;

                    var extraCourse = (from ec in ue.ExtraCourses
                                       join rec in ue.ExtraCourseRegister
                                       on ec.ecid equals rec.ExtraCourses.ecid
                                       join u in ue.Users
                                       on rec.Users.uid equals u.uid
                                       where u.username == username && rec.recenddate > todayDate
                                       select new { ec.ecname, rec.recstartdate, rec.recenddate }).ToList();

                    if (extraCourse.Count != 0)
                    {
                        foreach (var data in extraCourse)
                        {
                            lblExtraCourse.Text += j + "." + data.ecname + "<br>Start Date:" + data.recstartdate.ToString("MMM. dd yyyy") + "<br>End Date:" + data.recenddate.ToString("MMM. dd yyyy") + "<br>";
                            j++;
                        }
                    }
                    else
                        lblExtraCourse.Text = "(No extra courses registered now..)";
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
        Response.Redirect("EditProfileStudent.aspx");
    }
}
